using UnityEngine;

public class DrawSmallCircleCommand : ICommand
{
    private ControllPoint point1;
    private ControllPoint point2;
    private SmallCircle smallCircle;

    public DrawSmallCircleCommand(SmallCircle greatCircleSegment, ControllPoint point1, ControllPoint point2)
    {
        this.smallCircle = greatCircleSegment;
        this.point1 = point1;
        this.point2 = point2;
    }

    public void Execute()
    {
        ParametricCurveMeshGenerator.Instance.CreateSmallCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, smallCircle.CreateMesh);
    }

    public void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}
