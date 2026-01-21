using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class SelectOrPlaceControllPointsState : DrawingState
{
    private GameObject prefab;

    public void SetUp(DrawManager manager ,GameObject prefab)
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

            if (hit.transform.gameObject.tag == "point")
            {
                manager.SelectControllPoint(hit.transform.gameObject);
            }
            else
            {
                GameObject point = Instantiate(prefab);
                PlacePointCommand command = new PlacePointCommand(hit.point, point.GetComponent<ControllPoint>());
                manager.ExecuteCommand(command);
                manager.SelectControllPoint(point);
            }
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
