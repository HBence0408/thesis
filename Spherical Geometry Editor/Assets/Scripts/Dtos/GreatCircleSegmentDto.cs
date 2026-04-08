using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public record GreatCircleSegmentDto
{
    public Guid id { get; set; }
    public Guid EndPoint1 { get; set; }
    public Vector3Dto EndPoint1Pos { get; set; }
    public Guid EndPoint2 { get; set; }
    public Vector3Dto EndPoint2Pos { get; set; }
    public ColorDto Color { get; set; }
    public bool IsActive { get; set; }
    public List<Guid> Observers { get; set; }
}
