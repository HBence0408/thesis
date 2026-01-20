using UnityEngine;

public class TestDrawer : MonoBehaviour
{
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;
    [SerializeField] private GameObject parametricCurvePrefab;
    [SerializeField] private GameObject GreatCirclePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            

            DrawLine(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
            
            //ParametricCurveMeshGenerator.Instance.CreateSmallCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized);
        }
    }

    //GameObject parametricCurve = Instantiate(parametricCurvePrefab);
    //this.parametricCurveScript = parametricCurve.GetComponent<ParametricCurve>();
    //parametricCurve.transform.position = new Vector3(0, 0, 0);

    private void DrawLine(ControllPoint point1, ControllPoint point2)
    {
        GameObject parametricCurve = Instantiate(GreatCirclePrefab);
        parametricCurve.transform.position = new Vector3(0, 0, 0);
        ParametricCurve script = parametricCurve.GetComponent<GreatCircle>();
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, script.CreateMesh);
        script.AddContollPoints(point1, point2);
    }

    private void DrawSegment()
    {

    } 

    private void DrawCircle()
    {

    }

    private void PlacePoint(GameObject point)
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");
            point.transform.position = hit.point;
            point.transform.position = point.transform.position.normalized;
        }
    }

}
