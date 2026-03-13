using UnityEngine;

public class GreatCircleDrawState : DrawParametricCurveState
{

    public GreatCircleDrawState(DrawManager manager, GameObject curvePrefab, GameObject pointPrefab) : base(manager, curvePrefab, pointPrefab)
    {
        requiredControllPoints = 2;
    }

    protected override void DrawParametricCurve()
    {
        DrawGreatCircleCommand command = new DrawGreatCircleCommand(SelectedControllPoints[0], SelectedControllPoints[1], curvePrefab);
        manager.ExecuteCommand (command);
    }
}

