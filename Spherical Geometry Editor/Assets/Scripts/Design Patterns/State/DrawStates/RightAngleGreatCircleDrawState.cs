using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RightAngleGreatCircleDrawState : DrawingState
{
    private GameObject point;
    private GreatCircle greatCircle;
    protected ISphericalGeometryFactory factory;
    protected ICommandInvoker commandInvoker;
    protected IRepository repository;
    private ShadowPolePoint shadowPolePoint;

    public RightAngleGreatCircleDrawState(IDrawManager manager, ISphericalGeometryFactory factory, ICommandInvoker commandInvoker ,IRepository repository) : base(manager)
    {
        this.repository = repository;
        this.factory = factory;
        this.commandInvoker = commandInvoker;
    }

    private void OnDown()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {

            if (hit.transform.TryGetComponent<GreatCircle>(out GreatCircle g) && greatCircle == null)
            {
                // manager.SelectControllPoint(hit.transform.gameObject);
                greatCircle = g;
            }
            else if(greatCircle != null)
            {
                if (hit.transform.gameObject.tag == "point")
                {
                    point = hit.transform.gameObject;
                }
                else if (hit.transform.TryGetComponent<ParametricCurve>(out ParametricCurve curve))
                {
                    PlaceLimitedPointCommand pointCommand = new PlaceLimitedPointCommand(hit.point, curve, factory, repository);
                    commandInvoker.ExecuteCommand(pointCommand);
                    point = pointCommand.GetPoint().gameObject;
                }
                else
                {
                    PlacePointCommand pointCommand = new PlacePointCommand(hit.point, factory, repository);
                    commandInvoker.ExecuteCommand(pointCommand);
                    point = pointCommand.GetPoint().gameObject;
                }

                shadowPolePoint = factory.CreateShadowPolepoint(greatCircle.NormalOfPlane);
                repository.Store(shadowPolePoint);
                greatCircle.Subscirbe(shadowPolePoint);
                shadowPolePoint.SetCurve(greatCircle, true);
                

                DrawGreatCircleCommand greatCircleCommand = new DrawGreatCircleCommand(point, shadowPolePoint.gameObject, factory, repository);
                commandInvoker.ExecuteCommand(greatCircleCommand);

                manager.SetState(this);
            }
        }

        //if (requiredControllPoints == SelectedControllPoints.Count)
        //{
        //    DrawParametricCurve();
        //    SelectedControllPoints = new List<GameObject>();
        //}

        

    }

    public override void OnEnter()
    {
        manager.OnDown += OnDown;
    }

    public override void OnExit()
    {
        point = null;
        greatCircle = null;
        shadowPolePoint = null;
        manager.OnDown -= OnDown;
    }
}
