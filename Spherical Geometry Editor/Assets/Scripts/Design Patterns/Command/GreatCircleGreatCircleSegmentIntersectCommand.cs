using UnityEngine;

public class GreatCircleGreatCircleSegmentIntersectCommand : ICommand
{
    GreatCircleSegment greatCircleSegment;
    GreatCircle greatCircle;
    IntersectionPoint[] intersections;
    SphericalGeometryFactory factory;
    bool isExecuted;

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
        greatCircle = null;
        greatCircleSegment = null;
        factory = null;
        intersections = null;
    }
}
