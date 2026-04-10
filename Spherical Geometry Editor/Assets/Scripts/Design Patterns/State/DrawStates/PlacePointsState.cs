using Unity.VisualScripting;
using UnityEngine;

public class PlacePointsState : DrawingState
{
    private ISphericalGeometryFactory factory;
    private ICommandInvoker commandInvoker;
    private IRepository repository;

    public PlacePointsState(IDrawManager manager, ISphericalGeometryFactory factory, ICommandInvoker commandInvoker, IRepository repository) : base(manager)
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

            if (geometryObject is ParametricCurve)
            {
                PlaceLimitedPointCommand command = new PlaceLimitedPointCommand(hitpoint, geometryObject as ParametricCurve, factory, repository);
                commandInvoker.ExecuteCommand(command);
                manager.SetState(this);
            }
            else
            {
                PlacePointCommand command = new PlacePointCommand(hitpoint, factory, repository);
                commandInvoker.ExecuteCommand(command);
                manager.SetState(this);
            }   
        
    }
}
