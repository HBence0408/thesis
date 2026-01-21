using UnityEngine;

public class DrawParametricCurveState : DrawingState
{
    public override void OnLeftMouseDown()
    {
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
