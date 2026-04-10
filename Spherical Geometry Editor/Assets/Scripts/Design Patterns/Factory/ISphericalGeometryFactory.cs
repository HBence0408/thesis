using UnityEngine;

public interface ISphericalGeometryFactory
{
    GrabablePoint CreateGrabablepoint(Vector3 pos);
    LimitedPoint CreateLimitedpoint(Vector3 pos);
    PolePoint CreatePolepoint(Vector3 pos);
    ShadowPolePoint CreateShadowPolepoint(Vector3 pos);
    AntipodalPoint CreateAntipodalpoint(Vector3 pos);
    MidPoint CreateMidpoint(Vector3 pos);
    GreatCircle CreateGreatCircle(Vector3 point1Pos, Vector3 point2Pos);
    SmallCircle CreateSmallCircle(Vector3 point1Pos, Vector3 point2Pos);
    GreatCircleSegment CreateGreatCircleSegment(Vector3 point1Pos, Vector3 point2Pos);
    IntersectionPoint[] CreateIntersectionPoints(GreatCircle greatCircle1, GreatCircle greatCircle2);
    IntersectionPoint[] CreateIntersectionPoints(GreatCircleSegment greatCircleSegment, GreatCircle greatCircle);
    IntersectionPoint[] CreateIntersectionPoints(GreatCircleSegment greatCircleSegment1, GreatCircleSegment greatCircleSegment2);
    IntersectionPoint[] CreateIntersectionPoints(GreatCircleSegment greatCircleSegment, SmallCircle smallCircle);
    IntersectionPoint[] CreateIntersectionPoints(GreatCircle greatCircle, SmallCircle smallCircle);
    IntersectionPoint[] CreateIntersectionPoints(SmallCircle smallCircle1, SmallCircle smallCircle2);
}
