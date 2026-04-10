using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static GreatCircleGreatCircleIntersectCommand;

public class GreatCircleSegmentGreatCircleSegmentIntersectCommand : ICommand
{
    GreatCircleSegment greatCircleSegment1;
    GreatCircleSegment greatCircleSegment2;
    IntersectionPoint[] intersections;
    ISphericalGeometryFactory factory;
    IRepository repository;
    bool isExecuted;

    public GreatCircleSegmentGreatCircleSegmentIntersectCommand(GreatCircleSegment greatCircleSegment1, GreatCircleSegment greatCircleSegment2, ISphericalGeometryFactory factory, IRepository repository)
    {
        this.greatCircleSegment1 = greatCircleSegment1;
        this.greatCircleSegment2 = greatCircleSegment2;
        this.factory = factory;
        this.repository = repository;
    }

    public void Execute()
    {
       intersections = factory.CreateIntersectionPoints(greatCircleSegment1, greatCircleSegment2);

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
        greatCircleSegment1 = null;
        greatCircleSegment2 = null;
        factory = null;
        intersections = null;
    }
}
