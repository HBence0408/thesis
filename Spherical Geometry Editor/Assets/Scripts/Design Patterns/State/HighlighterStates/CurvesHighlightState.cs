using UnityEngine;

public class CurvesHighlightState : IHighlightState
{
    private IGeometryObject previousObject;

    public void Highlight(IGeometryObject geometryObject)
    {
        if (geometryObject != previousObject)
        {
            previousObject.UnHighlight();
        }
        if (geometryObject is ControllPoint)
        {
            geometryObject.Highlight();
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

    public void UnHighlight()
    {
        throw new System.NotImplementedException();
    }
}
