using Unity.Mathematics;
using UnityEngine;

public class Linker : ILinker
{
    private IRepository repository;
    private IIntersectionCalculater intersectionCalculater;
    private float epsilon = 0.0001f;

    public Linker(IRepository repository, IIntersectionCalculater intersectionCalculater)
    {
        this.repository = repository;
        this.intersectionCalculater = intersectionCalculater;
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
                    Vector3[] intersections = intersectionCalculater.CalculateIntersections(curve1 as SmallCircle, curve2 as SmallCircle);
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
                    Vector3[] intersections = intersectionCalculater.CalculateIntersections(curve1 as SmallCircle, curve2 as SmallCircle);
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
                    Vector3[] intersections = intersectionCalculater.CalculateIntersections(curve1 as GreatCircleSegment, curve2 as SmallCircle);
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
                    Vector3[] intersections = intersectionCalculater.CalculateIntersections(curve1 as GreatCircle, curve2 as SmallCircle);
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
                    Vector3[] intersections = intersectionCalculater.CalculateIntersections(curve1 as GreatCircleSegment, curve2 as SmallCircle);
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
                    Vector3[] intersections = intersectionCalculater.CalculateIntersections(curve1 as GreatCircleSegment, curve2 as SmallCircle);
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

}

