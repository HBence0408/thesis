using UnityEngine;

public class MoveState : DrawingState
{
    private GameObject point = null;

    public override void OnLeftMouseUp()
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
            }
            else if (point is not null)
            {
                MovePointCommand command = new MovePointCommand(hit.point, point, point.transform.position);
                manager.ExecuteCommand(command);
                manager.SetState(manager.IdleState);
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        point = null;
    }
}
