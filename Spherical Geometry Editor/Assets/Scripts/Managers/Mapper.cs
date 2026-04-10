using System.Collections.Generic;
using UnityEngine;

public class Mapper : IMapper
{
    public AntipodalPointDto Map(AntipodalPoint point)
    {
        return new AntipodalPointDto
        {
            id = point.Id,
            Position = new Vector3Dto 
            { 
                X = point.Position.x, 
                Y = point.Position.y, 
                Z = point.Position.z 
            },
            Color = new ColorDto
            {
                A = point.Color.a,
                R = point.Color.r,
                G = point.Color.g,
                B = point.Color.b,
            },
            ControllPoint = point.ControllPoint
        };
    }

    public AntipodalPointDto[] Map(AntipodalPoint[] points)
    {
        List<AntipodalPointDto> antipodalPointDtos = new List<AntipodalPointDto>();
        foreach (AntipodalPoint i in points)
        {
            antipodalPointDtos.Add(Map(i));
        }

        return antipodalPointDtos.ToArray();
    }

    public GrabablePointDto Map(GrabablePoint point)
    {
        return new GrabablePointDto
        {
            id = point.Id,
            Position = new Vector3Dto
            {
                X = point.Position.x,
                Y = point.Position.y,
                Z = point.Position.z
            },
            Color = new ColorDto
            {
                A = point.Color.a,
                R = point.Color.r,
                G = point.Color.g,
                B = point.Color.b,
            },
        };
    }

    public GrabablePointDto[] Map(GrabablePoint[] points)
    {
        List<GrabablePointDto> grabablePointDtos = new List<GrabablePointDto>();
        foreach (GrabablePoint i in points)
        {
            grabablePointDtos.Add(Map(i));
        }
        return grabablePointDtos.ToArray();
    }

    public GreatCircleDto Map(GreatCircle greatCircle)
    {
        return new GreatCircleDto
        {
            id = greatCircle.Id,
            ControllPoint1 = greatCircle.ControllPoint1,
            ControllPoint1Pos = new Vector3Dto
            {
                X = greatCircle.ControllPoint1Pos.x,
                Y = greatCircle.ControllPoint1Pos.y,
                Z = greatCircle.ControllPoint1Pos.z,
            },
            ControllPoint2 = greatCircle.ControllPoint2,
            ControllPoint2Pos = new Vector3Dto
            {
                X = greatCircle.ControllPoint2Pos.x,
                Y = greatCircle.ControllPoint2Pos.y,
                Z = greatCircle.ControllPoint2Pos.z,
            },
            Color = new ColorDto
            {
                A = greatCircle.Color.a,
                R = greatCircle.Color.r,
                G = greatCircle.Color.g,
                B = greatCircle.Color.b,
            },
        };
    }

    public GreatCircleDto[] Map(GreatCircle[] greatCircles)
    {
        List<GreatCircleDto> greatCircleDtos = new List<GreatCircleDto>();
        foreach (GreatCircle i in greatCircles)
        {
            greatCircleDtos.Add(Map(i));
        }
        return greatCircleDtos.ToArray();
    }

    public GreatCircleSegmentDto Map(GreatCircleSegment greatCircleSegment)
    {
        return new GreatCircleSegmentDto
        {
            id = greatCircleSegment.Id,
            EndPoint1 = greatCircleSegment.ControllPoint1,
            EndPoint1Pos = new Vector3Dto
            {
                X = greatCircleSegment.ControllPoint1Pos.x,
                Y = greatCircleSegment.ControllPoint1Pos.y,
                Z = greatCircleSegment.ControllPoint1Pos.z,
            },
            EndPoint2 = greatCircleSegment.ControllPoint2,
            EndPoint2Pos = new Vector3Dto
            {
                X = greatCircleSegment.ControllPoint2Pos.x,
                Y = greatCircleSegment.ControllPoint2Pos.y,
                Z = greatCircleSegment.ControllPoint2Pos.z,
            },
            Color = new ColorDto
            {
                A = greatCircleSegment.Color.a,
                R = greatCircleSegment.Color.r,
                G = greatCircleSegment.Color.g,
                B = greatCircleSegment.Color.b,
            },
        };
    }

    public GreatCircleSegmentDto[] Map(GreatCircleSegment[] greatCircleSegments)
    {
        List<GreatCircleSegmentDto> greatCircleSegmentDtos = new List<GreatCircleSegmentDto>();
        foreach (GreatCircleSegment i in greatCircleSegments)
        {
            greatCircleSegmentDtos.Add(Map(i));
        }
        return greatCircleSegmentDtos.ToArray();
    }

    public IntersectionPointDto Map(IntersectionPoint point)
    {
        return new IntersectionPointDto
        {
            id = point.Id,
            Position = new Vector3Dto
            {
                X = point.Position.x,
                Y = point.Position.y,
                Z = point.Position.z
            },
            Color = new ColorDto
            {
                A = point.Color.a,
                R = point.Color.r,
                G = point.Color.g,
                B = point.Color.b,
            },
            Curve1 = point.Curve1,
            Curve2 = point.Curve2,
            IntersectionType = point.IntersectionType,
        };
    }

    public IntersectionPointDto[] Map(IntersectionPoint[] points)
    {
        List<IntersectionPointDto> intersectionPointDtos = new List<IntersectionPointDto>();
        foreach (IntersectionPoint i in points)
        {
            intersectionPointDtos.Add(Map(i));
        }
        return intersectionPointDtos.ToArray();
    }

    public LimitedPointDto Map(LimitedPoint point)
    {
        return new LimitedPointDto
        {
            id = point.Id,
            Position = new Vector3Dto
            {
                X = point.Position.x,
                Y = point.Position.y,
                Z = point.Position.z
            },
            Color = new ColorDto
            {
                A = point.Color.a,
                R = point.Color.r,
                G = point.Color.g,
                B = point.Color.b,
            },
            Curve = point.Curve 
        };
    }

    public LimitedPointDto[] Map(LimitedPoint[] points)
    {
        List<LimitedPointDto> limitedPointDtos = new List<LimitedPointDto>();
        foreach (LimitedPoint i in points)
        {
            limitedPointDtos.Add(Map(i));
        }
        return limitedPointDtos.ToArray();
    }

    public MidPointDto Map(MidPoint point)
    {
        return new MidPointDto
        {
            id = point.Id,
            Position = new Vector3Dto
            {
                X = point.Position.x,
                Y = point.Position.y,
                Z = point.Position.z
            },
            Color = new ColorDto
            {
                A = point.Color.a,
                R = point.Color.r,
                G = point.Color.g,
                B = point.Color.b,
            },
            ControllPoint1 = point.ControllPoint1,
            ControllPoint2 = point.ControllPoint2
        };
    }

    public MidPointDto[] Map(MidPoint[] points)
    {
        List<MidPointDto> midPointDtos = new List<MidPointDto>();
        foreach (MidPoint i in points)
        {
            midPointDtos.Add(Map(i));
        }
        return midPointDtos.ToArray();
    }

    public PolePointDto Map(PolePoint point)
    {
        return new PolePointDto
        {
            id = point.Id,
            Position = new Vector3Dto
            {
                X = point.Position.x,
                Y = point.Position.y,
                Z = point.Position.z
            },
            Color = new ColorDto
            {
                A = point.Color.a,
                R = point.Color.r,
                G = point.Color.g,
                B = point.Color.b,
            },
            Curve = point.Curve,
            sign = point.Sign
        };
    }

    public PolePointDto[] Map(PolePoint[] points)
    {
        List<PolePointDto> polePointDtos = new List<PolePointDto>();
        foreach (PolePoint i in points)
        {
            polePointDtos.Add(Map(i));
        }
        return polePointDtos.ToArray();
    }

    public ShadowPolePointDto Map(ShadowPolePoint point) 
    {
        return new ShadowPolePointDto
        {
            id = point.Id,
            Position = new Vector3Dto
            {
                X = point.Position.x,
                Y = point.Position.y,
                Z = point.Position.z
            },
            Color = new ColorDto
            {
                A = point.Color.a,
                R = point.Color.r,
                G = point.Color.g,
                B = point.Color.b,
            },
            Curve = point.Curve,
            sign = point.Sign
        };
    }

    public ShadowPolePointDto[] Map(ShadowPolePoint[] points)
    {
        List<ShadowPolePointDto> shadowPolePointDtos = new List<ShadowPolePointDto>();
        foreach (ShadowPolePoint i in points)
        {
            shadowPolePointDtos.Add(Map(i));
        }
        return shadowPolePointDtos.ToArray();
    }

    public SmallCircleDto Map(SmallCircle smallCircle)
    {
        return new SmallCircleDto
        {
            id = smallCircle.Id,
            ControllPoint1 = smallCircle.ControllPoint1,
            ControllPoint1Pos = new Vector3Dto
            {
                X = smallCircle.ControllPoint1Pos.x,
                Y = smallCircle.ControllPoint1Pos.y,
                Z = smallCircle.ControllPoint1Pos.z,
            },
            ControllPoint2 = smallCircle.ControllPoint2,
            ControllPoint2Pos = new Vector3Dto
            {
                X = smallCircle.ControllPoint2Pos.x,
                Y = smallCircle.ControllPoint2Pos.y,
                Z = smallCircle.ControllPoint2Pos.z,
            },
            Color = new ColorDto
            {
                A = smallCircle.Color.a,
                R = smallCircle.Color.r,
                G = smallCircle.Color.g,
                B = smallCircle.Color.b,
            },
        };
    }

    public SmallCircleDto[] Map(SmallCircle[] smallCircles)
    {
        List<SmallCircleDto> smallCircleDtos = new List<SmallCircleDto>();
        foreach (SmallCircle i in smallCircles)
        {
            smallCircleDtos.Add(Map(i));
        }
        return smallCircleDtos.ToArray();
    }
}
