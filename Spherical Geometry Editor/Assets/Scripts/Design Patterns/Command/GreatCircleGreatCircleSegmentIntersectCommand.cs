using UnityEngine;

public class GreatCircleGreatCircleSegmentIntersectCommand : ICommand
{
    GreatCircleSegment greatCircleSegment;
    GreatCircle greatCircle;
    GameObject prefab;
    IntersectionPoint point1Script;
    double epsilon = 0.0001;

    public GreatCircleGreatCircleSegmentIntersectCommand(GreatCircleSegment greatCircleSegment, GreatCircle greatCircle, GameObject prefab)
    {
        this.greatCircleSegment = greatCircleSegment;
        this.greatCircle = greatCircle;
        this.prefab = prefab;
    }

    public void Execute()
    {
        
        Vector3 dir = Vector3.Cross(greatCircleSegment.NormalOfPlane, greatCircle.NormalOfPlane);

        Vector3[] segment1Endpoints = greatCircleSegment.GetEndpoints();

        Debug.Log("executeing great circle great circle segment intersect command");

        if (((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], dir) + Vector3.Angle(segment1Endpoints[1], dir)) || ((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], -dir) + Vector3.Angle(segment1Endpoints[1], -dir)) )))
        {
            Debug.Log("creating point");
            GameObject point1 = MonoBehaviour.Instantiate(prefab);

            point1.transform.position = ((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], dir) + Vector3.Angle(segment1Endpoints[1], dir))) ? dir.normalized : -dir.normalized;
            point1Script = point1.GetComponent<IntersectionPoint>();
            point1Script.SetRecalculate(greatCircleSegment, greatCircle,
            (curve1, curve2) =>
            {
                Vector3 possibleIntersection = Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane);
                Vector3[] curve1Endpoints = ((GreatCircleSegment)(curve1)).GetEndpoints();
                Debug.Log(Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + " " + Vector3.Angle(curve1Endpoints[0], possibleIntersection) + " " + Vector3.Angle(possibleIntersection, curve1Endpoints[1]));
                if ((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + epsilon >= Vector3.Angle(curve1Endpoints[0], possibleIntersection) + Vector3.Angle(curve1Endpoints[1], possibleIntersection)))
                {
                    Debug.Log("positive intersection");
                    return possibleIntersection;
                }
                else if ((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + epsilon >= Vector3.Angle(curve1Endpoints[0], -possibleIntersection) + Vector3.Angle(curve1Endpoints[1], -possibleIntersection)))
                {
                    Debug.Log("negative intersection");
                    return -possibleIntersection;
                }
                else
                {
                    return new Vector3(0, 0, 0);
                }
            });

            greatCircleSegment.Subscirbe(point1Script);
            greatCircle.Subscirbe(point1Script);
        }
    }

    public void UnExecute()
    {
        point1Script.Destroy();
    }
}
