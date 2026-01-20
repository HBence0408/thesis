using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Projector : MonoBehaviour
{

    [SerializeField] private GameObject projectedObjPrefab;
    [SerializeField] private ParametricCurve curveToProject;
    [SerializeField] private Transform projectedPlane;
    [SerializeField] private GameObject origo;

    private static Projector instance;

    public static Projector Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("multiple projector singleton, deleting self");
            Destroy(this.gameObject);
        }
    }

    public void StereographicProject(ParametricCurve curve)
    {
        Vector3[] pointsToProject = curve.PointsInCircle;
        Vector3[] projectedPoints = new Vector3[pointsToProject.Length];
        for (int i = 0; i < pointsToProject.Length; i++)
        {
            Vector3 projectedPoint = new Vector3(pointsToProject[i].x /( 1 - pointsToProject[i].z) * 50, pointsToProject[i].y /( 1 - pointsToProject[i].z) * 50, 1);
            projectedPoints[i] = projectedPoint;
        }

        Object obj = Instantiate(projectedObjPrefab);
    
        obj.GetComponent<Transform>().parent = projectedPlane;
        ProjectedParametricCurve script = obj.GetComponent<ProjectedParametricCurve>();
        script.SetPoints(projectedPoints);
    }

    public void StereographicProject2(ParametricCurve curve)
    {
        Vector3[] pointsToProject = curve.PointsInCircle;
        Vector3[] projectedPoints = new Vector3[pointsToProject.Length];
        for (int i = 0; i < pointsToProject.Length; i++)
        {
            Vector3 projectedPoint = new Vector3(pointsToProject[i].x / ( 2 - pointsToProject[i].z) * 100, pointsToProject[i].y / ( 2 - pointsToProject[i].z) * 100, 1);
            projectedPoints[i] = projectedPoint;
        }

        Object obj = Instantiate(projectedObjPrefab);

        obj.GetComponent<Transform>().parent = projectedPlane;
        ProjectedParametricCurve script = obj.GetComponent<ProjectedParametricCurve>();
        script.SetPoints(projectedPoints);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    Projector.Instance.StereographicProject(curveToProject);
        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Projector.Instance.StereographicProject2(curveToProject);
        //}
    }
}
