using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlacePolePointsState : DrawingState
{
    private ISphericalGeometryFactory factory;
    private ICommandInvoker commandInvoker;
    private IRepository repository;

    public PlacePolePointsState(IDrawManager manager, ISphericalGeometryFactory factory, ICommandInvoker commandInvoker, IRepository repository) : base(manager)
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
        manager.OnDown -= OnDown;
    }

    private void OnDown()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {

            if (hit.transform.TryGetComponent<GreatCircle>(out GreatCircle curve))
            {
                PlacePolesCommand command = new PlacePolesCommand(curve, factory, repository);
                commandInvoker.ExecuteCommand(command);
                
                curve.Subscirbe(command.GetPoints()[0]);
                curve.Subscirbe(command.GetPoints()[1]);
            }





            //ParametricCurve curve;
            //if (hit.transform.TryGetComponent<ParametricCurve>(out curve))
            //{
            //    command.GetPoint()
            //}

            manager.SetState(this);
        }
    }
}
