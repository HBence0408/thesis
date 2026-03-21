using Unity.Mathematics;
using UnityEngine;

public class GreatCircleSmallCircleIntersectCommand : ICommand
{
    GreatCircle greatCircle;
    SmallCircle smallCircle;
    SphericalGeometryFactory factory;
    IntersectionPoint point1Script;
    IntersectionPoint point2Script;

    public GreatCircleSmallCircleIntersectCommand(GreatCircle greatCircle, SmallCircle smallCircle, SphericalGeometryFactory factory)
    {
        this.greatCircle = greatCircle;
        this.smallCircle = smallCircle;
        this.factory = factory;
    }

    public void Execute()
    {
        IntersectionPoint[] intersections = factory.CreateIntersectionPoints(greatCircle, smallCircle);

        for (int i = 0; i < intersections.Length; i++)
        {
            smallCircle.Subscirbe(intersections[i]);
            greatCircle.Subscirbe(intersections[i]);
            Debug.Log(intersections[i]);
        }
    }

    public void UnExecute()
    {
        point1Script.Destroy();
        point2Script.Destroy();
    }
}
