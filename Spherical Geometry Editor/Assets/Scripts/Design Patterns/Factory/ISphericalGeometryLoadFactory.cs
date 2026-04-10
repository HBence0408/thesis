using UnityEngine;

public interface ISphericalGeometryLoadFactory
{
    public GrabablePoint CreateGrabablepoint(GrabablePointDto dto);
    public LimitedPoint CreateLimitedpoint(LimitedPointDto dto);
    public PolePoint CreatePolepoint(PolePointDto dto);
    public ShadowPolePoint CreateShadowPolepoint(ShadowPolePointDto dto);
    public AntipodalPoint CreateAntipodalpoint(AntipodalPointDto dto);
    public MidPoint CreateMidpoint(MidPointDto dto);
    public GreatCircle CreateGreatCircle(GreatCircleDto dto);
    public SmallCircle CreateSmallCircle(SmallCircleDto dto);
    public GreatCircleSegment CreateGreatCircleSegment(GreatCircleSegmentDto dto);
    public IntersectionPoint CreateIntersectionpoint(IntersectionPointDto dto);
}