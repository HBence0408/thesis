using UnityEngine;

public class LineDrawingMode : DrawingMode
{
    public override ICommand Draw(GameObject[] controllPoints)
    {
        GameObject parametricCurve = Instantiate(prefab);
        parametricCurve.transform.position = new Vector3(0, 0, 0);
        GreatCircle script = parametricCurve.GetComponent<GreatCircle>();
        DrawGreatCircleCommand command = new DrawGreatCircleCommand(script, controllPoints[0], controllPoints[1]);
        return command;
    }
}
