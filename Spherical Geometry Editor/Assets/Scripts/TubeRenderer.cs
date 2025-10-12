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
    [SerializeField] private GameObject pointTest;

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
			instance = this;
		}
		else
		{
			Debug.LogWarning("multiple tube renderer singleton, deleting self");
			Destroy(this.gameObject);
		}
    }

	public void Draw(double r, Vector3 point1, Vector3 point2/*, SphericalLineParametricEq x, SphericalLineParametricEq y, SphericalLineParametricEq z*/)
	{
        float angleStep = 2f * Mathf.PI / subdivisions;

        Vector3 v1 = point1;
        Vector3 v2 = point2;

        Vector3 normalOfPlane = Vector3.Cross(point1, point2);

        normalOfPlane.Normalize();

        Vector3 u = v1.normalized;
        Vector3 v = Vector3.Cross(normalOfPlane, u);
		v.Normalize();

        //GameObject parametricCurve = Instantiate(parametricCurvePrefab);
        //this.parametricCurveScript = parametricCurve.GetComponent<ParametricCurve>();

        List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();

		Vector3 PreviousPointInCircle;

        float FirstxPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.x + Mathf.Sin(angleStep * subdivisions - 1) * v.x;
        float FirstyPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.y + Mathf.Sin(angleStep * subdivisions -1) * v.y;
        float FirstzPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.z + Mathf.Sin(angleStep * subdivisions -1) * v.z;

        PreviousPointInCircle = new Vector3(FirstxPosition, FirstyPosition, FirstzPosition);


        for (int i = 0; i < subdivisions; i++)
        {
			// ezek majd delegate-ekkel lesznek
            float xPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.x + Mathf.Sin(angleStep * i) * v.x;
            float yPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.y + Mathf.Sin(angleStep * i) * v.y;
            float zPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.z + Mathf.Sin(angleStep * i) * v.z;

            Vector3 pointInCircle = new Vector3(xPosition, yPosition, zPosition);

			//itt ki kéne extrude-olni több irányba aztán hozzá adni a vertices-be õket
			// a normál vektroól meg van a sík
			//azon a sikon a kör és hasonlóan mint itt megrajzolni és a ponotkat avertexbe tenni



			for (int j = 0; j < extrudes; j++)
			{
                float extrudeStep = 2f * Mathf.PI / extrudes;

                Vector3 n = pointInCircle - PreviousPointInCircle;
				Vector3 a = new Vector3(PreviousPointInCircle.z - pointInCircle.z, 0, -(PreviousPointInCircle.x - pointInCircle.x)) + pointInCircle;
				Vector3 b = Vector3.Cross(n, a);
				a.Normalize();
				b.Normalize();

                float vertexXPosition = pointInCircle.x + 0.2f * Mathf.Cos(extrudeStep * j) * a.x + Mathf.Sin(extrudeStep * j) * b.x;
                float vertexYPosition = pointInCircle.y + 0.2f * Mathf.Cos(extrudeStep * j) * a.y + Mathf.Sin(extrudeStep * j) * b.y;
                float vertexZPosition = pointInCircle.z + 0.2f * Mathf.Cos(extrudeStep * j) * a.z + Mathf.Sin(extrudeStep * j) * b.z;

                Vector3 vertex = new Vector3(vertexXPosition, vertexYPosition, vertexZPosition);


                //teszt
                GameObject obj = Instantiate(pointTest);
                obj.transform.position = vertex;

            }

			PreviousPointInCircle = pointInCircle;
        }


		//  itt újjabb for amiben a vertex-eket õszzekötni háromszögekbe úgyh hogy jó legyen
		// ki számolni melyik indexwk melyikkel vannak


		//parametricCurveScript.CreateMesh(vertices.ToArray(), triangles.ToArray());
    }
}