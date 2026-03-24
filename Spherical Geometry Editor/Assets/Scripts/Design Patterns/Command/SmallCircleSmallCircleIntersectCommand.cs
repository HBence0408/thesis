using Unity.Mathematics;
using UnityEngine;

public class SmallCircleSmallCircleIntersectCommand : ICommand
{
    SmallCircle smallCircle1;
    SmallCircle smallCircle2;
    SphericalGeometryFactory factory;
    IntersectionPoint[] intersections;
    bool isExecuted;

    public SmallCircleSmallCircleIntersectCommand(SmallCircle smallCircle1, SmallCircle smallCircle2, SphericalGeometryFactory factory)
    {
        this.smallCircle1 = smallCircle1;
        this.smallCircle2 = smallCircle2;
        this.factory = factory;
    }

    public void Execute()
    {
        intersections = factory.CreateIntersectionPoints(smallCircle1, smallCircle2);

        for (int i = 0; i < intersections.Length; i++)
        {
            smallCircle1.Subscirbe(intersections[i]);
            smallCircle2.Subscirbe(intersections[i]);
        }
        isExecuted = true;
    }

    public void ReExecute()
    {
        for (int i = 0; i < intersections.Length; i++)
        {
            intersections[i].Restore();
        }
        isExecuted = true;
    }

    public void UnExecute()
    {
        for (int i = 0; i < intersections.Length; i++)
        {
            intersections[i].SoftDelete();
        }
        isExecuted = false;
    }

    public void Delete()
    {
        if (!isExecuted)
        {
            for (int i = 0; i < intersections.Length; i++)
            {
                intersections[i].HardDelete();
            }
        }
        smallCircle1 = null;
        smallCircle2 = null;
        factory = null;
        intersections = null;
    }
}
