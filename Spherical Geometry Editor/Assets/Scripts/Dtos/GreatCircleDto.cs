using System;
using System.Collections.Generic;
using UnityEngine;

public record GreatCircleDto 
{
    public Guid id { get; set; }
    public Guid ControllPoint1 { get; set; }
    public Vector3 ControllPoint1Pos { get; set; }
    public Guid ControllPoint2 { get; set; }
    public Vector3 ControllPoint2Pos { get; set; }
    public Color Color { get; set; }
    public bool IsActive { get; set; }
    public List<Guid> Observers { get; set; }
}
