using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SphericalGeometryFactory : ISphericalGeometryFactory
{
    GameObject GrabablePointPrefab;
    GameObject IntersectionPointPrefab;
    GameObject SmallCirclePrefab;
    GameObject GreatCirclePrefab;
    GameObject GreatCircleSegmentPrefab;
    GameObject LimitedPointPrefab;
    GameObject AntipodalPointPrefab;
    GameObject PolePointPrefab;
    GameObject ShadowPolePointPrefab;
    GameObject MidPointPrefab;
    IParametricCurveMeshGenerator meshGenerator;
    IIntersectionCalculater intersectionCalculater;
    double epsilon = 0.0001;

    public SphericalGeometryFactory(IIntersectionCalculater intersectionCalculater, IParametricCurveMeshGenerator meshGenerator, GameObject grabablePointPrefab, GameObject intersectionPointPrefab, GameObject smallCirclePrefab, GameObject greatCirclePrefab, GameObject greatCircleSegmentPrefab, GameObject limitedPointPrefab, GameObject antipodalPointPrefab, GameObject polePointPrefab, GameObject midPointPrefab, GameObject shadowPolePointPrefab)
    {
        this.GrabablePointPrefab = grabablePointPrefab;
        this.IntersectionPointPrefab = intersectionPointPrefab;
        this.SmallCirclePrefab = smallCirclePrefab;
        this.GreatCirclePrefab = greatCirclePrefab;
        this.GreatCircleSegmentPrefab = greatCircleSegmentPrefab;
        this.LimitedPointPrefab = limitedPointPrefab;
        this.AntipodalPointPrefab = antipodalPointPrefab;
        this.PolePointPrefab = polePointPrefab;
        this.ShadowPolePointPrefab = shadowPolePointPrefab;
        this.MidPointPrefab = midPointPrefab;
        this.meshGenerator = meshGenerator;
        this.intersectionCalculater = intersectionCalculater;
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

    public PolePoint CreatePolepoint(Vector3 pos)
    {
        GameObject PolePoint = MonoBehaviour.Instantiate(PolePointPrefab);
        PolePoint.transform.position = pos;
        PolePoint script = PolePoint.GetComponent<PolePoint>();
        script.Id = Guid.NewGuid();
        return script;
    }

    public ShadowPolePoint CreateShadowPolepoint(Vector3 pos)
    {
        GameObject ShadowPolePoint = MonoBehaviour.Instantiate(ShadowPolePointPrefab);
        ShadowPolePoint.transform.position = pos;
        ShadowPolePoint script = ShadowPolePoint.GetComponent<ShadowPolePoint>();
        script.Id = Guid.NewGuid();
        return script;
    }

    public AntipodalPoint CreateAntipodalpoint(Vector3 pos)
    {
        GameObject antipodalPoint = MonoBehaviour.Instantiate(AntipodalPointPrefab);
        antipodalPoint.transform.position = pos;
        AntipodalPoint script = antipodalPoint.GetComponent<AntipodalPoint>();
        script.Id = Guid.NewGuid();
        return script;
    }

    public MidPoint CreateMidpoint(Vector3 pos)
    {
        GameObject midPoint = MonoBehaviour.Instantiate(MidPointPrefab);
        midPoint.transform.position = pos;
        MidPoint script = midPoint.GetComponent<MidPoint>();
        script.Id = Guid.NewGuid();
        return script;
    }

    public GreatCircle CreateGreatCircle(Vector3 point1Pos, Vector3 point2Pos)
    {
        GameObject greatCircle = MonoBehaviour.Instantiate(GreatCirclePrefab);
        greatCircle.transform.position = new Vector3(0,0,0);
        GreatCircle script = greatCircle.GetComponent<GreatCircle>();
        meshGenerator.CreateGreatCircleMesh(point1Pos, point2Pos, script.CreateMesh);
        script.SetMeshGenerator(meshGenerator);
        script.Id = Guid.NewGuid();
        return script;
    }

    public SmallCircle CreateSmallCircle(Vector3 point1Pos, Vector3 point2Pos)
    {
        GameObject smallCircle = MonoBehaviour.Instantiate(SmallCirclePrefab);
        smallCircle.transform.position = new Vector3(0, 0, 0);
        SmallCircle script = smallCircle.GetComponent<SmallCircle>();
        meshGenerator.CreateSmallCircleMesh(point1Pos, point2Pos, script.CreateMesh);
        script.SetMeshGenerator(meshGenerator);
        script.Id = Guid.NewGuid();
        return script;
    }

    public GreatCircleSegment CreateGreatCircleSegment(Vector3 point1Pos, Vector3 point2Pos)
    {
        GameObject greatCircleSegment = MonoBehaviour.Instantiate(GreatCircleSegmentPrefab);
        greatCircleSegment.transform.position = new Vector3(0, 0, 0);
        GreatCircleSegment script = greatCircleSegment.GetComponent<GreatCircleSegment>();
        meshGenerator.CreateGreatCircleSegmentMesh(point1Pos, point2Pos, script.CreateMesh);
        script.SetMeshGenerator(meshGenerator);
        script.Id = Guid.NewGuid();
        return script;
    }

    public IntersectionPoint[] CreateIntersectionPoints(GreatCircle greatCircle1, GreatCircle greatCircle2)
    {
        Vector3 dir = Vector3.Cross(greatCircle1.NormalOfPlane, greatCircle2.NormalOfPlane);
        GameObject point1 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point1.transform.position = dir.normalized;
        IntersectionPoint point1Script = point1.GetComponent<IntersectionPoint>();
        point1Script.SetRecalculate(IntersectionType.GREATCIRCLE_GREATCIRCLE_A, greatCircle1, greatCircle2, (curve1, curve2) => Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane));
        point1Script.Id = Guid.NewGuid();

        GameObject point2 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point2.transform.position = -dir.normalized;
        IntersectionPoint point2Script = point2.GetComponent<IntersectionPoint>();
        point2Script.SetRecalculate(IntersectionType.GREATCIRCLE_GREATCIRCLE_B, greatCircle1, greatCircle2, (curve1, curve2) => -Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane));
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
            script.SetRecalculate(IntersectionType.GREATCIRCLE_GREATCIRCLESEGMENT, greatCircleSegment, greatCircle,
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
            script.SetRecalculate(IntersectionType.GREATCIRCLESEGMENT_GREATCIRCLESEGMENT, greatCircleSegment1, greatCircleSegment2,
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
        Vector3[] intersections = intersectionCalculater.CalculateIntersections(greatCircleSegment, smallCircle);
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
            point1Script.SetRecalculate(IntersectionType.SMALLCIRCLE_GREATCIRCLESEGMENT_A, greatCircleSegment, smallCircle, (curve1, curve2) =>
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
            point1Script.SetRecalculate(IntersectionType.SMALLCIRCLE_GREATCIRCLESEGMENT_A, greatCircleSegment, smallCircle, (curve1, curve2) =>
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
        }

        if (intersections[1] != Vector3.zero)
        {
            GameObject point2 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
            point2.transform.position = intersections[1];
            IntersectionPoint point2Script = point2.GetComponent<IntersectionPoint>();
            intersectionPoints[1] = point2Script;
            point2Script.Id = Guid.NewGuid();
            point2Script.SetRecalculate(IntersectionType.SMALLCIRCLE_GREATCIRCLESEGMENT_B, greatCircleSegment, smallCircle, (curve1, curve2) =>
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
        }
        return intersectionPoints;
    }

    public IntersectionPoint[] CreateIntersectionPoints(GreatCircle greatCircle, SmallCircle smallCircle)
    {
        Vector3[] intersections = intersectionCalculater.CalculateIntersections(greatCircle, smallCircle);
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
            pointScript.SetRecalculate(IntersectionType.SMALLCIRCLE_GREATCIRCLE_A, greatCircle, smallCircle, (curve1, curve2) =>
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
            intersectionPoints[0] = pointScript;
            pointScript.Id = Guid.NewGuid();
            return intersectionPoints;
        }


        GameObject point1 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point1.transform.position = intersections[0];
        IntersectionPoint point1Script = point1.GetComponent<IntersectionPoint>();
        intersectionPoints[0] = point1Script;
        point1Script.Id = Guid.NewGuid();
        point1Script.SetRecalculate(IntersectionType.SMALLCIRCLE_GREATCIRCLE_A, greatCircle, smallCircle, (curve1, curve2) =>
        {
            Vector3[] intersections = intersectionCalculater.CalculateIntersections(curve1 as GreatCircle, curve2 as SmallCircle);
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
        point2Script.SetRecalculate(IntersectionType.SMALLCIRCLE_GREATCIRCLE_B, greatCircle, smallCircle, (curve1, curve2) =>
        {
            Vector3[] intersections = intersectionCalculater.CalculateIntersections(curve1 as GreatCircle, curve2 as SmallCircle);
            if (intersections.Length == 0)
            {
                return Vector3.zero;
            }
            else if(intersections.Length == 2)
            {
                return intersections[1];
            }
            else
            {
                return intersections[0];
            }
        });

        return intersectionPoints;
    }

    public IntersectionPoint[] CreateIntersectionPoints(SmallCircle smallCircle1, SmallCircle smallCircle2)
    {
        Vector3[] intersections = intersectionCalculater.CalculateIntersections(smallCircle1, smallCircle2);
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
            pointScript.SetRecalculate(IntersectionType.SMALLCIRCLE_SMALLCIRCLE_A, smallCircle1, smallCircle2, (curve1, curve2) =>
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

            intersectionPoints[0] = pointScript;
            pointScript.Id = Guid.NewGuid();
            return intersectionPoints;
        }

        GameObject point1 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point1.transform.position = intersections[0];
        IntersectionPoint point1Script = point1.GetComponent<IntersectionPoint>();
        intersectionPoints[0] = point1Script;
        point1Script.Id = Guid.NewGuid();
        point1Script.SetRecalculate(IntersectionType.SMALLCIRCLE_SMALLCIRCLE_A, smallCircle1, smallCircle2, (curve1, curve2) =>
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


        GameObject point2 = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        point2.transform.position = intersections[1];
        IntersectionPoint point2Script = point2.GetComponent<IntersectionPoint>();
        intersectionPoints[1] = point2Script;
        point2Script.Id = Guid.NewGuid();
        point2Script.SetRecalculate(IntersectionType.SMALLCIRCLE_SMALLCIRCLE_B, smallCircle1, smallCircle2, (curve1, curve2) =>
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

        return intersectionPoints;
    }
}
