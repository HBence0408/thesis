using UnityEngine;

public class PlaceAntidotalPointState : DrawingState
{
    private ISphericalGeometryFactory factory;
    private ICommandInvoker commandInvoker;
    private IRepository repository;

    public PlaceAntidotalPointState(IDrawManager manager, ISphericalGeometryFactory factory, ICommandInvoker commandInvoker, IRepository repository) : base(manager)
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
            if (geometryObject is ControllPoint)
            {
                PlaceAntipodalPointCommand command = new PlaceAntipodalPointCommand(geometryObject as ControllPoint, factory, repository);
                commandInvoker.ExecuteCommand(command);
                manager.SetState(this);
            }
    }
}
