using UnityEditor;
using UnityEngine;

public class DrawGreatCircleCommand :ICommand
{
    private GameObject point1;
    private GameObject point2;
    private GreatCircle greatCircle;
    private GameObject prefab;

    public DrawGreatCircleCommand(GameObject point1, GameObject point2, GameObject prefab)
    { 
        this.point1 = point1;
        this.point2 = point2;
        this.prefab = prefab;
    }

    public void Execute()
    {
        GameObject parametricCurve = MonoBehaviour.Instantiate(prefab);
        parametricCurve.transform.position = new Vector3(0, 0, 0);
        greatCircle = parametricCurve.GetComponent<GreatCircle>();
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, greatCircle.CreateMesh);
        greatCircle.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
    }

    public void UnExecute()
    {
        greatCircle.Destroy();
    }
}
