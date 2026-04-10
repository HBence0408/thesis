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

    private void OnDown()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform.TryGetComponent<ParametricCurve>(out ParametricCurve curve))
            {
                PlaceLimitedPointCommand command = new PlaceLimitedPointCommand(hit.point, curve, factory, repository);
                commandInvoker.ExecuteCommand(command);
                manager.SetState(this);
            }
            else
            {
                PlacePointCommand command = new PlacePointCommand(hit.point, factory, repository);
                commandInvoker.ExecuteCommand(command);
                manager.SetState(this);
            }   
        }
    }
}
