using Unity.Mathematics;
using UnityEngine;

public class GreatCircleSmallCircleIntersectCommand : ICommand
{
    GreatCircle greatCircle;
    SmallCircle smallCircle;
    SphericalGeometryFactory factory;
    IntersectionPoint[] intersections;
    IRepository repository;
    bool isExecuted;

    public GreatCircleSmallCircleIntersectCommand(GreatCircle greatCircle, SmallCircle smallCircle, SphericalGeometryFactory factory, IRepository repository)
    {
        this.greatCircle = greatCircle;
        this.smallCircle = smallCircle;
        this.factory = factory;
        this.repository = repository;
    }

    public void Execute()
    {
        intersections = factory.CreateIntersectionPoints(greatCircle, smallCircle);

        for (int i = 0; i < intersections.Length; i++)
        {
            smallCircle.Subscirbe(intersections[i]);
            greatCircle.Subscirbe(intersections[i]);
            repository.Store(intersections[i]);
        }
        isExecuted = true;
    }

    public void ReExecute()
    {
        for (int i = 0; i < intersections.Length; i++)
        {
            intersections[i].Restore();
            repository.Store(intersections[i]);
        }
        isExecuted = true;
    }

    public void UnExecute()
    {
        for (int i = 0; i < intersections.Length; i++)
        {
            intersections[i].SoftDelete();
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
        smallCircle = null;
        greatCircle = null;
        factory = null;
        intersections = null;
    }
}
