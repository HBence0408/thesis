using UnityEngine;

public class DrawGreatCircleCommand :ICommand
{
    private ControllPoint point1;
    private ControllPoint point2;
    private GreatCircle greatCircle;

    public DrawGreatCircleCommand(GreatCircle greatCircle ,ControllPoint point1, ControllPoint point2)
    { 
        this.greatCircle = greatCircle;
        this.point1 = point1;
        this.point2 = point2;
    }

    public void Execute()
    {
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, greatCircle.CreateMesh);
        greatCircle.AddContollPoints(point1, point2);
    }

    public void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}
