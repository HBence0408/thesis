using UnityEngine;

public class PlacePointsState : DrawingState
{
    private GameObject prefab;

    public PlacePointsState(DrawManager manager, GameObject prefab) : base(manager)
    {
        this.prefab = prefab;
    }

    public override void OnLeftMouseUp()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(prefab);
            PlacePointCommand command = new PlacePointCommand(hit.point, prefab);
            manager.ExecuteCommand(command);
            manager.SetState(manager.IdleState);
        }
    }
}
