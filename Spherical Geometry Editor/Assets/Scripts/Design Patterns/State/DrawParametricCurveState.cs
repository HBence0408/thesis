using UnityEngine;

public class DrawParametricCurveState : DrawingState
{
    //public override void OnLeftMouseUp()
    //{
    //    Draw();
    //    manager.ClearControllPoints();
    //    manager.SetState(manager.IdleState, null);
    //}

    public override void OnEnter(DrawingMode mode)
    {
        base.OnEnter(mode);
        Draw();
        manager.ClearControllPoints();
        manager.SetState(manager.IdleState, null);
    }

    private void Draw()
    {
        ICommand command = drawingMode.Draw(manager.ControllPoints());
        manager.ExecuteCommand(command);
    }
}
