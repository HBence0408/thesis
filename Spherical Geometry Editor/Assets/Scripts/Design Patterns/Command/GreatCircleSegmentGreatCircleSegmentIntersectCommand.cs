using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static GreatCircleGreatCircleIntersectCommand;

public class GreatCircleSegmentGreatCircleSegmentIntersectCommand : ICommand
{
    GreatCircleSegment greatCircleSegment1;
    GreatCircleSegment greatCircleSegment2;
    IntersectionPoint[] intersections;
    SphericalGeometryFactory factory;

    public GreatCircleSegmentGreatCircleSegmentIntersectCommand(GreatCircleSegment greatCircleSegment1, GreatCircleSegment greatCircleSegment2, SphericalGeometryFactory factory)
    {
        this.greatCircleSegment1 = greatCircleSegment1;
        this.greatCircleSegment2 = greatCircleSegment2;
        this.factory = factory;
    }

    public void Execute()
    {
        

       intersections = factory.CreateIntersectionPoints(greatCircleSegment1, greatCircleSegment2);

       for (int i = 0; i < intersections.Length; i++)
       {
           greatCircleSegment1.Subscirbe(intersections[i]);
           greatCircleSegment2.Subscirbe(intersections[i]);
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
