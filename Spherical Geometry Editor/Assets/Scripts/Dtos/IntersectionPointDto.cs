using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public record IntersectionPointDto
{
    public Guid id { get; set; }
    public Vector3Dto Position { get; set; }
    public ColorDto Color { get; set; }
    public bool IsActive { get; set; }
    public List<Guid> Observers { get; set; }
    public Guid Curve1 { get; set; }
    public Guid Curve2 { get; set; }
  //  public string IntersectionType { get; set; }
}
