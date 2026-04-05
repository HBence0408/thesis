using UnityEngine;

public class Highlighter
{
    private IHighlightState currentState;
    private GenericHighlightState<ControllPoint> controllPointsHighlightState;
    private GenericHighlightState<ParametricCurve> curvesHighlightState;
    private GenericHighlightState<IGeometryObject> everythingHighlightState;
    private GenericHighlightState<GreatCircle> greatCirclesHighlightState;
    private GenericHighlightState<GrabablePoint> moveAblePointsHighlightState;

    public Highlighter()
    {
        controllPointsHighlightState = new GenericHighlightState<ControllPoint>();
        curvesHighlightState = new GenericHighlightState<ParametricCurve>();
        everythingHighlightState = new GenericHighlightState<IGeometryObject>();
        greatCirclesHighlightState = new GenericHighlightState<GreatCircle>();
        moveAblePointsHighlightState = new GenericHighlightState<GrabablePoint>();
        currentState = everythingHighlightState;
        currentState.OnEnter();
    }

    public void Highlight(IGeometryObject geometryObject)
    {
        currentState.Highlight(geometryObject);
    }

    public void UnHighlight()
    {
        currentState.UnHighlight();
    }

    public void HighlightEverythingState()
    {
        curvesHighlightState.OnExit();
        currentState = everythingHighlightState;
        currentState.OnEnter();
    }

    public void HighlightControllPointsState()
    {
        curvesHighlightState.OnExit();
        currentState = controllPointsHighlightState;
        currentState.OnEnter();
    }

    public void HighlightCurvesState()
    {
        curvesHighlightState.OnExit();
        currentState = curvesHighlightState;
        currentState.OnEnter();
    }

    public void HighlightMoveAblePointsState()
    {
        curvesHighlightState.OnExit();
        currentState = moveAblePointsHighlightState;
        currentState.OnEnter();
    }

    public void HighlightGreatCirclesState() 
    {
        curvesHighlightState.OnExit();
        currentState = greatCirclesHighlightState;
        currentState.OnEnter();
    }
}
