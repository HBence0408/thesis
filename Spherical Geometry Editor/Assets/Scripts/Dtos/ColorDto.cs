using System;
using UnityEngine;

[Serializable]
public record ColorDto
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }
}
