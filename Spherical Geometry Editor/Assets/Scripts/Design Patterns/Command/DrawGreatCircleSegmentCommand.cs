using UnityEngine;

public class DrawGreatCircleSegmentCommand : ICommand
{
    private GameObject point1;
    private GameObject point2;
    private GreatCircleSegment greatCircleSegment;

    public DrawGreatCircleSegmentCommand(GreatCircleSegment greatCircleSegment, GameObject point1, GameObject point2)
    {
        this.greatCircleSegment = greatCircleSegment;
        this.point1 = point1;
        this.point2 = point2;
    }

    public void Execute()
    {
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleSegmentMesh(point1.transform.position.normalized, point2.transform.position.normalized, greatCircleSegment.CreateMesh);
        greatCircleSegment.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
    }

    public void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}
