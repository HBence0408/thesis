using UnityEngine;

public class MovePointCommand : ICommand
{
    private Vector3 toPos;
    private Vector3 fromPos;
    private ControllPoint point;

    public MovePointCommand(Vector3 toPos, ControllPoint point, Vector3 fromPos)
    {
        this.toPos = toPos.normalized;
        this.fromPos = fromPos.normalized;
        this.point = point;
    }

    public void Execute()
    {
        point.Reposition(toPos);
    }

    public void ReExecute()
    {
        point.Reposition(toPos);
    }

    public void UnExecute()
    {
        point.Reposition(fromPos);
    }

    public void Delete()
    {
        point = null;
    }
}
