using UnityEngine;

public class DrawSmallCircleCommand : ICommand
{
    private GameObject point1;
    private GameObject point2;
    private SmallCircle smallCircle;
    private SphericalGeometryFactory factory;
    private bool isExecuted;

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
        isExecuted = true;
    }

    public void ReExecute()
    {
        smallCircle.Restore();
        isExecuted = true;
    }

    public void UnExecute()
    {
        smallCircle.SoftDelete();
        isExecuted = false;
    }

    public void Delete()
    {
        if (!isExecuted && smallCircle != null)
        {
            smallCircle.HardDelete();
        }
        point1 = null;
        point2 = null;
        smallCircle = null;
        factory = null;
    }
}