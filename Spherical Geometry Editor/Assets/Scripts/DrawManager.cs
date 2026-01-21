using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private static DrawManager instance;
    [SerializeField] private GameObject controllPointPreafab;
    [SerializeField] private GameObject parametricCurvePrefab;
    [SerializeField] private GameObject GreatCirclePrefab;
    [SerializeField] private GameObject GreatCircleSegmentPrefab;
    [SerializeField] private GameObject SmallCirclePrefab;
    private Stack<ICommand> undoStack = new Stack<ICommand>();
    private List<GameObject> SelectedControllPoints = new List<GameObject>();
    private DrawingState currentState;
    private SelectOrPlaceControllPointsState selectOrPlaceControllPointsState;
    private DrawParametricCurveState drawParametricCurveState;
    private IntersectDrawState intersectDrawState;
    private MoveState moveState;
    private PlacePointsState placePointsState;
    private IdleState idleState;
    private LineDrawingMode lineDrawingMode;
    private CircleDrawMode circleDrawMode;
    private SegmentDrawingMode segmentDrawingMode;

    public static DrawManager Instance {  get { return instance; } }
    public SelectOrPlaceControllPointsState SelectOrPlaceControllPointsState { get { return selectOrPlaceControllPointsState; } }
    public DrawParametricCurveState DrawParametricCurveState { get { return drawParametricCurveState; } }
    public IntersectDrawState IntersectDrawState { get { return intersectDrawState; } }
    public MoveState MoveState { get { return moveState; } }
    public PlacePointsState PlacePointsState { get { return placePointsState; } }
    public IdleState IdleState { get { return idleState; } }

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

        selectOrPlaceControllPointsState = ScriptableObject.CreateInstance<SelectOrPlaceControllPointsState>();
        selectOrPlaceControllPointsState.SetUp(this,controllPointPreafab);
        drawParametricCurveState = ScriptableObject.CreateInstance<DrawParametricCurveState>();
        drawParametricCurveState.SetUp(this);
        intersectDrawState = ScriptableObject.CreateInstance<IntersectDrawState>();
        intersectDrawState.SetUp(this,controllPointPreafab);
        moveState = ScriptableObject.CreateInstance<MoveState>();
        moveState.SetUp(this);
        placePointsState = ScriptableObject.CreateInstance<PlacePointsState>();
        placePointsState.SetUp(this, controllPointPreafab);
        idleState = ScriptableObject.CreateInstance<IdleState>();
        idleState.SetUp(this);

        lineDrawingMode = ScriptableObject.CreateInstance<LineDrawingMode>();
        lineDrawingMode.SetUp(GreatCirclePrefab);
        circleDrawMode = ScriptableObject.CreateInstance<CircleDrawMode>();
        circleDrawMode.SetUp(SmallCirclePrefab);
        segmentDrawingMode = ScriptableObject.CreateInstance<SegmentDrawingMode>();
        segmentDrawingMode.SetUp(GreatCircleSegmentPrefab);

        currentState = idleState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //DrawLine(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
            SetState(selectOrPlaceControllPointsState, lineDrawingMode);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //point1 = PlacePoint();
            //Debug.Log(point1);
            currentState.OnLeftMouseUp();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //point2 = PlacePoint();
            //Debug.Log(point2);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //DrawSegment(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
            SetState(selectOrPlaceControllPointsState, segmentDrawingMode);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // DrawCircle(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
            SetState(selectOrPlaceControllPointsState, circleDrawMode);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            SetState(idleState);
            Undo();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SetState(moveState);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SetState(placePointsState); 
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            SetState(intersectDrawState);
        }

    }


    public void SetState(DrawingState state, DrawingMode mode)
    {
        currentState.OnExit();
        currentState = state;
        Debug.Log(currentState);
        Debug.Log(mode);
        currentState.OnEnter(mode);
    }

    public void SetState(DrawingState state)
    {
        currentState.OnExit();
        currentState = state;
        Debug.Log(currentState);
        currentState.OnEnter(null);
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

    public GameObject[] ControllPoints()
    {
        return SelectedControllPoints.ToArray();
    }

    public int ControllPointsCount()
    {
        return SelectedControllPoints.Count;
    }

    public void ClearControllPoints()
    {
        SelectedControllPoints = new List<GameObject>();
    }

    public void SelectControllPoint(GameObject point)
    {
        SelectedControllPoints.Add(point);
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
    //        GameObject point = Instantiate(controllPointPreafab); 
    //        PlacePointCommand command = new PlacePointCommand(hit.point, point);
    //        command.Execute();
    //        undoStack.Push(command);
    //        return point;
    //    }

    //    return null;
    //}

}
