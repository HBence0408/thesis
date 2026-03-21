using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public abstract class DrawParametricCurveState : DrawingState
{
    protected List<GameObject> SelectedControllPoints = new List<GameObject>();
    protected int requiredControllPoints;
    protected SphericalGeometryFactory factory;
    protected CommandInvoker commandInvoker;

    public DrawParametricCurveState(DrawManager manager, SphericalGeometryFactory factory, CommandInvoker commandInvoker) : base(manager)
    {
        this.factory = factory;
        this.commandInvoker = commandInvoker;
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
                PlacePointCommand command = new PlacePointCommand(hit.point,factory);
                commandInvoker.ExecuteCommand(command);
                SelectedControllPoints.Add(command.GetPoint().gameObject);
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

