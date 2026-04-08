using System;
using UnityEngine;

public interface IGeometryObject
{
    Guid Id { get; set; }
    bool IsActive { get; }
    Color Color { get; }

    void SoftDelete(Action<Guid> delete);
    void HardDelete();
    void Restore(Action<IGeometryObject> restore);
    void Highlight();
    void UnHighlight();
    void SetColor(Color color);
}

