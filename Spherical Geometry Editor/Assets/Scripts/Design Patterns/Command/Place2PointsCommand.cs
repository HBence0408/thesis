using UnityEngine;

public class Place2PointsCommand : ICommand
{
    private Vector3 pos1;
    private ControllPoint point1;
    private Vector3 pos2;
    private ControllPoint point2;

    public Place2PointsCommand(Vector3 pos1, ControllPoint point1, Vector3 pos2, ControllPoint point2)
    {
        this.pos1 = pos1;
        this.point1 = point1;
        this.pos2 = pos2;
        this.point2 = point2;
    }

    public void Execute()
    {
        point1.transform.position = pos1;
        point2.transform.position = pos2;
    }

    public void UnExecute()
    {
        point1.Destroy();
        point2.Destroy();
    }
}
