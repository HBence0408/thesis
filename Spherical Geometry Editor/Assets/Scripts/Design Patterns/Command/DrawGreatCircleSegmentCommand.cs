using UnityEngine;

public class DrawGreatCircleSegmentCommand : ICommand
{
    private GameObject point1;
    private GameObject point2;
    private GreatCircleSegment greatCircleSegment;
    private SphericalGeometryFactory factory;

    public DrawGreatCircleSegmentCommand(GameObject point1, GameObject point2, SphericalGeometryFactory factory)
    {
        this.point1 = point1;
        this.point2 = point2;
        this.factory = factory;
    }

    public void Execute()
    {

        greatCircleSegment = factory.CreateGreatCircleSegment(point1.transform.position.normalized, point2.transform.position.normalized);
        greatCircleSegment.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
    }

    public void UnExecute()
    {
        greatCircleSegment.Destroy();
    }
}
