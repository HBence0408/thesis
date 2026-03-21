using UnityEngine;

public class GreatCircleGreatCircleSegmentIntersectCommand : ICommand
{
    GreatCircleSegment greatCircleSegment;
    GreatCircle greatCircle;
    IntersectionPoint[] intersections;
    SphericalGeometryFactory factory;

    public GreatCircleGreatCircleSegmentIntersectCommand(GreatCircleSegment greatCircleSegment, GreatCircle greatCircle, SphericalGeometryFactory factory)
    {
        this.greatCircleSegment = greatCircleSegment;
        this.greatCircle = greatCircle;
        this.factory = factory;
    }

    public void Execute()
    {
        intersections = factory.CreateIntersectionPoints(greatCircleSegment, greatCircle);

        for (int i = 0; i < intersections.Length; i++)
        {
            greatCircleSegment.Subscirbe(intersections[i]);
            greatCircle.Subscirbe(intersections[i]);
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
