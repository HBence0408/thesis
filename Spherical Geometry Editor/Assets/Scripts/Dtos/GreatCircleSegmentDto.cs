using System;
using System.Collections.Generic;
using UnityEngine;

public record GreatCircleSegmentDto : MonoBehaviour
{
    public Guid id { get; set; }
    public Guid EndPoint1 { get; set; }
    public Vector3 EndPoint1Pos { get; set; }
    public Guid EndPoint2 { get; set; }
    public Vector3 EndPoint2Pos { get; set; }
    public Color Color { get; set; }
    public bool IsActive { get; set; }
    public List<Guid> Observers { get; set; }
}
