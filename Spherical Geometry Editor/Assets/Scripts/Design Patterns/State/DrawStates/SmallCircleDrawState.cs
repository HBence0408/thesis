using UnityEngine;

public class SmallCircleDrawState : DrawParametricCurveState
{
    public SmallCircleDrawState(IDrawManager manager, ISphericalGeometryFactory factory, ICommandInvoker commandInvoker, IRepository repository) : base(manager, factory, commandInvoker, repository)
    {
        requiredControllPoints = 2;
    }

    protected override void DrawParametricCurve()
    {
        if (SelectedControllPoints[0].transform.position == SelectedControllPoints[1].transform.position || SelectedControllPoints[0].transform.position == -SelectedControllPoints[1].transform.position)
        {
            return;
        }

        DrawSmallCircleCommand command = new DrawSmallCircleCommand(SelectedControllPoints[0], SelectedControllPoints[1], factory, repository);
        commandInvoker.ExecuteCommand(command);
    }
}
