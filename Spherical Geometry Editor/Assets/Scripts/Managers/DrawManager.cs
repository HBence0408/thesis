using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DrawManager
{
    private SphericalGeometryFactory factory;
    private CommandInvoker commandInvoker;
    private IRepository repository;

    private DrawingState currentState;
    private IntersectDrawState intersectDrawState;
    private MoveState moveState;
    private PlacePointsState placePointsState;
    private IdleState idleState;
    private GreatCircleDrawState greatCircleDrawState;
    private SmallCircleDrawState smallCircleDrawState;
    private GreatCircleSegmentDrawState greatCircleSegmentDrawState;
    private DeleteState deleteState;
    private PlaceAntidotalPointState placeAntidotalPointState;
    private PlacePolePointsState placePolePointsState;
    private PlaceMidPointState placeMidPointState;

    public DrawManager(SphericalGeometryFactory factory, CommandInvoker commandInvoker, IRepository repoitory)
    {
        this.factory = factory;
        this.commandInvoker = commandInvoker;
        this.repository = repoitory;

        intersectDrawState = new IntersectDrawState(this, factory, commandInvoker, repository);
        greatCircleDrawState = new GreatCircleDrawState(this, factory, commandInvoker, repository);
        greatCircleSegmentDrawState = new GreatCircleSegmentDrawState(this, factory, commandInvoker, repository);
        smallCircleDrawState = new SmallCircleDrawState(this, factory, commandInvoker, repository);
        moveState = new MoveState(this, commandInvoker);
        placePointsState = new PlacePointsState(this, factory, commandInvoker, repository);
        idleState = new IdleState(this);
        deleteState = new DeleteState(this, commandInvoker, repository);
        placeAntidotalPointState = new PlaceAntidotalPointState(this, factory, commandInvoker, repository);
        placePolePointsState = new PlacePolePointsState(this, factory, commandInvoker, repository);
        placeMidPointState = new PlaceMidPointState(this, factory, commandInvoker, repository);

        currentState = idleState;
    }

    // itt vagy clone-ozás vagy csak enummal hogy melyik actív éppen
    //public IntersectDrawState IntersectDrawState { get { return intersectDrawState; } }
    //public MoveState MoveState { get { return moveState; } }
    //public PlacePointsState PlacePointsState { get { return placePointsState; } }
    //public IdleState IdleState { get { return idleState; } }
    //public GreatCircleDrawState GreatCircleDrawState { get { return greatCircleDrawState; }  }
    //public SmallCircleDrawState SmallCircleDrawState { get {return smallCircleDrawState;  }  }
    //public GreatCircleSegmentDrawState GreatCircleSegmentDrawState { get { return  greatCircleSegmentDrawState; }  }

    //TODO
    //eventek itt is?

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

    public void ResetState()
    {
        SetState(currentState);
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

    public void Delete()
    {
        SetState(deleteState);
    }

    public void PlaceAntipodalPoint()
    {
        SetState(placeAntidotalPointState);
    }

    public void PlacePolePoints()
    {
        SetState(placePolePointsState);
    }

    public void PlaceMidPoint()
    {
        SetState(placeMidPointState);
    }
}
