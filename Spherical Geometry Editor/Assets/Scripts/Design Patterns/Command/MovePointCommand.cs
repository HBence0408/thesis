using UnityEngine;

public class MovePointCommand : ICommand
{
    private Vector3 toPos;
    private Vector3 fromPos;
    private GameObject point;

    public MovePointCommand(Vector3 toPos, GameObject point, Vector3 fromPos)
    {
        this.toPos = toPos.normalized;
        this.fromPos = fromPos.normalized;
        this.point = point;
    }

    public void Execute()
    {
        point.transform.position = toPos;
    }

    public void UnExecute()
    {
        point.transform.position = fromPos;
    }
}
