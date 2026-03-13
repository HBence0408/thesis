using UnityEngine;

public class DrawSmallCircleCommand : ICommand
{
    private GameObject point1;
    private GameObject point2;
    private SmallCircle smallCircle;
    private GameObject prefab;

    public DrawSmallCircleCommand(GameObject point1, GameObject point2, GameObject prefab)
    {
        this.point1 = point1;
        this.point2 = point2;
        this.prefab = prefab;
    }

    public void Execute()
    {
        GameObject parametricCurve = MonoBehaviour.Instantiate(prefab);
        parametricCurve.transform.position = new Vector3(0, 0, 0);
        smallCircle = parametricCurve.GetComponent<SmallCircle>();
        ParametricCurveMeshGenerator.Instance.CreateSmallCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, smallCircle.CreateMesh);
        smallCircle.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
    }

    public void UnExecute()
    {
        smallCircle.Destroy();
    }
}