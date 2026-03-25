using UnityEngine;

public class DrawGreatCircleSegmentCommand : ICommand
{
    private GameObject point1;
    private GameObject point2;
    private GreatCircleSegment greatCircleSegment;
    private SphericalGeometryFactory factory;
    IRepository repository;
    private bool isExecuted;

    public DrawGreatCircleSegmentCommand(GameObject point1, GameObject point2, SphericalGeometryFactory factory, IRepository repository)
    {
        this.point1 = point1;
        this.point2 = point2;
        this.factory = factory;
        this.repository = repository;
    }

    public void Execute()
    {

        greatCircleSegment = factory.CreateGreatCircleSegment(point1.transform.position.normalized, point2.transform.position.normalized);
        greatCircleSegment.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
        isExecuted = true;
        repository.Store(greatCircleSegment);
    }

    public void ReExecute()
    {
        greatCircleSegment.Restore();
        isExecuted = true;
        repository.Store(greatCircleSegment);
    }

    public void UnExecute()
    {
        greatCircleSegment.SoftDelete();
        isExecuted = false;
        repository.Delete(greatCircleSegment.Id);
    }

    public void Delete()
    {
        if (!isExecuted && greatCircleSegment != null)
        {
            greatCircleSegment.HardDelete();
        }
        point1 = null;
        point2 = null;
        greatCircleSegment = null;
        factory = null;
        repository = null;
    }

}
