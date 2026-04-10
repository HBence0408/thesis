using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface ILinker
    {
    void Link(LimitedPoint limitedPoint, LimitedPointDto dto);
    void Link(AntipodalPoint antipodalPoint, AntipodalPointDto dto);
    void Link(MidPoint midPoint, MidPointDto dto);
    void Link(PolePoint polePoint, PolePointDto dto);
    void Link(ShadowPolePoint shadowPolePoint, ShadowPolePointDto dto);
    void Link(IntersectionPoint intersectionPoint, IntersectionPointDto dto);
    void Link(GreatCircle greatCircle, GreatCircleDto dto);
    void Link(SmallCircle smallCircle, SmallCircleDto dto);
    void Link(GreatCircleSegment greatCircleSegment, GreatCircleSegmentDto dto);
}

