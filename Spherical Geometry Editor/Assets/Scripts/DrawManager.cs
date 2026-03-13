using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private static DrawManager instance;
    [SerializeField] private GameObject ControllPointPreafab;
    [SerializeField] private GameObject parametricCurvePrefab;
    [SerializeField] private GameObject GreatCirclePrefab;
    [SerializeField] private GameObject GreatCircleSegmentPrefab;
    [SerializeField] private GameObject SmallCirclePrefab;
    [SerializeField] private GameObject IntersectPointPrefab;
    private Stack<ICommand> undoStack = new Stack<ICommand>();

    private DrawingState currentState;
    private IntersectDrawState intersectDrawState;
    private MoveState moveState;
    private PlacePointsState placePointsState;
    private IdleState idleState;
    private GreatCircleDrawState greatCircleDrawState;
    private SmallCircleDrawState smallCircleDrawState;
    private GreatCircleSegmentDrawState greatCircleSegmentDrawState;

    // itt vagy clone-ozás vagy csak enummal hogy melyik actív éppen
    public static DrawManager Instance {  get { return instance; } }
    public IntersectDrawState IntersectDrawState { get { return intersectDrawState; } }
    public MoveState MoveState { get { return moveState; } }
    public PlacePointsState PlacePointsState { get { return placePointsState; } }
    public IdleState IdleState { get { return idleState; } }
    public GreatCircleDrawState GreatCircleDrawState { get { return greatCircleDrawState; }  }
    public SmallCircleDrawState SmallCircleDrawState { get {return smallCircleDrawState;  }  }
    public GreatCircleSegmentDrawState GreatCircleSegmentDrawState { get { return  greatCircleSegmentDrawState; }  }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("multiple drawing managers, destroying self");
            Destroy(this.gameObject);
        }

        intersectDrawState = new IntersectDrawState(this, IntersectPointPrefab);
        greatCircleDrawState = new GreatCircleDrawState(this, GreatCirclePrefab, ControllPointPreafab);
        greatCircleSegmentDrawState = new GreatCircleSegmentDrawState(this, GreatCircleSegmentPrefab, ControllPointPreafab);
        smallCircleDrawState = new SmallCircleDrawState(this, SmallCirclePrefab, ControllPointPreafab);
        moveState = new MoveState(this);
        Debug.Log(ControllPointPreafab);
        placePointsState = new PlacePointsState(this, ControllPointPreafab);
        idleState = new IdleState(this);

        currentState = idleState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //DrawLine(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
          //  SetState(selectOrPlaceControllPointsState, lineDrawingMode);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //point1 = PlacePoint();
            //Debug.Log(point1);
            currentState.OnLeftMouseUp();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //point2 = PlacePoint();
            //Debug.Log(point2);
            currentState.OnLeftMouseDown();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //DrawSegment(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
           // SetState(selectOrPlaceControllPointsState, segmentDrawingMode);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // DrawCircle(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
          //  SetState(selectOrPlaceControllPointsState, circleDrawMode);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
           // SetState(idleState);
          //  Undo();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
           // SetState(moveState);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
           // SetState(placePointsState); 
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
           // SetState(intersectDrawState);
        }
    }

    public void SetState(DrawingState state)
    {
        currentState.OnExit();
        currentState = state;
        Debug.Log(currentState);
        currentState.OnEnter();
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
    }

    public void Undo()
    {
        ICommand command = undoStack.Pop(); 
        command.UnExecute();
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

    //private void DrawLine(ControllPoint point1, ControllPoint point2)
    //{
    //    GameObject parametricCurve = Instantiate(GreatCirclePrefab);
    //    parametricCurve.transform.position = new Vector3(0, 0, 0);
    //    GreatCircle script = parametricCurve.GetComponent<GreatCircle>();
    //    DrawGreatCircleCommand command = new DrawGreatCircleCommand(script, point1, point2);
    //    command.Execute();
    //    undoStack.Push(command);
    //}

    //private void DrawSegment(ControllPoint point1, ControllPoint point2)
    //{
    //    GameObject parametricCurve = Instantiate(GreatCircleSegmentPrefab);
    //    parametricCurve.transform.position = new Vector3(0, 0, 0);
    //    GreatCircleSegment script = parametricCurve.GetComponent<GreatCircleSegment>();
    //    DrawGreatCircleSegmentCommand command = new DrawGreatCircleSegmentCommand(script, point1, point2);
    //    command.Execute();
    //    undoStack.Push(command);
    //} 

    ////private void DrawCircle(ControllPoint point1, ControllPoint point2)
    //{
    //    GameObject parametricCurve = Instantiate(SmallCirclePrefab);
    //    parametricCurve.transform.position = new Vector3(0, 0, 0);
    //    SmallCircle script = parametricCurve.GetComponent<SmallCircle>();
    //    DrawSmallCircleCommand command = new DrawSmallCircleCommand(script, point1, point2);
    //    command.Execute();
    //    undoStack.Push(command);
    //}

    //private GameObject PlacePoint()
    //{
    //    RaycastHit hit;

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out hit, 1000))
    //    {
    //        Debug.Log(hit.transform.name);
    //        Debug.Log("hit");
    //        GameObject point = Instantiate(ControllPointPreafab); 
    //        PlacePointCommand command = new PlacePointCommand(hit.point, point);
    //        command.Execute();
    //        undoStack.Push(command);
    //        return point;
    //    }

    //    return null;
    //}

}
