using Unity.VisualScripting;
using UnityEngine;

public class PlacePointsState : DrawingState
{
    private SphericalGeometryFactory factory;
    private CommandInvoker commandInvoker;
    private IRepository repository;

    public PlacePointsState(DrawManager manager, SphericalGeometryFactory factory, CommandInvoker commandInvoker, IRepository repository) : base(manager)
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

            if (hit.transform.TryGetComponent<ParametricCurve>(out ParametricCurve curve))
            {
                PlaceLimitedPointCommand command = new PlaceLimitedPointCommand(hit.point, factory, repository);
                commandInvoker.ExecuteCommand(command);
                command.GetPoint().SetCurve(curve);
                curve.Subscirbe(command.GetPoint());
            }
            else
            {
                PlacePointCommand command = new PlacePointCommand(hit.point, factory, repository);
                commandInvoker.ExecuteCommand(command);
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
