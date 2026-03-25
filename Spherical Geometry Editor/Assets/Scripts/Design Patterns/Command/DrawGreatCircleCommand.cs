using UnityEditor;
using UnityEngine;

public class DrawGreatCircleCommand :ICommand
{
    private GameObject point1;
    private GameObject point2;
    private GreatCircle greatCircle;
    private SphericalGeometryFactory factory;
    private IRepository repository;
    private bool isExecuted;

    public DrawGreatCircleCommand(GameObject point1, GameObject point2, SphericalGeometryFactory factory, IRepository repoitory)
    { 
        this.point1 = point1;
        this.point2 = point2;
        this.factory = factory;
        this.repository = repoitory;
    }

    public void Execute()
    {
        greatCircle = factory.CreateGreatCircle(point1.transform.position.normalized, point2.transform.position.normalized);
        greatCircle.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
        isExecuted = true;
        repository.Store(greatCircle);
    }

    public void ReExecute()
    {
        greatCircle.Restore();
        isExecuted = true;
        repository.Store(greatCircle);
    }

    public void UnExecute()
    {
        greatCircle.SoftDelete();
        isExecuted = false;
        repository.Delete(greatCircle.Id);
    }

    public void Delete()
    {
        if (!isExecuted && greatCircle != null)
        {
            greatCircle.HardDelete();
        }
        point1 = null;
        point2 = null;
        greatCircle = null;
        factory = null;
        repository = null;
    }
}
