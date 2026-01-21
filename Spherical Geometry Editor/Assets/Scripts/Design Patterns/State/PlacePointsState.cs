using UnityEngine;

public class PlacePointsState : DrawingState
{
    private GameObject prefab;

    public void SetUp(DrawManager manager, GameObject prefab)
    {
        base.SetUp(manager);
        this.prefab = prefab;
    }

    public override void OnLeftMouseUp()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");

            GameObject point = Instantiate(prefab);
            PlacePointCommand command = new PlacePointCommand(hit.point, point.GetComponent<ControllPoint>());
            manager.ExecuteCommand(command);
            manager.SetState(manager.IdleState);
        }
    }
}
