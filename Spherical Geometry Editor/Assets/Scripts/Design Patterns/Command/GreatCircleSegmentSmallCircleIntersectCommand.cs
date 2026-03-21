using Unity.Mathematics;
using UnityEngine;

public class GreatCircleSegmentSmallCircleIntersectCommand : ICommand
{
    GreatCircleSegment greatCircleSegment;
    SmallCircle smallCircle;
    IntersectionPoint[] intersections;
    SphericalGeometryFactory factory;

    public GreatCircleSegmentSmallCircleIntersectCommand(GreatCircleSegment greatCircle, SmallCircle smallCircle, SphericalGeometryFactory factory)
    {
        this.greatCircleSegment = greatCircle;
        this.smallCircle = smallCircle;
        this.factory = factory;
    }

    public void Execute()
    {
        intersections = factory.CreateIntersectionPoints(greatCircleSegment, smallCircle);

        for (int i = 0; i < intersections.Length; i++)
        {
            greatCircleSegment.Subscirbe(intersections[i]);
            smallCircle.Subscirbe(intersections[i]);
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
