using System;
using System.Collections.Generic;
using UnityEngine;

public record AntipodalPointDto 
{
    public Guid id { get; set; }
    public Vector3 Position { get; set; }
    public Color Color { get; set; }
    public bool IsActive { get; set; }
    public List<Guid> Observers { get; set; }
    public Guid ControllPoint { get; set; }
}
