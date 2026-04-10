using Unity.Mathematics;
using UnityEngine;

public class SmallCircleSmallCircleIntersectCommand : ICommand
{
    SmallCircle smallCircle1;
    SmallCircle smallCircle2;
    ISphericalGeometryFactory factory;
    IntersectionPoint[] intersections;
    IRepository repository;
    bool isExecuted;

    public SmallCircleSmallCircleIntersectCommand(SmallCircle smallCircle1, SmallCircle smallCircle2, ISphericalGeometryFactory factory, IRepository repository)
    {
        this.smallCircle1 = smallCircle1;
        this.smallCircle2 = smallCircle2;
        this.factory = factory;
        this.repository = repository;
    }

    public void Execute()
    {
        intersections = factory.CreateIntersectionPoints(smallCircle1, smallCircle2);

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
        smallCircle1 = null;
        smallCircle2 = null;
        factory = null;
        intersections = null;
    }
}
