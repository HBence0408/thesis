using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public record SaveFile 
{
    public string FileName { get; set; }

    public AntipodalPointDto[] AntipodalPoints { get; set; }
    public GrabablePointDto[] GrabablePoints { get; set; }
    public GreatCircleDto[] GreatCircles { get; set; }
    public GreatCircleSegmentDto[] GreatCircleSegments { get; set; }
    public IntersectionPointDto[] IntersectionPoints { get; set; }
    public LimitedPointDto[] LimitedPoints { get; set; }
    public MidPointDto[] MidPoints { get; set; }
    public PolePointDto[] PolePoints { get; set; }
    public ShadowPolePointDto[] ShadowPolePoints { get; set; }
    public SmallCircleDto[] SmallCircles { get; set; }
}
