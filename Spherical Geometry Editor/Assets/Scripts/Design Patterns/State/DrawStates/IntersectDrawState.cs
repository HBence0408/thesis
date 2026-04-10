using System.Runtime.InteropServices;
using UnityEngine;

public class IntersectDrawState : DrawingState
{
    public delegate GameObject CreatePointDelegate(GameObject prefab);
    private ParametricCurve intersectable1;
    private ParametricCurve intersectable2;
    private ISphericalGeometryFactory factory;
    private ICommandInvoker commandInvoker;
    private IRepository repository;

    public IntersectDrawState(IDrawManager manager, ISphericalGeometryFactory factory, ICommandInvoker commandInvoker, IRepository repository) : base(manager)
    {
        this.factory = factory;
        this.commandInvoker = commandInvoker;
        this.repository = repository;
    }

    private void OnDown(IGeometryObject geometryObject, Vector3 hitpoint)
    {

        if (geometryObject != null && geometryObject is ParametricCurve)
        {
            ParametricCurve intersectable = geometryObject as ParametricCurve;
            if (intersectable1 == null)
            {
                intersectable1 = intersectable;
            }
            else if ( intersectable1 != null)
            {
                intersectable2 = intersectable;
                ICommand command = null;
                if (intersectable1 is GreatCircle && intersectable2 is GreatCircle)
                {
                    command = new GreatCircleGreatCircleIntersectCommand(intersectable1 as GreatCircle, intersectable2 as GreatCircle, factory, repository);
                }
                if (intersectable1 is GreatCircleSegment && intersectable2 is GreatCircleSegment)
                {
                    command = new GreatCircleSegmentGreatCircleSegmentIntersectCommand(intersectable1 as GreatCircleSegment, intersectable2 as GreatCircleSegment, factory, repository);
                }
                if (intersectable1 is GreatCircle && intersectable2 is GreatCircleSegment)
                {
                    command = new GreatCircleGreatCircleSegmentIntersectCommand(intersectable2 as GreatCircleSegment, intersectable1 as GreatCircle, factory, repository);
                }
                if (intersectable2 is GreatCircle && intersectable1 is GreatCircleSegment)
                {
                    command = new GreatCircleGreatCircleSegmentIntersectCommand(intersectable1 as GreatCircleSegment, intersectable2 as GreatCircle, factory, repository);
                }
                if (intersectable1 is GreatCircle && intersectable2 is SmallCircle)
                {
                    command = new GreatCircleSmallCircleIntersectCommand(intersectable1 as GreatCircle, intersectable2 as SmallCircle, factory, repository);
                }
                if (intersectable2 is GreatCircle && intersectable1 is SmallCircle)
                {
                    command = new GreatCircleSmallCircleIntersectCommand(intersectable2 as GreatCircle, intersectable1 as SmallCircle, factory, repository);
                }
                if (intersectable1 is SmallCircle && intersectable2 is GreatCircleSegment)
                {
                    command = new GreatCircleSegmentSmallCircleIntersectCommand(intersectable2 as GreatCircleSegment, intersectable1 as SmallCircle, factory, repository);
                }
                if (intersectable2 is SmallCircle && intersectable1 is GreatCircleSegment)
                {
                    command = new GreatCircleSegmentSmallCircleIntersectCommand(intersectable1 as GreatCircleSegment, intersectable2 as SmallCircle, factory, repository);
                }
                if (intersectable1 is SmallCircle && intersectable2 is SmallCircle)
                {
                    command = new SmallCircleSmallCircleIntersectCommand(intersectable1 as SmallCircle, intersectable2 as SmallCircle, factory, repository);
                }
                if (command != null)
                {
                    commandInvoker.ExecuteCommand(command);
                    manager.SetState(this);
                }
            }
        }
    }

    public override void OnEnter()
    {
        manager.OnDown += OnDown;
    }

    public override void OnExit()
    {
        manager.OnDown -= OnDown;
        intersectable1 = null;
        intersectable2 = null;
    }
}
