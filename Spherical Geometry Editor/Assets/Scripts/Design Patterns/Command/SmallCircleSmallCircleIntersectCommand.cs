using Unity.Mathematics;
using UnityEngine;

public class SmallCircleSmallCircleIntersectCommand : ICommand
{
    SmallCircle smallCircle1;
    SmallCircle smallCircle2;
    SphericalGeometryFactory factory;
    IntersectionPoint[] intersections;

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
    }


    public void UnExecute()
    {
        for (int i = 0; i < intersections.Length; i++)
        {
            intersections[i].Destroy();
        }
    }
}
