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

    private void OnDown(IGeometryObject geometryObject, Vector3 hitpoint)
    {
            if (geometryObject is GreatCircle && greatCircle == null)
            {
                // manager.SelectControllPoint(hit.transform.gameObject);
                greatCircle = geometryObject as GreatCircle;
            }
            else if(greatCircle != null)
            {
                if (geometryObject is ControllPoint)
                {
                    point = ((ControllPoint)(geometryObject)).gameObject;
                }
                else if (geometryObject is ParametricCurve)
                {
                    PlaceLimitedPointCommand pointCommand = new PlaceLimitedPointCommand(hitpoint, greatCircle, factory, repository);
                    commandInvoker.ExecuteCommand(pointCommand);
                    point = pointCommand.GetPoint().gameObject;
                }
                else
                {
                    PlacePointCommand pointCommand = new PlacePointCommand(hitpoint, factory, repository);
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
