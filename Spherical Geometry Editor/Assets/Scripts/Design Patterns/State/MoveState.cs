using UnityEngine;

public class MoveState : DrawingState
{
    private GameObject point = null;
    private Vector3? pos = null;

    public MoveState(DrawManager manager) : base(manager) {}

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
                else
                {
                    Debug.Log("point grabbed");
                }
            }
        }
    }

    public override void OnLeftMouseUp()
    {
            if (point is not null && pos is not null)
            {
                Vector3 currentPos = point.transform.position;
                MovePointCommand command = new MovePointCommand(currentPos, point, (Vector3)pos);
                manager.ExecuteCommand(command);
                manager.SetState(this);
            Debug.Log("point released");
        }      
    }

    public override void OnExit()
    {
        base.OnExit();
        point = null;
        pos = null;
    }
}
