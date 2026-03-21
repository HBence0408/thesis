using UnityEngine;

public class GreatCircleDrawState : DrawParametricCurveState
{

    public GreatCircleDrawState(DrawManager manager, SphericalGeometryFactory factory, CommandInvoker commandInvoker) : base(manager, factory, commandInvoker)
    {
        requiredControllPoints = 2;
    }

    protected override void DrawParametricCurve()
    {

        DrawGreatCircleCommand command = new DrawGreatCircleCommand(SelectedControllPoints[0], SelectedControllPoints[1], factory);
        commandInvoker.ExecuteCommand (command);
    }
}

