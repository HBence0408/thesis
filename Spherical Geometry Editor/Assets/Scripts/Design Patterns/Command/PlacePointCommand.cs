using Unity.VisualScripting;
using UnityEngine;

public class PlacePointCommand : ICommand
{
    private Vector3 pos;
    private ControllPoint point;

    public PlacePointCommand(Vector3 pos, ControllPoint point)
    {
        this.pos = pos.normalized;
        this.point = point;
    }

    public void Execute()
    {
        point.transform.position = pos;
    }

    public void UnExecute()
    {
        point.Destroy();
    }
}
