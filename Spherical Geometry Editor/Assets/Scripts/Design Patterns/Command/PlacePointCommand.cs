using UnityEngine;

public class PlacePointCommand : ICommand
{
    private Vector3 pos;
    private GameObject point;

    public PlacePointCommand(Vector3 pos, GameObject point)
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
        throw new System.NotImplementedException();
    }
}
