using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public record AntipodalPointDto 
{
    public Guid id { get; set; }
    public Vector3Dto Position { get; set; }
    public ColorDto Color { get; set; }
    public Guid ControllPoint { get; set; }
}
