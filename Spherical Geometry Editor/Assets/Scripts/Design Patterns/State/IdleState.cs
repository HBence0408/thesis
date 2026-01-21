using UnityEngine;

public class IdleState : DrawingState
{
    public override void OnEnter(DrawingMode mode)
    {
        base.OnEnter(mode);
        manager.ClearControllPoints();
    }
}
