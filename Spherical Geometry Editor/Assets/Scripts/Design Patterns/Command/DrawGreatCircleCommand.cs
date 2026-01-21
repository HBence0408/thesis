using UnityEditor;
using UnityEngine;

public class DrawGreatCircleCommand :ICommand
{
    private GameObject point1;
    private GameObject point2;
    private GreatCircle greatCircle;

    public DrawGreatCircleCommand(GreatCircle greatCircle ,GameObject point1, GameObject point2)
    { 
        this.greatCircle = greatCircle;
        this.point1 = point1;
        this.point2 = point2;
    }

    public void Execute()
    {
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, greatCircle.CreateMesh);
        greatCircle.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
    }

    public void UnExecute()
    {
        greatCircle.Destroy();
    }
}
