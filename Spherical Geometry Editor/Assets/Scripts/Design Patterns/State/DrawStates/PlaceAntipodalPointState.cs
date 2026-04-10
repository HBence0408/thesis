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

    private void OnDown()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            ControllPoint point;
            if (hit.collider.gameObject.TryGetComponent<ControllPoint>(out point))
            {
                PlaceAntipodalPointCommand command = new PlaceAntipodalPointCommand(point, factory, repository);
                commandInvoker.ExecuteCommand(command);
                manager.SetState(this);
            }
        }
    }
}
