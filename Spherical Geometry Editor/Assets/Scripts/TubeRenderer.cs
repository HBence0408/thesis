using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TubeRenderer : MonoBehaviour
{
    public delegate double SphericalLineParametricEq(double radius, double t, double pointACoord, double pointBCoord);

    [SerializeField] private int subdivisions = 10;
    [SerializeField] private int extrudes = 3;
    [SerializeField] private float radius = 5;
	[SerializeField] private GameObject parametricCurvePrefab;
	[SerializeField] private ParametricCurve parametricCurveScript;
    private static TubeRenderer instance;

	public static TubeRenderer Instance
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
			instance = new TubeRenderer();
		}
		else
		{
			Debug.LogWarning("multiple tube renderer singleton, deleting self");
			Destroy(this.gameObject);
		}
    }

	public void Draw(double r, Vector3 point1, Vector3 point2, SphericalLineParametricEq x, SphericalLineParametricEq y, SphericalLineParametricEq z)
	{
        float angleStep = 2f * Mathf.PI / subdivisions;

		GameObject parametricCurve = Instantiate(parametricCurvePrefab);
        this.parametricCurveScript = parametricCurve.GetComponent<ParametricCurve>();

		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();

        for (int i = 0; i < subdivisions; i++)
        {
			// ezek majd delegate-ekkel lesznek
            float xPosition = 0 + radius * Mathf.Cos(angleStep * i) * point1.x + Mathf.Sin(angleStep * i) * point2.x;
            float yPosition = 0 + radius * Mathf.Cos(angleStep * i) * point1.y + Mathf.Sin(angleStep * i) * point2.y;
            float zPosition = 0 + radius * Mathf.Cos(angleStep * i) * point1.z + Mathf.Sin(angleStep * i) * point2.z;

            Vector3 pointInCircle = new Vector3(xPosition, yPosition, zPosition);

			//itt ki kéne extrude-olni több irányba aztán hozzá adni a vertices-be õket
           
        }


		//  itt újjabb for amiben a vertex-eket õszzekötni háromszögekbe úgyh hogy jó legyen


		parametricCurveScript.CreateMesh(vertices.ToArray(), triangles.ToArray());
    }
}
