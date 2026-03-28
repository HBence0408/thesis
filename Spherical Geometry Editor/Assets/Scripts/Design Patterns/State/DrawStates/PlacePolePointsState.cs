using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlacePolePointsState : DrawingState
{
    private SphericalGeometryFactory factory;
    private CommandInvoker commandInvoker;
    private IRepository repository;

    public PlacePolePointsState(DrawManager manager, SphericalGeometryFactory factory, CommandInvoker commandInvoker, IRepository repository) : base(manager)
    {
        this.factory = factory;
        this.commandInvoker = commandInvoker;
        this.repository = repository;
    }

    public override void OnLeftMouseUp()
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
