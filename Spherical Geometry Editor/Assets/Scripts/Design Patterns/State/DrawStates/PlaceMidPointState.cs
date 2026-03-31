using UnityEngine;

public class PlaceMidPointState : DrawingState
{
    private SphericalGeometryFactory factory;
    private CommandInvoker commandInvoker;
    private IRepository repository;
    private ControllPoint point1 = null;
    private ControllPoint point2 = null;

    public PlaceMidPointState(DrawManager manager, SphericalGeometryFactory factory, CommandInvoker commandInvoker, IRepository repository) : base(manager)
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
            Debug.Log("Hit: " + hit.transform.name);
            
            if (this.point1 == null && hit.transform.TryGetComponent<ControllPoint>(out ControllPoint point1))
            {
                this.point1 = point1;
            }
            else if(this.point2 == null && hit.transform.TryGetComponent<ControllPoint>(out ControllPoint point2))
            {
                this.point2 = point2;

                PlaceMidPointCommand command = new PlaceMidPointCommand(this.point1, this.point2, factory, repository);
                commandInvoker.ExecuteCommand(command);
                manager.SetState(this);
            }
        }
    }

    public override void OnExit()
    {
        point1 = null;
        point2 = null;
        base.OnExit();
    }
}
