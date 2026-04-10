using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DrawManager : IDrawManager
{
    private ISphericalGeometryFactory factory;
    private ICommandInvoker commandInvoker;
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
    private RightAngleGreatCircleDrawState rightAngleGreatCircleDrawState;
    private ColorState blackColoringState;
    private ColorState greyColoringState;
    private ColorState blueColoringState;
    private ColorState redColoringState;
    private ColorState greenColoringState;
    private ColorState magentaColoringState;

    public event Action<IGeometryObject, Vector3> OnDown;
    public event Action<IGeometryObject, Vector3> OnUp;
    public event Action<IGeometryObject, Vector3> OnHold;

    public DrawManager(ISphericalGeometryFactory factory, ICommandInvoker commandInvoker, IRepository repoitory)
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
        rightAngleGreatCircleDrawState = new RightAngleGreatCircleDrawState(this, factory, commandInvoker, repository);
        blackColoringState = new ColorState(this, commandInvoker, Color.black);
        greyColoringState = new ColorState(this, commandInvoker, new Color(0.3f, 0.3f, 0.3f, 1) );
        blueColoringState = new ColorState(this, commandInvoker, Color.blue);
        redColoringState = new ColorState(this, commandInvoker, Color.red);
        greenColoringState = new ColorState(this, commandInvoker, Color.green);
        magentaColoringState = new ColorState(this, commandInvoker, Color.magenta);

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

    public void OnLeftMouseDown(IGeometryObject geometryObject, Vector3 hitpoint)
    {
        //currentState.OnLeftMouseDown();
        OnDown?.Invoke(geometryObject, hitpoint);
    }

    public void OnLeftMouseUp(IGeometryObject geometryObject, Vector3 hitpoint)
    {
        //currentState.OnLeftMouseUp();
        OnUp?.Invoke(geometryObject, hitpoint);
    }

    public void OnLeftMouseHold(IGeometryObject geometryObject, Vector3 hitpoint)
    {
        //currentState.OnLeftMouseHold();
        OnHold?.Invoke(geometryObject, hitpoint);
    }

    public void SetState(DrawingState state)
    {
        currentState.OnExit();
        currentState = state;
        Debug.Log(currentState);
        currentState.OnEnter();
    }

    public void Idle()
    {
        SetState(idleState);
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

    public void DrawRightAngleGreatCircle()
    {
        SetState(rightAngleGreatCircleDrawState);
    }

    public void ColorBlack()
    {
        SetState(blackColoringState);
    }

    public void ColorGrey()
    {
        SetState(greyColoringState);
    }

    public void ColorBlue()
    {
        SetState(blueColoringState);
    }

    public void ColorRed()
    {
        SetState(redColoringState);
    }

    public void ColorGreen()
    {
        SetState(greenColoringState);
    }

    public void ColorMagenta()
    {
        SetState(magentaColoringState);
    }
}
