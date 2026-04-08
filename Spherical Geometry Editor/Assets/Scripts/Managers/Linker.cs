using Unity.Mathematics;
using UnityEngine;

public class Linker 
{
    private IRepository repository;
    private float epsilon = 0.0001f;

    public Linker(IRepository repository)
    {
        this.repository = repository;
    }

    public void Link(LimitedPoint limitedPoint, LimitedPointDto dto)
    {
        ParametricCurve curve = (ParametricCurve)repository.GetById(dto.Curve);
        limitedPoint.SetCurve(curve);
    }

    public void Link(AntipodalPoint antipodalPoint, AntipodalPointDto dto)
    {
        ControllPoint controllPoint = (ControllPoint)repository.GetById(dto.ControllPoint);
        antipodalPoint.SetPoint(controllPoint);
    }

    public void Link(MidPoint midPoint, MidPointDto dto)
    {
        ControllPoint controllPoint1 = (ControllPoint)repository.GetById(dto.ControllPoint1);
        ControllPoint controllPoint2 = (ControllPoint)repository.GetById(dto.ControllPoint2);
        midPoint.SetPoints(controllPoint1, controllPoint2);
    }

    public void Link(PolePoint polePoint, PolePointDto dto)
    {
        ParametricCurve curve = (ParametricCurve)repository.GetById(dto.Curve);
        polePoint.SetCurve(curve, dto.sign);
    }

    public void Link(ShadowPolePoint shadowPolePoint, ShadowPolePointDto dto)
    {
        ParametricCurve curve = (ParametricCurve)repository.GetById(dto.Curve);
        shadowPolePoint.SetCurve(curve, dto.sign);
    }

    public void Link(GreatCircle greatCircle, GreatCircleDto dto)
    {
        ControllPoint controllPoint1 = (ControllPoint)repository.GetById(dto.ControllPoint1);
        ControllPoint controllPoint2 = (ControllPoint)repository.GetById(dto.ControllPoint2);
        greatCircle.AddControllPoints(controllPoint1, controllPoint2);
    }

    public void Link(SmallCircle smallCircle, SmallCircleDto dto) 
    {
        ControllPoint controllPoint1 = (ControllPoint)repository.GetById(dto.ControllPoint1);
        ControllPoint controllPoint2 = (ControllPoint)repository.GetById(dto.ControllPoint2);
        smallCircle.AddControllPoints(controllPoint1, controllPoint2);
    }

    public void Link(GreatCircleSegment greatCircleSegment, GreatCircleSegmentDto dto)
    {
        ControllPoint EndPoint1 = (ControllPoint)repository.GetById(dto.EndPoint1);
        ControllPoint EndPoint2 = (ControllPoint)repository.GetById(dto.EndPoint2);
        greatCircleSegment.AddControllPoints(EndPoint1, EndPoint2);
    }

    public void Link(IntersectionPoint intersectionPoint, IntersectionPointDto dto)
    {
        ParametricCurve curve1 = (ParametricCurve)repository.GetById(dto.Curve1);
        ParametricCurve curve2 = (ParametricCurve)repository.GetById(dto.Curve2);

        GreatCircle greatCircle;
        GreatCircleSegment greatCircleSegment;
        SmallCircle smallCircle;

        switch (dto.IntersectionType)
        {
            case IntersectionType.GREATCIRCLE_GREATCIRCLE_A:
                intersectionPoint.SetRecalculate(IntersectionType.GREATCIRCLE_GREATCIRCLE_A, curve1, curve2, (curve1, curve2) => Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane));
                break;
            case IntersectionType.GREATCIRCLE_GREATCIRCLE_B:
                intersectionPoint.SetRecalculate(IntersectionType.GREATCIRCLE_GREATCIRCLE_B, curve1, curve2, (curve1, curve2) => -Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane));
                break;
            case IntersectionType.GREATCIRCLE_GREATCIRCLESEGMENT:

                if (curve1 is GreatCircle && curve2 is GreatCircleSegment)
                {
                    greatCircle = curve1 as GreatCircle;
                    greatCircleSegment = curve2 as GreatCircleSegment;
                }
                else
                {
                    greatCircleSegment = curve1 as GreatCircleSegment;
                    greatCircle = curve2 as GreatCircle;
                }

                intersectionPoint.SetRecalculate(IntersectionType.GREATCIRCLE_GREATCIRCLESEGMENT, greatCircleSegment, greatCircle,
                    (curve1, curve2) =>
                    {
                        Vector3 possibleIntersection = Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane);
                        Vector3[] curve1Endpoints = ((GreatCircleSegment)(curve1)).GetEndpoints();
                        Debug.Log(Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + " " + Vector3.Angle(curve1Endpoints[0], possibleIntersection) + " " + Vector3.Angle(possibleIntersection, curve1Endpoints[1]));
                        if ((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + 0.0001 >= Vector3.Angle(curve1Endpoints[0], possibleIntersection) + Vector3.Angle(curve1Endpoints[1], possibleIntersection)))
                        {
                            return possibleIntersection;
                        }
                        else if ((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + 0.0001 >= Vector3.Angle(curve1Endpoints[0], -possibleIntersection) + Vector3.Angle(curve1Endpoints[1], -possibleIntersection)))
                        {
                            return -possibleIntersection;
                        }
                        else
                        {
                            return new Vector3(0, 0, 0);
                        }
                    });
                break;
            case IntersectionType.GREATCIRCLESEGMENT_GREATCIRCLESEGMENT:
                intersectionPoint.SetRecalculate(IntersectionType.GREATCIRCLESEGMENT_GREATCIRCLESEGMENT, curve1, curve2,
                    (curve1, curve2) =>
                    {
                        Vector3 possibleIntersection = Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane);
                        Vector3[] curve1Endpoints = ((GreatCircleSegment)(curve1)).GetEndpoints();
                        Vector3[] curve2Endpoints = ((GreatCircleSegment)(curve2)).GetEndpoints();
                        Debug.Log(Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + " " + Vector3.Angle(curve1Endpoints[0], possibleIntersection) + " " + Vector3.Angle(possibleIntersection, curve1Endpoints[1]));
                        if ((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + 0.0001 >= Vector3.Angle(curve1Endpoints[0], possibleIntersection) + Vector3.Angle(curve1Endpoints[1], possibleIntersection)) && (Vector3.Angle(curve2Endpoints[0], curve2Endpoints[1]) + 0.0001 >= Vector3.Angle(curve2Endpoints[0], possibleIntersection) + Vector3.Angle(curve2Endpoints[1], possibleIntersection)))
                        {
                            return possibleIntersection;
                        }
                        else if ((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + 0.0001 >= Vector3.Angle(curve1Endpoints[0], -possibleIntersection) + Vector3.Angle(curve1Endpoints[1], -possibleIntersection)) && (Vector3.Angle(curve2Endpoints[0], curve2Endpoints[1]) + 0.0001 >= Vector3.Angle(curve2Endpoints[0], -possibleIntersection) + Vector3.Angle(curve2Endpoints[1], -possibleIntersection)))
                        {
                            return -possibleIntersection;
                        }
                        else
                        {
                            return new Vector3(0, 0, 0);
                        }
                    });
                break;
            case IntersectionType.SMALLCIRCLE_SMALLCIRCLE_A:

                intersectionPoint.SetRecalculate(IntersectionType.SMALLCIRCLE_SMALLCIRCLE_A, curve1, curve2, (curve1, curve2) =>
                {
                    Vector3[] intersections = CalculateIntersections(curve1 as SmallCircle, curve2 as SmallCircle);
                    if (intersections.Length == 0)
                    {
                        return Vector3.zero;
                    }
                    else
                    {
                        return intersections[0];
                    }
                });

                break;
            case IntersectionType.SMALLCIRCLE_SMALLCIRCLE_B:

                intersectionPoint.SetRecalculate(IntersectionType.SMALLCIRCLE_SMALLCIRCLE_B, curve1, curve2, (curve1, curve2) =>
                {
                    Vector3[] intersections = CalculateIntersections(curve1 as SmallCircle, curve2 as SmallCircle);
                    if (intersections.Length == 0)
                    {
                        return Vector3.zero;
                    }
                    else
                    {
                        return intersections[1];
                    }
                });

                break;
            case IntersectionType.SMALLCIRCLE_GREATCIRCLE_A:

                if (curve1 is GreatCircle && curve2 is SmallCircle)
                {
                    greatCircle = curve1 as GreatCircle;
                    smallCircle = curve2 as SmallCircle;
                }
                else
                {
                    smallCircle = curve1 as SmallCircle;
                    greatCircle = curve2 as GreatCircle;
                }

                intersectionPoint.SetRecalculate(IntersectionType.SMALLCIRCLE_GREATCIRCLE_A, greatCircle, smallCircle, (curve1, curve2) =>
                {
                    Vector3[] intersections = CalculateIntersections(curve1 as GreatCircleSegment, curve2 as SmallCircle);
                    if (intersections.Length == 0)
                    {
                        return Vector3.zero;
                    }
                    else
                    {
                        return intersections[0];
                    }
                });

                break;
            case IntersectionType.SMALLCIRCLE_GREATCIRCLE_B:

                if (curve1 is GreatCircle && curve2 is SmallCircle)
                {
                    greatCircle = curve1 as GreatCircle;
                    smallCircle = curve2 as SmallCircle;
                }
                else
                {
                    smallCircle = curve1 as SmallCircle;
                    greatCircle = curve2 as GreatCircle;
                }
                intersectionPoint.SetRecalculate(IntersectionType.SMALLCIRCLE_GREATCIRCLE_B, greatCircle, smallCircle, (curve1, curve2) =>
                {
                    Vector3[] intersections = CalculateIntersections(curve1 as GreatCircle, curve2 as SmallCircle);
                    if (intersections.Length == 0)
                    {
                        return Vector3.zero;
                    }
                    else if (intersections.Length == 2)
                    {
                        return intersections[1];
                    }
                    else
                    {
                        return intersections[0];
                    }
                });

                break;
            case IntersectionType.SMALLCIRCLE_GREATCIRCLESEGMENT_A:

                if (curve1 is GreatCircleSegment && curve2 is SmallCircle)
                {
                    greatCircleSegment = curve1 as GreatCircleSegment;
                    smallCircle = curve2 as SmallCircle;
                }
                else
                {
                    smallCircle = curve1 as SmallCircle;
                    greatCircleSegment = curve2 as GreatCircleSegment;
                }

                intersectionPoint.SetRecalculate(IntersectionType.SMALLCIRCLE_GREATCIRCLESEGMENT_A, greatCircleSegment, smallCircle, (curve1, curve2) =>
                {
                    Vector3[] intersections = CalculateIntersections(curve1 as GreatCircleSegment, curve2 as SmallCircle);
                    if (intersections.Length == 0)
                    {
                        return Vector3.zero;
                    }
                    else
                    {
                        return intersections[0];
                    }
                });
                break;
            case IntersectionType.SMALLCIRCLE_GREATCIRCLESEGMENT_B:

                if (curve1 is GreatCircleSegment && curve2 is SmallCircle)
                {
                    greatCircleSegment = curve1 as GreatCircleSegment;
                    smallCircle = curve2 as SmallCircle;
                }
                else
                {
                    smallCircle = curve1 as SmallCircle;
                    greatCircleSegment = curve2 as GreatCircleSegment;
                }


                intersectionPoint.SetRecalculate(IntersectionType.SMALLCIRCLE_GREATCIRCLESEGMENT_B, greatCircleSegment, smallCircle, (curve1, curve2) =>
                {
                    Vector3[] intersections = CalculateIntersections(curve1 as GreatCircleSegment, curve2 as SmallCircle);
                    if (intersections.Length == 0)
                    {
                        return Vector3.zero;
                    }
                    else
                    {
                        return intersections[1];
                    }
                });

                break;
            default:
                break;
        }

    }

    private Vector3[] CalculateIntersections(GreatCircleSegment greatCircleSegment, SmallCircle smallCircle)
    {
        Vector3 greatCircleSegmentCenter = greatCircleSegment.Center;
        Vector3 greatCircleSegmentNormal = greatCircleSegment.NormalOfPlane.normalized;
        Vector3[] greatCircleSegmentEndPoints = greatCircleSegment.GetEndpoints();
        Vector3 smallCircleCenter = smallCircle.Center;
        Vector3 smallCircleNormal = smallCircle.NormalOfPlane.normalized;

        // Intersection of Two Planes John Krumm Microsoft Research Redmond, WA, USA 5 / 15 / 00

        // csak akkor lehet 0 a determinánsa az A-nak ha párhuzamos a 2 sík vagy egybe esik
        float[,] A = {
            {2, 0, 0, greatCircleSegmentNormal.x, smallCircleNormal.x},
            {0, 2, 0, greatCircleSegmentNormal.y, smallCircleNormal.y},
            {0, 0, 2, greatCircleSegmentNormal.z, smallCircleNormal.z},
            {greatCircleSegmentNormal.x, greatCircleSegmentNormal.y, greatCircleSegmentNormal.z, 0, 0},
            {smallCircleNormal.x, smallCircleNormal.y, smallCircleNormal.z, 0, 0}
        };

        float[] B = { 0, 0, 0, Vector3.Dot(greatCircleSegmentCenter, greatCircleSegmentNormal), Vector3.Dot(smallCircleCenter, smallCircleNormal) };

        float DetA = Determinant(A, 5);

        if (DetA == 0)
        {
            Debug.Log("paralell");
            return new Vector3[0];
        }

        //cramer szabály, Juhász Tibor linalg jegyzet

        float[,] delta0 = {
            {B[0], A[0, 1], A[0, 2], A[0, 3], A[0, 4]},
            {B[1], A[1, 1], A[1, 2], A[1, 3], A[1, 4]},
            {B[2], A[2, 1], A[2, 2], A[2, 3], A[2, 4]},
            {B[3], A[3, 1], A[3, 2], A[3, 3], A[3, 4]},
            {B[4], A[4, 1], A[4, 2], A[4, 3], A[4, 4]}
        };

        float[,] delta1 = {
            {A[0, 0], B[0], A[0, 2], A[0, 3], A[0, 4]},
            {A[1, 0], B[1], A[1, 2], A[1, 3], A[1, 4]},
            {A[2, 0], B[2], A[2, 2], A[2, 3], A[2, 4]},
            {A[3, 0], B[3], A[3, 2], A[3, 3], A[3, 4]},
            {A[4, 0], B[4], A[4, 2], A[4, 3], A[4, 4]}
        };

        float[,] delta2 = {
            {A[0, 0], A[0, 1], B[0], A[0, 3], A[0, 4]},
            {A[1, 0], A[1, 1], B[1], A[1, 3], A[1, 4]},
            {A[2, 0], A[2, 1], B[2], A[2, 3], A[2, 4]},
            {A[3, 0], A[3, 1], B[3], A[3, 3], A[3, 4]},
            {A[4, 0], A[4, 1], B[4], A[4, 3], A[4, 4]}
        };


        Vector3 d = Vector3.Cross(greatCircleSegmentNormal, smallCircleNormal).normalized;
        float x0 = Determinant(delta0, 5) / DetA;
        float y0 = Determinant(delta1, 5) / DetA;
        float z0 = Determinant(delta2, 5) / DetA;

        // ha a kapott pont hossza 1 vagyis a gömbön van akkor az egyenes érinti -> nem kell tivább számolni ez a metszés pont is ez az érintő

        // gömb egyenes metszés pont 

        Vector3 closestPoint = new Vector3(x0, y0, z0);

        Vector3[] intersections = new Vector3[0];

        if (closestPoint.magnitude - epsilon >= 1)
        {
            return intersections;
        }

        float a = d.x * d.x + d.y * d.y + d.z * d.z;
        float b = 2 * (d.x * x0 + d.y * y0 + d.z * z0);
        float c = x0 * x0 + y0 * y0 + z0 * z0 - 1;

        //if ((b * b - 4 * a * c) < 0)
        //{
        //    return intersections;
        //}

        if (closestPoint.magnitude + epsilon >= 1)
        {
            float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
            Vector3 possibleIntersection = new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z);


            if (Vector3.Angle(greatCircleSegmentEndPoints[0], greatCircleSegmentEndPoints[1]) + epsilon >= Vector3.Angle(greatCircleSegmentEndPoints[0], possibleIntersection) + Vector3.Angle(greatCircleSegmentEndPoints[1], possibleIntersection))
            {
                intersections = new Vector3[] { possibleIntersection };
            }

        }
        else
        {
            float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
            float p2 = (-b - math.sqrt(b * b - 4 * a * c)) / (2 * a);

            intersections = new Vector3[2];

            Vector3[] possibleIntersections = {
            new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z),
            new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z)
            };

            if (Vector3.Angle(greatCircleSegmentEndPoints[0], greatCircleSegmentEndPoints[1]) + epsilon >= Vector3.Angle(greatCircleSegmentEndPoints[0], possibleIntersections[0]) + Vector3.Angle(greatCircleSegmentEndPoints[1], possibleIntersections[0]))
            {
                intersections[0] = possibleIntersections[0];
            }

            if (Vector3.Angle(greatCircleSegmentEndPoints[0], greatCircleSegmentEndPoints[1]) + epsilon >= Vector3.Angle(greatCircleSegmentEndPoints[0], possibleIntersections[1]) + Vector3.Angle(greatCircleSegmentEndPoints[1], possibleIntersections[1]))
            {
                intersections[1] = possibleIntersections[1];
            }
        }

        return intersections;
    }

    private Vector3[] CalculateIntersections(GreatCircle greatCircle, SmallCircle smallCircle)
    {
        Vector3 greatCircleNormal = greatCircle.NormalOfPlane.normalized;
        Vector3 smallCircleCenter = smallCircle.Center;
        Vector3 smallCircleNormal = smallCircle.NormalOfPlane.normalized;

        // Intersection of Two Planes John Krumm Microsoft Research Redmond, WA, USA 5 / 15 / 00

        // csak akkor lehet 0 a determinánsa az A-nak ha párhuzamos a 2 sík vagy egybe esik
        float[,] A = {
            {2, 0, 0, greatCircleNormal.x, smallCircleNormal.x},
            {0, 2, 0, greatCircleNormal.y, smallCircleNormal.y},
            {0, 0, 2, greatCircleNormal.z, smallCircleNormal.z},
            {greatCircleNormal.x, greatCircleNormal.y, greatCircleNormal.z, 0, 0},
            {smallCircleNormal.x, smallCircleNormal.y, smallCircleNormal.z, 0, 0}
        };

        float[] B = { 0, 0, 0, Vector3.Dot(Vector3.zero, greatCircleNormal), Vector3.Dot(smallCircleCenter, smallCircleNormal) };

        float DetA = Determinant(A, 5);

        if (DetA == 0)
        {
            Debug.Log("paralell");
            return new Vector3[0];
        }

        //cramer szabály, Juhász Tibor linalg jegyzet

        float[,] delta0 = {
            {B[0], A[0, 1], A[0, 2], A[0, 3], A[0, 4]},
            {B[1], A[1, 1], A[1, 2], A[1, 3], A[1, 4]},
            {B[2], A[2, 1], A[2, 2], A[2, 3], A[2, 4]},
            {B[3], A[3, 1], A[3, 2], A[3, 3], A[3, 4]},
            {B[4], A[4, 1], A[4, 2], A[4, 3], A[4, 4]}
        };

        float[,] delta1 = {
            {A[0, 0], B[0], A[0, 2], A[0, 3], A[0, 4]},
            {A[1, 0], B[1], A[1, 2], A[1, 3], A[1, 4]},
            {A[2, 0], B[2], A[2, 2], A[2, 3], A[2, 4]},
            {A[3, 0], B[3], A[3, 2], A[3, 3], A[3, 4]},
            {A[4, 0], B[4], A[4, 2], A[4, 3], A[4, 4]}
        };

        float[,] delta2 = {
            {A[0, 0], A[0, 1], B[0], A[0, 3], A[0, 4]},
            {A[1, 0], A[1, 1], B[1], A[1, 3], A[1, 4]},
            {A[2, 0], A[2, 1], B[2], A[2, 3], A[2, 4]},
            {A[3, 0], A[3, 1], B[3], A[3, 3], A[3, 4]},
            {A[4, 0], A[4, 1], B[4], A[4, 3], A[4, 4]}
        };


        Vector3 d = Vector3.Cross(greatCircleNormal, smallCircleNormal).normalized;
        float x0 = Determinant(delta0, 5) / DetA;
        float y0 = Determinant(delta1, 5) / DetA;
        float z0 = Determinant(delta2, 5) / DetA;

        // gömb egyenes metszés pont 

        Vector3 closestPoint = new Vector3(x0, y0, z0);

        Vector3[] intersections = new Vector3[0];

        if (closestPoint.magnitude - epsilon >= 1)
        {
            return intersections;
        }

        float a = d.x * d.x + d.y * d.y + d.z * d.z;
        float b = 2 * (d.x * x0 + d.y * y0 + d.z * z0);
        float c = x0 * x0 + y0 * y0 + z0 * z0 - 1;

        //if ((b * b - 4 * a * c) < 0)
        //{
        //    Debug.Log((b * b - 4 * a * c));
        //    Debug.Log("no intersection");
        //    return new Vector3[0];
        //}


        if (closestPoint.magnitude + epsilon >= 1)
        {
            float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
            intersections = new Vector3[] { new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z) };
        }
        else
        {
            float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
            float p2 = (-b - math.sqrt(b * b - 4 * a * c)) / (2 * a);

            intersections = new Vector3[] {
                new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z),
                new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z)
            };
        }

        return intersections;
    }

    private Vector3[] CalculateIntersections(SmallCircle smallCircle1, SmallCircle smallCircle2)
    {
        Vector3 smallCircle1Center = smallCircle1.Center;
        Vector3 smallCircle1Normal = smallCircle1.NormalOfPlane.normalized;
        Vector3 smallCircle2Center = smallCircle2.Center;
        Vector3 smallCircle2Normal = smallCircle2.NormalOfPlane.normalized;

        // Intersection of Two Planes John Krumm Microsoft Research Redmond, WA, USA 5 / 15 / 00

        // csak akkor lehet 0 a determinánsa az A-nak ha párhuzamos a 2 sík vagy egybe esik
        float[,] A = {
            {2, 0, 0, smallCircle1Normal.x, smallCircle2Normal.x},
            {0, 2, 0, smallCircle1Normal.y, smallCircle2Normal.y},
            {0, 0, 2, smallCircle1Normal.z, smallCircle2Normal.z},
            {smallCircle1Normal.x, smallCircle1Normal.y, smallCircle1Normal.z, 0, 0},
            {smallCircle2Normal.x, smallCircle2Normal.y, smallCircle2Normal.z, 0, 0}
        };

        float[] B = { 0, 0, 0, Vector3.Dot(smallCircle1Center, smallCircle1Normal), Vector3.Dot(smallCircle2Center, smallCircle2Normal) };

        float DetA = Determinant(A, 5);

        if (DetA == 0)
        {
            Debug.Log("paralell");
            return new Vector3[0];
        }

        //cramer szabály, Juhász Tibor linalg jegyzet

        float[,] delta0 = {
            {B[0], A[0, 1], A[0, 2], A[0, 3], A[0, 4]},
            {B[1], A[1, 1], A[1, 2], A[1, 3], A[1, 4]},
            {B[2], A[2, 1], A[2, 2], A[2, 3], A[2, 4]},
            {B[3], A[3, 1], A[3, 2], A[3, 3], A[3, 4]},
            {B[4], A[4, 1], A[4, 2], A[4, 3], A[4, 4]}
        };

        float[,] delta1 = {
            {A[0, 0], B[0], A[0, 2], A[0, 3], A[0, 4]},
            {A[1, 0], B[1], A[1, 2], A[1, 3], A[1, 4]},
            {A[2, 0], B[2], A[2, 2], A[2, 3], A[2, 4]},
            {A[3, 0], B[3], A[3, 2], A[3, 3], A[3, 4]},
            {A[4, 0], B[4], A[4, 2], A[4, 3], A[4, 4]}
        };

        float[,] delta2 = {
            {A[0, 0], A[0, 1], B[0], A[0, 3], A[0, 4]},
            {A[1, 0], A[1, 1], B[1], A[1, 3], A[1, 4]},
            {A[2, 0], A[2, 1], B[2], A[2, 3], A[2, 4]},
            {A[3, 0], A[3, 1], B[3], A[3, 3], A[3, 4]},
            {A[4, 0], A[4, 1], B[4], A[4, 3], A[4, 4]}
        };


        Vector3 d = Vector3.Cross(smallCircle1Normal, smallCircle2Normal).normalized;
        float x0 = Determinant(delta0, 5) / DetA;
        float y0 = Determinant(delta1, 5) / DetA;
        float z0 = Determinant(delta2, 5) / DetA;

        // gömb egyenes metszés pont 

        float a = d.x * d.x + d.y * d.y + d.z * d.z;
        float b = 2 * (d.x * x0 + d.y * y0 + d.z * z0);
        float c = x0 * x0 + y0 * y0 + z0 * z0 - 1;

        Vector3 closestPoint = new Vector3(x0, y0, z0);

        Vector3[] intersections = new Vector3[0];

        if (closestPoint.magnitude - epsilon >= 1)
        {
            return intersections;
        }


        //if ((b * b - 4 * a * c) < 0)
        //{
        //    Debug.Log((b * b - 4 * a * c));
        //    Debug.Log("no intersection");
        //    return new Vector3[0];
        //}

        if (closestPoint.magnitude + epsilon >= 1)
        {
            float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
            intersections = new Vector3[] { new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z) };
        }
        else
        {
            float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
            float p2 = (-b - math.sqrt(b * b - 4 * a * c)) / (2 * a);

            intersections = new Vector3[] {
                new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z),
                new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z)
            };
        }

        return intersections;


        //float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
        //float p2 = (-b - math.sqrt(b * b - 4 * a * c)) / (2 * a);
        //Vector3[] intersections = {
        //    new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z),
        //    new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z)
        //};

        //return intersections;
    }

    public float Determinant(float[,] matrix, int n)
    {
        float det = 0;
        if (n == 1)
        {
            det = matrix[0, 0];
        }
        else if (n == 2)
        {
            det = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                float[,] subMatrix = new float[n - 1, n - 1];
                for (int j = 1; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (k < i)
                        {
                            subMatrix[j - 1, k] = matrix[j, k];
                        }
                        else if (k > i)
                        {
                            subMatrix[j - 1, k - 1] = matrix[j, k];
                        }
                    }
                }
                det += Mathf.Pow(-1, i) * matrix[0, i] * Determinant(subMatrix, n - 1);
            }
        }

        return det;
    }

}

