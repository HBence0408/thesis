using UnityEngine;

public class PlaceMidPointState : DrawingState
{
    private ISphericalGeometryFactory factory;
    private ICommandInvoker commandInvoker;
    private IRepository repository;
    private ControllPoint point1 = null;
    private ControllPoint point2 = null;

    public PlaceMidPointState(IDrawManager manager, ISphericalGeometryFactory factory, ICommandInvoker commandInvoker, IRepository repository) : base(manager)
    {
        this.factory = factory;
        this.commandInvoker = commandInvoker;
        this.repository = repository;
    }

    private void OnDown(IGeometryObject geometryObject, Vector3 hitpoint)
    {


            if (this.point1 == null && geometryObject is ControllPoint)
            {
                this.point1 = geometryObject as ControllPoint;
            }
            else if(this.point2 == null && geometryObject is ControllPoint)
            {
                this.point2 = geometryObject as ControllPoint;

                PlaceMidPointCommand command = new PlaceMidPointCommand(this.point1, this.point2, factory, repository);
                commandInvoker.ExecuteCommand(command);
                manager.SetState(this);
            }
        
    }

    public override void OnEnter()
    {
        manager.OnDown += OnDown;
    }

    public override void OnExit()
    {
        point1 = null;
        point2 = null;
        manager.OnDown -= OnDown;
    }
}
