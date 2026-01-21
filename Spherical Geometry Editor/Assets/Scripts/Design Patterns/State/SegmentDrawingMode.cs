using UnityEngine;

public class SegmentDrawingMode : DrawingMode
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
        GreatCircleSegment script = parametricCurve.GetComponent<GreatCircleSegment>();
        DrawGreatCircleSegmentCommand command = new DrawGreatCircleSegmentCommand(script, controllPoints[0], controllPoints[1]);
        return command;
    }
}
