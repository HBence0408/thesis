using UnityEngine;

public class GreatCircleSegmentDrawState : DrawParametricCurveState
{
    public GreatCircleSegmentDrawState(DrawManager manager, SphericalGeometryFactory factory, CommandInvoker commandInvoker) : base(manager, factory, commandInvoker)
    {
        requiredControllPoints = 2;
    }

    protected override void DrawParametricCurve()
    {
        DrawGreatCircleSegmentCommand command = new DrawGreatCircleSegmentCommand(SelectedControllPoints[0], SelectedControllPoints[1], factory);
        commandInvoker.ExecuteCommand(command);
    }
}
