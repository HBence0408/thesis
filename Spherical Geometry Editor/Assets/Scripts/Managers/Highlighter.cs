using UnityEngine;

public class Highlighter : IHighlighter
{
    private IHighlightState currentState;
    private GenericHighlightState<ControllPoint> controllPointsHighlightState;
    private GenericHighlightState<ParametricCurve> curvesHighlightState;
    private GenericHighlightState<IGeometryObject> everythingHighlightState;
    private GenericHighlightState<GreatCircle> greatCirclesHighlightState;
    private GenericHighlightState<IMoveablePoint> moveAblePointsHighlightState;

    public Highlighter()
    {
        controllPointsHighlightState = new GenericHighlightState<ControllPoint>();
        curvesHighlightState = new GenericHighlightState<ParametricCurve>();
        everythingHighlightState = new GenericHighlightState<IGeometryObject>();
        greatCirclesHighlightState = new GenericHighlightState<GreatCircle>();
        moveAblePointsHighlightState = new GenericHighlightState<IMoveablePoint>();
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
        currentState.OnExit();
        currentState = everythingHighlightState;
        currentState.OnEnter();
    }

    public void HighlightControllPointsState()
    {
        currentState.OnExit();
        currentState = controllPointsHighlightState;
        currentState.OnEnter();
    }

    public void HighlightCurvesState()
    {
        currentState.OnExit();
        currentState = curvesHighlightState;
        currentState.OnEnter();
    }

    public void HighlightMoveAblePointsState()
    {
        currentState.OnExit();
        currentState = moveAblePointsHighlightState;
        currentState.OnEnter();
    }

    public void HighlightGreatCirclesState() 
    {
        currentState.OnExit();
        currentState = greatCirclesHighlightState;
        currentState.OnEnter();
    }
}
