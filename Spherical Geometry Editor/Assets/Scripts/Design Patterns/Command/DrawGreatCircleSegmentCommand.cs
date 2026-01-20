using UnityEngine;

public class DrawGreatCircleSegmentCommand : ICommand
{
    private ControllPoint point1;
    private ControllPoint point2;
    private GreatCircleSegment greatCircleSegment;

    public DrawGreatCircleSegmentCommand(GreatCircleSegment greatCircleSegment, ControllPoint point1, ControllPoint point2)
    {
        this.greatCircleSegment = greatCircleSegment;
        this.point1 = point1;
        this.point2 = point2;
    }

    public void Execute()
    {
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleSegmentMesh(point1.transform.position.normalized, point2.transform.position.normalized, greatCircleSegment.CreateMesh);
    }

    public void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}
