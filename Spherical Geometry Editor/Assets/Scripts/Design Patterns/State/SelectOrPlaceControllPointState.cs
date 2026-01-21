using Unity.VisualScripting;
using UnityEngine;

public class SelectOrPlaceControllPointsState : DrawingState
{
    // még nincs select csak place
    private GameObject prefab;

    public void SetUp(DrawManager manager ,GameObject prefab)
    {
        base.SetUp(manager);
        this.prefab = prefab;
    }

    public override void OnLeftMouseDown()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");
            GameObject point = Instantiate(prefab);
            PlacePointCommand command = new PlacePointCommand(hit.point, point);
            manager.ExecuteCommand(command);
            manager.SelectControllPoint(point);
        }

        if (drawingMode.ControllPoints <= manager.ControllPointsCount())
        {
            manager.SetState(manager.DrawParametricCurveState, drawingMode);
        }
        else
        {
            Debug.Log(manager.SelectOrPlaceControllPointsState);
            manager.SetState(manager.SelectOrPlaceControllPointsState, drawingMode);
        }
    }
}
