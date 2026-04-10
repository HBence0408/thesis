using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public record GrabablePointDto
{
    public Guid id { get; set; }
    public Vector3Dto Position { get; set; }
    public ColorDto Color { get; set; }
}
