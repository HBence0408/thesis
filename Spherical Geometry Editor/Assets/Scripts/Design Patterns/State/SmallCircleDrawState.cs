using UnityEngine;

public class SmallCircleDrawState : DrawParametricCurveState
{

    public SmallCircleDrawState(DrawManager manager, GameObject curvePrefab, GameObject pointPrefab) : base(manager, curvePrefab, pointPrefab)
    {
        requiredControllPoints = 2;
    }

    protected override void DrawParametricCurve()
    {
        DrawSmallCircleCommand command = new DrawSmallCircleCommand(SelectedControllPoints[0], SelectedControllPoints[1], curvePrefab);
        manager.ExecuteCommand(command);
    }
}
