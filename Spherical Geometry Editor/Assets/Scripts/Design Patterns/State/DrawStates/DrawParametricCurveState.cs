using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public abstract class DrawParametricCurveState : DrawingState
{
    protected List<GameObject> SelectedControllPoints = new List<GameObject>();
    protected int requiredControllPoints;
    protected ISphericalGeometryFactory factory;
    protected ICommandInvoker commandInvoker;
    protected IRepository repository;

    public DrawParametricCurveState(IDrawManager manager, ISphericalGeometryFactory factory, ICommandInvoker commandInvoker, IRepository repository) : base(manager)
    {
        this.factory = factory;
        this.commandInvoker = commandInvoker;
        this.repository = repository;
    }


    public override void OnEnter()
    {
        manager.OnDown += OnDown;
    }

    public override void OnExit()
    {
        manager.OnDown += OnDown;
    }

    private void OnDown()
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
                PlacePointCommand command = new PlacePointCommand(hit.point,factory, repository);
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

