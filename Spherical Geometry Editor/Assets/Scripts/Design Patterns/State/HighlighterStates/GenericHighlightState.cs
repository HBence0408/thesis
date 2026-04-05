using UnityEngine;

public class GenericHighlightState<T> : IHighlightState
{
    private IGeometryObject previousObject;

    public void Highlight(IGeometryObject geometryObject)
    {
        if (geometryObject != previousObject && previousObject != null)
        {
            previousObject.UnHighlight();
        }
        if (geometryObject is T)
        {
            geometryObject.Highlight();
        }
        previousObject = geometryObject;
    }

    public void UnHighlight()
    {
        if (previousObject != null)
        {
            previousObject.UnHighlight();
        }
    }

    public void OnEnter()
    {
        previousObject = null;
    }

    public void OnExit()
    {
        previousObject = null;
    }
}
