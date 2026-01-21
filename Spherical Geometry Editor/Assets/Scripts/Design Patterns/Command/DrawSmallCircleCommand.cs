using UnityEngine;

public class DrawSmallCircleCommand : ICommand
{
    private GameObject point1;
    private GameObject point2;
    private SmallCircle smallCircle;

    public DrawSmallCircleCommand(SmallCircle greatCircleSegment, GameObject point1, GameObject point2)
    {
        this.smallCircle = greatCircleSegment;
        this.point1 = point1;
        this.point2 = point2;
    }

    public void Execute()
    {
        ParametricCurveMeshGenerator.Instance.CreateSmallCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, smallCircle.CreateMesh);
        smallCircle.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
    }

    public void UnExecute()
    {
        smallCircle.Destroy();
    }
}
