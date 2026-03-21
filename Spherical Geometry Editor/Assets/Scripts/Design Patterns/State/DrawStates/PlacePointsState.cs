using UnityEngine;

public class PlacePointsState : DrawingState
{
    private SphericalGeometryFactory factory;
    private CommandInvoker commandInvoker;

    public PlacePointsState(DrawManager manager, SphericalGeometryFactory factory, CommandInvoker commandInvoker) : base(manager)
    {
        this.factory = factory;
        this.commandInvoker = commandInvoker;
    }

    public override void OnLeftMouseUp()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {

            PlacePointCommand command = new PlacePointCommand(hit.point, factory);
            commandInvoker.ExecuteCommand(command);
            manager.SetState(this);
        }
    }
}
