using System;
using UnityEngine;

[Serializable]
public record Vector3Dto
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}
