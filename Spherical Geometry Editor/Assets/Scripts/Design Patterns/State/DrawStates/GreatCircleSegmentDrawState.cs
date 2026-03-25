using UnityEngine;

public class GreatCircleSegmentDrawState : DrawParametricCurveState
{
    public GreatCircleSegmentDrawState(DrawManager manager, SphericalGeometryFactory factory, CommandInvoker commandInvoker, IRepository repository) : base(manager, factory, commandInvoker, repository)
    {
        requiredControllPoints = 2;
    }

    protected override void DrawParametricCurve()
    {
        DrawGreatCircleSegmentCommand command = new DrawGreatCircleSegmentCommand(SelectedControllPoints[0], SelectedControllPoints[1], factory, repository);
        commandInvoker.ExecuteCommand(command);
    }
}
