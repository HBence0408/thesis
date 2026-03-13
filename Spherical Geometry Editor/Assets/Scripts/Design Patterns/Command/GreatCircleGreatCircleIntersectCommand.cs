using System;
using UnityEngine;

public class GreatCircleGreatCircleIntersectCommand : ICommand
{
    GreatCircle greatCircle1;
    GreatCircle greatCircle2;
    GameObject prefab;
    IntersectionPoint point1Script;
    IntersectionPoint point2Script;

    public GreatCircleGreatCircleIntersectCommand(GreatCircle greatCircle1, GreatCircle greatCircle2, GameObject prefab)
    {
        this.greatCircle1 = greatCircle1;
        this.greatCircle2 = greatCircle2;
        this.prefab = prefab;
    }

    public void Execute()
    {
        Vector3 dir = Vector3.Cross(greatCircle1.NormalOfPlane, greatCircle2.NormalOfPlane);
        GameObject point1 = MonoBehaviour.Instantiate(prefab);
        point1.transform.position = dir.normalized;
        point1Script = point1.GetComponent<IntersectionPoint>();
        point1Script.SetRecalculate(greatCircle1, greatCircle2, (curve1, curve2) => Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane));
        greatCircle1.Subscirbe(point1Script);
        greatCircle2.Subscirbe(point1Script);
        GameObject point2 = MonoBehaviour.Instantiate(prefab);
        point2.transform.position = -dir.normalized;
        point2Script = point2.GetComponent<IntersectionPoint>();
        point2Script.SetRecalculate(greatCircle1, greatCircle2, (curve1, curve2) => -Vector3.Cross(curve1.NormalOfPlane, curve2.NormalOfPlane));
        greatCircle1.Subscirbe(point2Script);
        greatCircle2.Subscirbe(point2Script);
    }

    public void UnExecute()
    {
        point1Script.Destroy();
        point2Script.Destroy();
    }
}
