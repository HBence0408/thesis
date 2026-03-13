using UnityEngine;

public class GreatCircleSegmentDrawState : DrawParametricCurveState
{
    public GreatCircleSegmentDrawState(DrawManager manager, GameObject curvePrefab, GameObject pointPrefab) : base(manager, curvePrefab, pointPrefab)
    {
        requiredControllPoints = 2;
    }

    protected override void DrawParametricCurve()
    {
        DrawGreatCircleSegmentCommand command = new DrawGreatCircleSegmentCommand(SelectedControllPoints[0], SelectedControllPoints[1], curvePrefab);
        manager.ExecuteCommand(command);
    }
}
