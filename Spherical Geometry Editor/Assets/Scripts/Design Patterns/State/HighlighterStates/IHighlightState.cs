using UnityEngine;

public interface IHighlightState
{
    void Highlight(IGeometryObject geometryObject);
    void UnHighlight();
    void OnEnter();
    void OnExit();
}
