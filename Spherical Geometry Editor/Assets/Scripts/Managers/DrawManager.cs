using System.Collections.Generic;
using UnityEngine;

public class DrawManager
{
    private SphericalGeometryFactory factory;
    private CommandInvoker commandInvoker;

    private DrawingState currentState;
    private IntersectDrawState intersectDrawState;
    private MoveState moveState;
    private PlacePointsState placePointsState;
    private IdleState idleState;
    private GreatCircleDrawState greatCircleDrawState;
    private SmallCircleDrawState smallCircleDrawState;
    private GreatCircleSegmentDrawState greatCircleSegmentDrawState;

    public DrawManager(SphericalGeometryFactory factory, CommandInvoker commandInvoker)
    {
        this.factory = factory;
        this.commandInvoker = commandInvoker;

        intersectDrawState = new IntersectDrawState(this, factory, commandInvoker);
        greatCircleDrawState = new GreatCircleDrawState(this, factory, commandInvoker);
        greatCircleSegmentDrawState = new GreatCircleSegmentDrawState(this, factory, commandInvoker);
        smallCircleDrawState = new SmallCircleDrawState(this, factory, commandInvoker);
        moveState = new MoveState(this, commandInvoker);
        placePointsState = new PlacePointsState(this, factory, commandInvoker);
        idleState = new IdleState(this);

        currentState = idleState;
    }

    // itt vagy clone-ozás vagy csak enummal hogy melyik actív éppen
    public IntersectDrawState IntersectDrawState { get { return intersectDrawState; } }
    public MoveState MoveState { get { return moveState; } }
    public PlacePointsState PlacePointsState { get { return placePointsState; } }
    public IdleState IdleState { get { return idleState; } }
    public GreatCircleDrawState GreatCircleDrawState { get { return greatCircleDrawState; }  }
    public SmallCircleDrawState SmallCircleDrawState { get {return smallCircleDrawState;  }  }
    public GreatCircleSegmentDrawState GreatCircleSegmentDrawState { get { return  greatCircleSegmentDrawState; }  }


    public void OnLeftMouseDown()
    {
        currentState.OnLeftMouseDown();
    }

    public void OnLeftMouseUp()
    {
        currentState.OnLeftMouseUp();
    }

    public void OnLeftMouseHold()
    {
        currentState.OnLeftMouseHold();
    }

    public void SetState(DrawingState state)
    {
        currentState.OnExit();
        currentState = state;
        Debug.Log(currentState);
        currentState.OnEnter();
    }

    public void DrawLine()
    {
        SetState(greatCircleDrawState);
    }

    public void DrawSegment()
    {
        SetState(greatCircleSegmentDrawState);
    }

    public void DrawPoint()
    {
        SetState(placePointsState);
    }

    public void DrawCircle()
    {
       SetState(smallCircleDrawState);
    }

    public void MovePoint()
    {
        SetState(moveState);
    }

    public void Intersect()
    {
        SetState(intersectDrawState);
    }
}
