using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public abstract class DrawParametricCurveState : DrawingState
{
    protected GameObject curvePrefab;
    protected GameObject pointPrefab;
    protected List<GameObject> SelectedControllPoints = new List<GameObject>();
    protected int requiredControllPoints;

    public DrawParametricCurveState(DrawManager manager, GameObject curvePrefab, GameObject pointPrefab) : base(manager)
    {
        this.curvePrefab = curvePrefab;
        this.pointPrefab = pointPrefab;
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
                SelectedControllPoints.Add(hit.transform.gameObject);
            }
            else
            {
                //  GameObject point = Instantiate(prefab);
                PlacePointCommand command = new PlacePointCommand(hit.point, pointPrefab, (point) => SelectedControllPoints.Add(point));
                manager.ExecuteCommand(command);
                Debug.Log(SelectedControllPoints.Count);
                //  manager.SelectControllPoint(point);
            }
        }

        if (requiredControllPoints == SelectedControllPoints.Count)
        {
            DrawParametricCurve();
            SelectedControllPoints = new List<GameObject>();
        }
    }

    protected abstract void DrawParametricCurve();
}

