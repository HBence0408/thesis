using UnityEngine;

public interface IIntersectionCalculater 
{
    public Vector3[] CalculateIntersections(GreatCircleSegment greatCircleSegment, SmallCircle smallCircle);
    public Vector3[] CalculateIntersections(GreatCircle greatCircle, SmallCircle smallCircle);
    public Vector3[] CalculateIntersections(SmallCircle smallCircle1, SmallCircle smallCircle2);
}
