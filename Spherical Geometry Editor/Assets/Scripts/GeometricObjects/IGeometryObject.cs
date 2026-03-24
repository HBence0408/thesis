using System;
using UnityEngine;

public interface IGeometryObject
{
    Guid Id { get; set; }
    bool IsActive { get; }

    void SoftDelete();
    void HardDelete();
    void Restore();
}

