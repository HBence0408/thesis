using UnityEngine;

public class TestDrawer : MonoBehaviour
{
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;
    [SerializeField] private GameObject parametricCurvePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            

            DrawLine(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
            
            //ParametricCurveMeshGenerator.Instance.CreateSmallCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized);
        }
    }

    //GameObject parametricCurve = Instantiate(parametricCurvePrefab);
    //this.parametricCurveScript = parametricCurve.GetComponent<ParametricCurve>();
    //parametricCurve.transform.position = new Vector3(0, 0, 0);

    void DrawLine(ControllPoint point1, ControllPoint point2)
    {
        GameObject parametricCurve = Instantiate(parametricCurvePrefab);
        parametricCurve.transform.position = new Vector3(0, 0, 0);
        ParametricCurve script = parametricCurve.GetComponent<ParametricCurve>();
        ParametricCurveMeshGenerator.Instance.CreateGreatCircrcleMesh(point1.transform.position.normalized, point2.transform.position.normalized, script.CreateMesh);
        point1.Subscirbe(script);
        point2.Subscirbe(script);
    }

    void DrawSegment()
    {

    } 

    void DrawCircle()
    {

    }

}
