using System;
using UnityEngine;

public interface IGeometryObject
{
    Guid Id { get; set; }
    bool IsActive { get; }

    void SoftDelete();
    void HardDelete();
    void Restore();
    void Highlight();
    void UnHighlight();
    void SetColor(Color color);
}

