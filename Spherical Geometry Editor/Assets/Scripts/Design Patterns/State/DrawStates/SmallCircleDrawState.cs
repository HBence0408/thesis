using UnityEngine;

public class SmallCircleDrawState : DrawParametricCurveState
{
    public SmallCircleDrawState(DrawManager manager, SphericalGeometryFactory factory, CommandInvoker commandInvoker) : base(manager, factory, commandInvoker)
    {
        requiredControllPoints = 2;
    }

    protected override void DrawParametricCurve()
    {
        DrawSmallCircleCommand command = new DrawSmallCircleCommand(SelectedControllPoints[0], SelectedControllPoints[1], factory);
        commandInvoker.ExecuteCommand(command);
    }
}
