using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SphericalGeometryFactory
{
    GameObject GrabablePointPrefab;
    GameObject IntersectionPointPrefab;
    GameObject SmallCirclePrefab;
    GameObject GreatCirclePrefab;
    GameObject GreatCircleSegmentPrefab;
    GameObject LimitedPointPrefab;
    double epsilon = 0.0001;

    public SphericalGeometryFactory(GameObject grabablePointPrefab, GameObject intersectionPointPrefab, GameObject smallCirclePrefab, GameObject greatCirclePrefab, GameObject greatCircleSegmentPrefab, GameObject limitedPointPrefab)
    {
        this.GrabablePointPrefab = grabablePointPrefab;
        this.IntersectionPointPrefab = intersectionPointPrefab;
        this.SmallCirclePrefab = smallCirclePrefab;
        this.GreatCirclePrefab = greatCirclePrefab;
        this.GreatCircleSegmentPrefab = greatCircleSegmentPrefab;
        this.LimitedPointPrefab = limitedPointPrefab;
    }

    public GrabablePoint CreateGrabablepoint(Vector3 pos)
    {
        GameObject grabablePoint = MonoBehaviour.Instantiate(GrabablePointPrefab);
        grabablePoint.transform.position = pos;
        GrabablePoint script = grabablePoint.GetComponent<GrabablePoint>();
        script.Id = Guid.NewGuid();
        return script;
    }

    public LimitedPoint CreateLimitedpoint(Vector3 pos)
    {
        GameObject limitedPoint = MonoBehaviour.Instantiate(LimitedPointPrefab);
        limitedPoint.transform.position = pos;
        LimitedPoint script = limitedPoint.GetComponent<LimitedPoint>();
        script.Id = Guid.NewGuid();
        return script;
    }

    public GreatCircle CreateGreatCircle(Vector3 point1Pos, Vector3 point2Pos)
    {
        GameObject greatCircle = MonoBehaviour.Instantiate(GreatCirclePrefab);
        greatCircle.transform.position = new Vector3(0,0,0);
        GreatCircle script = greatCircle.GetComponent<GreatCircle>();
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1Pos, point2Pos, script.CreateMesh);
        script.Id = Guid.NewGuid();
        return script;
    }

    public SmallCircle CreateSmallCircle(Vector3 point1Pos, Vector3 point2Pos)
    {
        Debug.Log("small circle create");
        GameObject smallCircle = MonoBehaviour.Instantiate(SmallCirclePrefab);
        Debug.Log(" instantiate");
        smallCircle.transform.position = new Vector3(0, 0, 0);
        SmallCircle script = smallCircle.GetComponent<SmallCircle>();
        ParametricCurveMeshGenerator.Instance.CreateSmallCircleMesh(point1Pos, point2Pos, script.CreateMesh);
        script.Id = Guid.NewGuid();
        return script;
    }

    public GreatCircleSegment CreateGreatCircleSegment(Vector3 point1Pos, Vector3 point2Pos)
    {
        GameObject greatCircleSegment = MonoBehaviour.Instantiate(GreatCircleSegmentPrefab);
        greatCircleSegment.transform.position = new Vector3(0, 0, 0);
        GreatCircleSegment script = greatCircleSegment.GetComponent<GreatCircleSegment>();
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleSegmentMesh(point1Pos, point2Pos, script.CreateMesh);
        script.Id = Guid.NewGuid();
        return script;
    }

    public IntersectionPoint[] CreateIntersectionPoints(GreatCircle greatCircle1, GreatCircle greatCircle2)
    {
        Vector3 dir = Vector3.Cross(greatCircle1.NormalOfPlane, greatCircle2.NormalOfPlane);
        GameObject point1 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point1.transform.position = dir.normalized;
        IntersectionPoint point1Script = point1.GetComponent<IntersectionPoint>();
        point1Script.SetRecalculate(greatCircle1, greatCircle2, (curve1, curve2) => Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane));
        point1Script.Id = Guid.NewGuid();

        GameObject point2 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point2.transform.position = -dir.normalized;
        IntersectionPoint point2Script = point2.GetComponent<IntersectionPoint>();
        point2Script.SetRecalculate(greatCircle1, greatCircle2, (curve1, curve2) => -Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane));
        point2Script.Id = Guid.NewGuid();

        return new IntersectionPoint[] { point1Script, point2Script };
    }

    public IntersectionPoint[] CreateIntersectionPoints(GreatCircleSegment greatCircleSegment, GreatCircle greatCircle)
    {
        Vector3 dir = Vector3.Cross(greatCircleSegment.NormalOfPlane, greatCircle.NormalOfPlane);

        Vector3[] segment1Endpoints = greatCircleSegment.GetEndpoints();

        if (((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], dir) + Vector3.Angle(segment1Endpoints[1], dir)) || ((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], -dir) + Vector3.Angle(segment1Endpoints[1], -dir)))))
        {
            GameObject point1 = MonoBehaviour.Instantiate(IntersectionPointPrefab);

            point1.transform.position = ((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], dir) + Vector3.Angle(segment1Endpoints[1], dir))) ? dir.normalized : -dir.normalized;
            IntersectionPoint script = point1.GetComponent<IntersectionPoint>();
            script.SetRecalculate(greatCircleSegment, greatCircle,
            (curve1, curve2) =>
            {
                Vector3 possibleIntersection = Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane);
                Vector3[] curve1Endpoints = ((GreatCircleSegment)(curve1)).GetEndpoints();
                Debug.Log(Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + " " + Vector3.Angle(curve1Endpoints[0], possibleIntersection) + " " + Vector3.Angle(possibleIntersection, curve1Endpoints[1]));
                if ((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + epsilon >= Vector3.Angle(curve1Endpoints[0], possibleIntersection) + Vector3.Angle(curve1Endpoints[1], possibleIntersection)))
                {
                    return possibleIntersection;
                }
                else if ((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + epsilon >= Vector3.Angle(curve1Endpoints[0], -possibleIntersection) + Vector3.Angle(curve1Endpoints[1], -possibleIntersection)))
                {
                    return -possibleIntersection;
                }
                else
                {
                    return new Vector3(0, 0, 0);
                }
            });

            script.Id = Guid.NewGuid();
            return new IntersectionPoint[] {script};
        }

        return new IntersectionPoint[] { };
    }

    public IntersectionPoint[] CreateIntersectionPoints(GreatCircleSegment greatCircleSegment1, GreatCircleSegment greatCircleSegment2)
    {
        Vector3 dir = Vector3.Cross(greatCircleSegment1.NormalOfPlane, greatCircleSegment2.NormalOfPlane);

        Vector3[] segment1Endpoints = greatCircleSegment1.GetEndpoints();
        Vector3[] segment2Endpoints = greatCircleSegment2.GetEndpoints();

        if (((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], dir) + Vector3.Angle(segment1Endpoints[1], dir)) && (Vector3.Angle(segment2Endpoints[0], segment2Endpoints[1]) + epsilon >= Vector3.Angle(segment2Endpoints[0], dir) + Vector3.Angle(segment2Endpoints[1], dir))) || ((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], -dir) + Vector3.Angle(segment1Endpoints[1], -dir)) && (Vector3.Angle(segment2Endpoints[0], segment2Endpoints[1]) + epsilon >= Vector3.Angle(segment2Endpoints[0], -dir) + Vector3.Angle(segment2Endpoints[1], -dir))))
        {
            GameObject point1 = MonoBehaviour.Instantiate(IntersectionPointPrefab);


            point1.transform.position = ((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], dir) + Vector3.Angle(segment1Endpoints[1], dir)) && (Vector3.Angle(segment2Endpoints[0], segment2Endpoints[1]) + epsilon >= Vector3.Angle(segment2Endpoints[0], dir) + Vector3.Angle(segment2Endpoints[1], dir))) ? dir.normalized : -dir.normalized;
            IntersectionPoint script = point1.GetComponent<IntersectionPoint>();
            script.SetRecalculate(greatCircleSegment1, greatCircleSegment2,
            (curve1, curve2) =>
            {
                Vector3 possibleIntersection = Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane);
                Vector3[] curve1Endpoints = ((GreatCircleSegment)(curve1)).GetEndpoints();
                Vector3[] curve2Endpoints = ((GreatCircleSegment)(curve2)).GetEndpoints();
                Debug.Log(Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + " " + Vector3.Angle(curve1Endpoints[0], possibleIntersection) + " " + Vector3.Angle(possibleIntersection, curve1Endpoints[1]));
                if ((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + epsilon >= Vector3.Angle(curve1Endpoints[0], possibleIntersection) + Vector3.Angle(curve1Endpoints[1], possibleIntersection)) && (Vector3.Angle(curve2Endpoints[0], curve2Endpoints[1]) + epsilon >= Vector3.Angle(curve2Endpoints[0], possibleIntersection) + Vector3.Angle(curve2Endpoints[1], possibleIntersection)))
                {
                    return possibleIntersection;
                }
                else if ((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + epsilon >= Vector3.Angle(curve1Endpoints[0], -possibleIntersection) + Vector3.Angle(curve1Endpoints[1], -possibleIntersection)) && (Vector3.Angle(curve2Endpoints[0], curve2Endpoints[1]) + epsilon >= Vector3.Angle(curve2Endpoints[0], -possibleIntersection) + Vector3.Angle(curve2Endpoints[1], -possibleIntersection)))
                {
                    return -possibleIntersection;
                }
                else
                {
                    return new Vector3(0, 0, 0);
                }
            });

            script.Id = Guid.NewGuid();
            return new IntersectionPoint[] { script };
        }

        return new IntersectionPoint[] { };
    }

    public IntersectionPoint[] CreateIntersectionPoints(GreatCircleSegment greatCircleSegment, SmallCircle smallCircle)
    {
        Vector3[] intersections = CalculateIntersections(greatCircleSegment, smallCircle);
        IntersectionPoint[] intersectionPoints = new IntersectionPoint[intersections.Length];

        if (intersections.Length == 0)
        {
            return new IntersectionPoint[] { };
        }

        if (intersections.Length == 1) {
            GameObject point = MonoBehaviour.Instantiate(IntersectionPointPrefab);
            point.transform.position = intersections[0];
            IntersectionPoint point1Script = point.GetComponent<IntersectionPoint>();
            intersectionPoints[0] = point1Script;
            point1Script.SetRecalculate(greatCircleSegment, smallCircle, (curve1, curve2) =>
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
            intersectionPoints[0] = point1Script;
            point1Script.Id = Guid.NewGuid();
            return intersectionPoints;
        }

        if (intersections[0] != Vector3.zero)
        {
            GameObject point1 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
            point1.transform.position = intersections[0];
            IntersectionPoint point1Script = point1.GetComponent<IntersectionPoint>();
            intersectionPoints[0] = point1Script;
            point1Script.Id = Guid.NewGuid();
            point1Script.SetRecalculate(greatCircleSegment, smallCircle, (curve1, curve2) =>
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
        }

        if (intersections[1] != Vector3.zero)
        {
            GameObject point2 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
            point2.transform.position = intersections[1];
            IntersectionPoint point2Script = point2.GetComponent<IntersectionPoint>();
            intersectionPoints[1] = point2Script;
            point2Script.Id = Guid.NewGuid();
            point2Script.SetRecalculate(greatCircleSegment, smallCircle, (curve1, curve2) =>
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
        }
        return intersectionPoints;
    }

    public IntersectionPoint[] CreateIntersectionPoints(GreatCircle greatCircle, SmallCircle smallCircle)
    {
        Vector3[] intersections = CalculateIntersections(greatCircle, smallCircle);
        IntersectionPoint[] intersectionPoints = new IntersectionPoint[intersections.Length];

        Debug.Log(intersections.Length);

        if (intersections.Length == 0)
        {
            return new IntersectionPoint[] { };
        }

        if (intersections.Length == 1)
        {
            GameObject point = MonoBehaviour.Instantiate(IntersectionPointPrefab);
            point.transform.position = intersections[0];
            IntersectionPoint pointScript = point.GetComponent<IntersectionPoint>();
            intersectionPoints[0] = pointScript;
            pointScript.SetRecalculate(greatCircle, smallCircle, (curve1, curve2) =>
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
            intersectionPoints[0] = pointScript;
            pointScript.Id = Guid.NewGuid();
            return intersectionPoints;
        }


        GameObject point1 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point1.transform.position = intersections[0];
        IntersectionPoint point1Script = point1.GetComponent<IntersectionPoint>();
        intersectionPoints[0] = point1Script;
        point1Script.Id = Guid.NewGuid();
        point1Script.SetRecalculate(greatCircle, smallCircle, (curve1, curve2) =>
        {
            Vector3[] intersections = CalculateIntersections(curve1 as GreatCircle, curve2 as SmallCircle);
            if (intersections.Length == 0)
            {
                return Vector3.zero;
            }
            else
            {
                return intersections[0];
            }
        });

        GameObject point2 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point2.transform.position = intersections[1];      
        IntersectionPoint point2Script = point2.GetComponent<IntersectionPoint>();
        intersectionPoints[1] = point2Script;
        point2Script.Id = Guid.NewGuid();
        point2Script.SetRecalculate(greatCircle, smallCircle, (curve1, curve2) =>
        {
            Vector3[] intersections = CalculateIntersections(curve1 as GreatCircle, curve2 as SmallCircle);
            if (intersections.Length == 0)
            {
                return Vector3.zero;
            }
            else
            {
                return intersections[1];
            }
        });

        return intersectionPoints;
    }

    public IntersectionPoint[] CreateIntersectionPoints(SmallCircle smallCircle1, SmallCircle smallCircle2)
    {
        Vector3[] intersections = CalculateIntersections(smallCircle1, smallCircle2);
        IntersectionPoint[] intersectionPoints = new IntersectionPoint[intersections.Length];

        if (intersections.Length == 0)
        {
            return new IntersectionPoint[] { };
        }

        if (intersections.Length == 1)
        {
            GameObject point = MonoBehaviour.Instantiate(IntersectionPointPrefab);
            point.transform.position = intersections[0];
            IntersectionPoint pointScript = point.GetComponent<IntersectionPoint>();
            pointScript.SetRecalculate(smallCircle1, smallCircle2, (curve1, curve2) =>
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

            intersectionPoints[0] = pointScript;
            pointScript.Id = Guid.NewGuid();
            return intersectionPoints;
        }

        GameObject point1 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point1.transform.position = intersections[0];
        IntersectionPoint point1Script = point1.GetComponent<IntersectionPoint>();
        intersectionPoints[0] = point1Script;
        point1Script.Id = Guid.NewGuid();
        point1Script.SetRecalculate(smallCircle1, smallCircle2, (curve1, curve2) =>
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


        GameObject point2 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point2.transform.position = intersections[1];
        IntersectionPoint point2Script = point2.GetComponent<IntersectionPoint>();
        intersectionPoints[1] = point2Script;
        point2Script.Id = Guid.NewGuid();
        point2Script.SetRecalculate(smallCircle1, smallCircle2, (curve1, curve2) =>
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


        return intersectionPoints;
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

        float a = d.x * d.x + d.y * d.y + d.z * d.z;
        float b = 2 * (d.x * x0 + d.y * y0 + d.z * z0);
        float c = x0 * x0 + y0 * y0 + z0 * z0 - 1;

        if ((b * b - 4 * a * c) < 0)
        {
            Debug.Log((b * b - 4 * a * c));
            Debug.Log("no intersection");
            return new Vector3[0];
        }

        float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
        float p2 = (-b - math.sqrt(b * b - 4 * a * c)) / (2 * a);

        Vector3[] possibleIntersections = {
            new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z),
            new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z)
        };

        Vector3[] intersections ={
            Vector3.zero,
            Vector3.zero
        };

        if (Vector3.Angle(greatCircleSegmentEndPoints[0], greatCircleSegmentEndPoints[1]) + epsilon >= Vector3.Angle(greatCircleSegmentEndPoints[0], possibleIntersections[0]) + Vector3.Angle(greatCircleSegmentEndPoints[1], possibleIntersections[0]))
        {
            intersections[0] = possibleIntersections[0];
        }

        if (Vector3.Angle(greatCircleSegmentEndPoints[0], greatCircleSegmentEndPoints[1]) + epsilon >= Vector3.Angle(greatCircleSegmentEndPoints[0], possibleIntersections[1]) + Vector3.Angle(greatCircleSegmentEndPoints[1], possibleIntersections[1]))
        {
            intersections[1] = possibleIntersections[1];
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

        float a = d.x * d.x + d.y * d.y + d.z * d.z;
        float b = 2 * (d.x * x0 + d.y * y0 + d.z * z0);
        float c = x0 * x0 + y0 * y0 + z0 * z0 - 1;

        if ((b * b - 4 * a * c) < 0)
        {
            Debug.Log((b * b - 4 * a * c));
            Debug.Log("no intersection");
            return new Vector3[0];
        }

        float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
        float p2 = (-b - math.sqrt(b * b - 4 * a * c)) / (2 * a);
        Vector3[] intersections = {
            new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z),
            new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z)
        };

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

        if ((b * b - 4 * a * c) < 0)
        {
            Debug.Log((b * b - 4 * a * c));
            Debug.Log("no intersection");
            return new Vector3[0];
        }

        float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
        float p2 = (-b - math.sqrt(b * b - 4 * a * c)) / (2 * a);
        Vector3[] intersections = {
            new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z),
            new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z)
        };

        return intersections;
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
