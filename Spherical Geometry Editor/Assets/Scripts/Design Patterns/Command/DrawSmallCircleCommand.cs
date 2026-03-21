using UnityEngine;

public class DrawSmallCircleCommand : ICommand
{
    private GameObject point1;
    private GameObject point2;
    private SmallCircle smallCircle;
    private SphericalGeometryFactory factory;

    public DrawSmallCircleCommand(GameObject point1, GameObject point2, SphericalGeometryFactory factory)
    {
        this.point1 = point1;
        this.point2 = point2;
        this.factory = factory;
    }

    public void Execute()
    {
        smallCircle = factory.CreateSmallCircle(point1.transform.position.normalized, point2.transform.position.normalized);
        smallCircle.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
    }

    public void UnExecute()
    {
        smallCircle.Destroy();
    }
}