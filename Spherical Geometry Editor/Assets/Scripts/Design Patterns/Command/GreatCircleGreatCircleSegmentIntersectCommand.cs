using UnityEngine;

public class GreatCircleGreatCircleSegmentIntersectCommand : ICommand
{
    GreatCircleSegment greatCircleSegment;
    GreatCircle greatCircle;
    IntersectionPoint[] intersections;
    SphericalGeometryFactory factory;
    IRepository repository;
    bool isExecuted;

    public GreatCircleGreatCircleSegmentIntersectCommand(GreatCircleSegment greatCircleSegment, GreatCircle greatCircle, SphericalGeometryFactory factory, IRepository repository)
    {
        this.greatCircleSegment = greatCircleSegment;
        this.greatCircle = greatCircle;
        this.factory = factory;
        this.repository = repository;
    }

    public void Execute()
    {
        intersections = factory.CreateIntersectionPoints(greatCircleSegment, greatCircle);

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
