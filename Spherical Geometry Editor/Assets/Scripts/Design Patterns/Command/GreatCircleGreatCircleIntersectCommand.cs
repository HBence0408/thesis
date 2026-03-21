using System;
using UnityEngine;

public class GreatCircleGreatCircleIntersectCommand : ICommand
{
    GreatCircle greatCircle1;
    GreatCircle greatCircle2;
    SphericalGeometryFactory factory;
    IntersectionPoint[] intersections;

    public GreatCircleGreatCircleIntersectCommand(GreatCircle greatCircle1, GreatCircle greatCircle2, SphericalGeometryFactory factory)
    {
        this.greatCircle1 = greatCircle1;
        this.greatCircle2 = greatCircle2;
        this.factory = factory;
    }

    public void Execute()
    {
        intersections = factory.CreateIntersectionPoints(greatCircle1, greatCircle2);

        for (int i = 0; i < intersections.Length; i++)
        {
            greatCircle1.Subscirbe(intersections[i]);
            greatCircle2.Subscirbe(intersections[i]);
        }
    }

    public void UnExecute()
    {
        for (int i = 0; i < intersections.Length; i++)
        {
            intersections[i].Destroy();
        }
    }
}
