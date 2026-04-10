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

    private void OnDown(IGeometryObject geometryObject, Vector3 hitpoint)
    {


        

            if (geometryObject is GreatCircle)
            {
                GreatCircle greatCircle = geometryObject as GreatCircle;

                PlacePolesCommand command = new PlacePolesCommand(greatCircle, factory, repository);
                commandInvoker.ExecuteCommand(command);
                
                greatCircle.Subscirbe(command.GetPoints()[0]);
                greatCircle.Subscirbe(command.GetPoints()[1]);
            }

            manager.SetState(this);
        
    }
}
