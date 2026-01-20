using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private GameObject point1;
    private GameObject point2;
    [SerializeField] private GameObject controllPointPreafab;
    [SerializeField] private GameObject parametricCurvePrefab;
    [SerializeField] private GameObject GreatCirclePrefab;
    [SerializeField] private GameObject GreatCircleSegmentPrefab;
    [SerializeField] private GameObject SmallCirclePrefab;
    private Stack<ICommand> undoStack = new Stack<ICommand>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DrawLine(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            point1 = PlacePoint();
            Debug.Log(point1);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            point2 = PlacePoint();
            Debug.Log(point2);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            DrawSegment(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            DrawCircle(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
        }
    }

    private void DrawLine(ControllPoint point1, ControllPoint point2)
    {
        GameObject parametricCurve = Instantiate(GreatCirclePrefab);
        parametricCurve.transform.position = new Vector3(0, 0, 0);
        GreatCircle script = parametricCurve.GetComponent<GreatCircle>();
        DrawGreatCircleCommand command = new DrawGreatCircleCommand(script, point1, point2);
        command.Execute();
        undoStack.Push(command);
    }

    private void DrawSegment(ControllPoint point1, ControllPoint point2)
    {
        GameObject parametricCurve = Instantiate(GreatCircleSegmentPrefab);
        parametricCurve.transform.position = new Vector3(0, 0, 0);
        GreatCircleSegment script = parametricCurve.GetComponent<GreatCircleSegment>();
        DrawGreatCircleSegmentCommand command = new DrawGreatCircleSegmentCommand(script, point1, point2);
        command.Execute();
        undoStack.Push(command);
    } 

    private void DrawCircle(ControllPoint point1, ControllPoint point2)
    {
        GameObject parametricCurve = Instantiate(SmallCirclePrefab);
        parametricCurve.transform.position = new Vector3(0, 0, 0);
        SmallCircle script = parametricCurve.GetComponent<SmallCircle>();
        DrawSmallCircleCommand command = new DrawSmallCircleCommand(script, point1, point2);
        command.Execute();
        undoStack.Push(command);
    }

    private GameObject PlacePoint()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");
            GameObject point = Instantiate(controllPointPreafab); 
            PlacePointCommand command = new PlacePointCommand(hit.point, point);
            command.Execute();
            undoStack.Push(command);
            return point;
        }

        return null;
    }

}
