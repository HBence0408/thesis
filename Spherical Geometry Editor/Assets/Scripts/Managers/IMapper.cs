using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 public interface IMapper
 {
    AntipodalPointDto Map(AntipodalPoint point);
    GrabablePointDto Map(GrabablePoint point);
    IntersectionPointDto Map(IntersectionPoint point);
    LimitedPointDto Map(LimitedPoint point);
    MidPointDto Map(MidPoint point);
    PolePointDto Map(PolePoint point);
    ShadowPolePointDto Map(ShadowPolePoint point);
    GreatCircleDto Map(GreatCircle greatCircle);
    GreatCircleSegmentDto Map(GreatCircleSegment greatCircleSegment);
    SmallCircleDto Map(SmallCircle smallCircle);

    AntipodalPointDto[] Map(AntipodalPoint[] point);
    GrabablePointDto[] Map(GrabablePoint[] point);
    IntersectionPointDto[] Map(IntersectionPoint[] point);
    LimitedPointDto[] Map(LimitedPoint[] point);
    MidPointDto[] Map(MidPoint[] point);
    PolePointDto[] Map(PolePoint[] point);
    ShadowPolePointDto[] Map(ShadowPolePoint[] point);
    GreatCircleDto[] Map(GreatCircle[] greatCircle);
    GreatCircleSegmentDto[] Map(GreatCircleSegment[] greatCircleSegment);
    SmallCircleDto[] Map(SmallCircle[] smallCircle);
}
