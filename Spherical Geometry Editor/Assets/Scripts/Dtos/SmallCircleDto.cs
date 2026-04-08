using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public record SmallCircleDto
{
    public Guid id { get; set; }
    public Guid ControllPoint1 { get; set; }
    public Vector3Dto ControllPoint1Pos { get; set; }
    public Guid ControllPoint2 { get; set; }
    public Vector3Dto ControllPoint2Pos { get; set; }
    public ColorDto Color { get; set; }
    public bool IsActive { get; set; }
    public List<Guid> Observers { get; set; }
}
