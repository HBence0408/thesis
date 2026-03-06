using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static GreatCircleGreatCircleIntersectCommand;

public class GreatCircleSegmentGreatCircleSegmentIntersectCommand : ICommand
{
    IntersectDrawState.CreatePointDelegate createPoint;
    GreatCircleSegment greatCircleSegment1;
    GreatCircleSegment greatCircleSegment2;
    GameObject prefab;
    IntersectionPoint point1Script;
    double epsilon = 0.0001;
    //IntersectionPoint point2Script;

    public GreatCircleSegmentGreatCircleSegmentIntersectCommand(GreatCircleSegment greatCircleSegment1, GreatCircleSegment greatCircleSegment2, IntersectDrawState.CreatePointDelegate createPoint, GameObject prefab)
    {
        this.createPoint = createPoint;
        this.greatCircleSegment1 = greatCircleSegment1;
        this.greatCircleSegment2 = greatCircleSegment2;
        this.prefab = prefab;
    }

    public void Execute()
    {
        Debug.Log("executein segment intersection");
        Vector3 dir = Vector3.Cross(greatCircleSegment1.NormalOfPlane, greatCircleSegment2.NormalOfPlane);

        Vector3[] segment1Endpoints = greatCircleSegment1.GetEndpoints();
        Vector3[] segment2Endpoints = greatCircleSegment2.GetEndpoints();

        if (((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], dir) + Vector3.Angle(segment1Endpoints[1], dir)) && (Vector3.Angle(segment2Endpoints[0], segment2Endpoints[1]) + epsilon >= Vector3.Angle(segment2Endpoints[0], dir) + Vector3.Angle(segment2Endpoints[1], dir))) || ((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], -dir) + Vector3.Angle(segment1Endpoints[1], -dir)) && (Vector3.Angle(segment2Endpoints[0], segment2Endpoints[1]) + epsilon >= Vector3.Angle(segment2Endpoints[0], -dir) + Vector3.Angle(segment2Endpoints[1], -dir))))
        { 
            GameObject point1 = createPoint(prefab);
            

            point1.transform.position = ((Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) + epsilon >= Vector3.Angle(segment1Endpoints[0], dir) + Vector3.Angle(segment1Endpoints[1], dir)) && (Vector3.Angle(segment2Endpoints[0], segment2Endpoints[1]) + epsilon >= Vector3.Angle(segment2Endpoints[0], dir) + Vector3.Angle(segment2Endpoints[1], dir))) ? dir.normalized : -dir.normalized;
            point1Script = point1.GetComponent<IntersectionPoint>();
            point1Script.SetRecalculate(greatCircleSegment1, greatCircleSegment2,
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
                else if((Vector3.Angle(curve1Endpoints[0], curve1Endpoints[1]) + epsilon >= Vector3.Angle(curve1Endpoints[0], -possibleIntersection) + Vector3.Angle(curve1Endpoints[1], -possibleIntersection)) && (Vector3.Angle(curve2Endpoints[0], curve2Endpoints[1]) + epsilon >= Vector3.Angle(curve2Endpoints[0], -possibleIntersection) + Vector3.Angle(curve2Endpoints[1], -possibleIntersection)))
                {
                    return -possibleIntersection;
                } 
                else
                {
                    return new Vector3(0, 0, 0);
                }
            });

            greatCircleSegment1.Subscirbe(point1Script);
            greatCircleSegment2.Subscirbe(point1Script);
        }

        //if (Vector3.Angle(segment1Endpoints[0], segment1Endpoints[1]) >= Vector3.Angle(segment1Endpoints[0], -dir))
        //{

        //}

        //GameObject point1 = createPoint(prefab);
        //point1.transform.position = dir.normalized;
        //point1Script = point1.GetComponent<IntersectionPoint>();
        //point1Script.SetRecalculate(greatCircle1, greatCircle2, (curve1, curve2) => Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane));
        //greatCircle1.Subscirbe(point1Script);
        //greatCircle2.Subscirbe(point1Script);


        //GameObject point2 = createPoint(prefab);
        //point2.transform.position = -dir.normalized;
        //point2Script = point2.GetComponent<IntersectionPoint>();
        //point2Script.SetRecalculate(greatCircle1, greatCircle2, (curve1, curve2) => -Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane));
        //greatCircle1.Subscirbe(point2Script);
        //greatCircle2.Subscirbe(point2Script);
    }

    public void UnExecute()
    {
        point1Script.Destroy();
    }
}
