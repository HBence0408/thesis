using UnityEngine;

public class CircleDrawMode : DrawingMode
{
    public new void SetUp(GameObject prefab)
    {
        base.SetUp(prefab);
        ControllPoints = 2;
    }

    public override ICommand Draw(GameObject[] controllPoints)
    {
        GameObject parametricCurve = Instantiate(prefab);
        parametricCurve.transform.position = new Vector3(0, 0, 0);
        SmallCircle script = parametricCurve.GetComponent<SmallCircle>();
        DrawSmallCircleCommand command = new DrawSmallCircleCommand(script, controllPoints[0], controllPoints[1]);
        return command;
    }
}
