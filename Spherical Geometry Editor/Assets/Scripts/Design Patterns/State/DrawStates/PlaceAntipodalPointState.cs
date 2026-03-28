using UnityEngine;

public class PlaceAntidotalPointState : DrawingState
{
    private SphericalGeometryFactory factory;
    private CommandInvoker commandInvoker;
    private IRepository repository;

    public PlaceAntidotalPointState(DrawManager manager, SphericalGeometryFactory factory, CommandInvoker commandInvoker, IRepository repository) : base(manager)
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
