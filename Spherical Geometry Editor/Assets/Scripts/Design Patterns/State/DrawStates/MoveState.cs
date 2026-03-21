using UnityEngine;
using UnityEngine.UIElements;

public class MoveState : DrawingState
{
    private GameObject point = null;
    private Vector3? pos = null;
    private CommandInvoker commandInvoker;

    public MoveState(DrawManager manager, CommandInvoker commandInvoker) : base(manager) 
    {
        this.commandInvoker = commandInvoker;
    }

    public override void OnLeftMouseDown()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");
            if (hit.transform.gameObject.tag == "point")
            {
                point = hit.transform.gameObject;
                pos = hit.transform.position;
                if (point.GetComponent<ControllPoint>() is IntersectionPoint)
                {
                    point = null;
                    pos = null;
                }
            }
        }
    }

    public override void OnLeftMouseHold()
    {
        if (point != null && pos != null)
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Plane plane = new Plane(Vector3.up, Vector3.zero);
            //float distance;
            //if (plane.Raycast(ray, out distance))
            //{
            //    Vector3 worldPos = ray.GetPoint(distance);
            //    point.transform.position = worldPos;
            //}

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Plane plane = new Plane(-Camera.main.transform.forward, (Vector3)pos);
            //float distance;
            //if (plane.Raycast(ray, out distance))
            //{
            //    Vector3 point = ray.GetPoint(distance);
            //    point = point.normalized;
            //}

            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                point.transform.position = hit.point.normalized;
            }

        }
    }
    

    public override void OnLeftMouseUp()
    {
            if (point is not null && pos is not null)
            {
                Vector3 currentPos = point.transform.position;
                MovePointCommand command = new MovePointCommand(currentPos, point, (Vector3)pos);
                commandInvoker.ExecuteCommand(command);
                manager.SetState(this);
        }      
    }

    public override void OnExit()
    {
        base.OnExit();
        point = null;
        pos = null;
    }
}
