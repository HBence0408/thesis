using Unity.Mathematics;
using UnityEngine;

public class GreatCircleSegmentSmallCircleIntersectCommand : ICommand
{
    GreatCircleSegment greatCircleSegment;
    SmallCircle smallCircle;
    IntersectionPoint[] intersections;
    SphericalGeometryFactory factory;
    IRepository repository;
    bool isExecuted;

    public GreatCircleSegmentSmallCircleIntersectCommand(GreatCircleSegment greatCircle, SmallCircle smallCircle, SphericalGeometryFactory factory, IRepository repository)
    {
        this.greatCircleSegment = greatCircle;
        this.smallCircle = smallCircle;
        this.factory = factory;
        this.repository = repository;
    }

    public void Execute()
    {
        intersections = factory.CreateIntersectionPoints(greatCircleSegment, smallCircle);

        for (int i = 0; i < intersections.Length; i++)
        {
            repository.Store(intersections[i]);
        }
        isExecuted = true;
    }

    public void ReExecute()
    {
        for (int i = 0; i < intersections.Length; i++)
        {
            intersections[i].Restore(repository.Store);
            repository.Store(intersections[i]);
        }
        isExecuted = true;
    }

    public void UnExecute()
    {
        for (int i = 0; i < intersections.Length; i++)
        {
            intersections[i].SoftDelete(repository.Delete);
            repository.Delete(intersections[i].Id);
        }
        isExecuted= false;
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
        greatCircleSegment = null;
        smallCircle = null;
        factory = null;
        intersections = null;
    }
}
