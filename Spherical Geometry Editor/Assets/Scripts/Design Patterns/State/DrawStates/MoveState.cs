using UnityEngine;
using UnityEngine.UIElements;

public class MoveState : DrawingState
{
    private ControllPoint point = null;
    private Vector3? pos = null;
    private ICommandInvoker commandInvoker;

    public MoveState(IDrawManager manager, ICommandInvoker commandInvoker) : base(manager) 
    {
        this.commandInvoker = commandInvoker;
    }

    private void OnDown(IGeometryObject geometryObject, Vector3 hitpoint)
    {
        if (geometryObject is ControllPoint)
        {
            pos = hitpoint;
            this.point = geometryObject as ControllPoint;

        }
    }

    private void OnHold(IGeometryObject geometryObject, Vector3 hitpoint)
    {
        if (point != null && pos != null)
        {
            point.Reposition(hitpoint.normalized);
        }
    }
    

    private void OnUp(IGeometryObject geometryObject, Vector3 hitpoint)
    {
        if (point is not null && pos is not null)
        {
            Vector3 currentPos = point.transform.position;
            MovePointCommand command = new MovePointCommand(currentPos, point, (Vector3)pos);
            commandInvoker.ExecuteCommand(command);
            manager.SetState(this);
        }      
    }

    public override void OnEnter()
    {
        manager.OnDown += OnDown;
        manager.OnUp += OnUp;
        manager.OnHold += OnHold;
    }

    public override void OnExit()
    {
        manager.OnDown -= OnDown;
        manager.OnUp -= OnUp;
        manager.OnHold -= OnHold;
        point = null;
        pos = null;
    }
}
