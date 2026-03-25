using UnityEngine;

public class DrawSmallCircleCommand : ICommand
{
    private GameObject point1;
    private GameObject point2;
    private SmallCircle smallCircle;
    private SphericalGeometryFactory factory;
    IRepository repository;
    private bool isExecuted;

    public DrawSmallCircleCommand(GameObject point1, GameObject point2, SphericalGeometryFactory factory, IRepository repository)
    {
        this.point1 = point1;
        this.point2 = point2;
        this.factory = factory;
        this.repository = repository;
    }

    public void Execute()
    {
        smallCircle = factory.CreateSmallCircle(point1.transform.position.normalized, point2.transform.position.normalized);
        smallCircle.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
        isExecuted = true;
        repository.Store(smallCircle);
    }

    public void ReExecute()
    {
        smallCircle.Restore();
        isExecuted = true;
        repository.Store(smallCircle);
    }

    public void UnExecute()
    {
        smallCircle.SoftDelete();
        isExecuted = false;
        repository.Delete(smallCircle.Id);
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
        repository = null;
    }
}