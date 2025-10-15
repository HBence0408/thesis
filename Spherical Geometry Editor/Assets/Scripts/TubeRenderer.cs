using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TubeRenderer : MonoBehaviour
{
    public delegate double SphericalLineParametricEq(double radius, double t, double pointACoord, double pointBCoord);

    [SerializeField] private int subdivisions = 10;
    [SerializeField] private int extrudes = 3;
    [SerializeField] private float radius;
	[SerializeField] private GameObject parametricCurvePrefab;
	[SerializeField] private ParametricCurve parametricCurveScript;
    private static TubeRenderer instance;
    [SerializeField] private GameObject pointTest;
    [SerializeField] private float tubeWidth;

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

        GameObject parametricCurve = Instantiate(parametricCurvePrefab);
        this.parametricCurveScript = parametricCurve.GetComponent<ParametricCurve>();
        parametricCurve.transform.position = new Vector3(0, 0, 0);

        List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
        
		Vector3 PreviousPointInCircle;

        float FirstxPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.x + radius * Mathf.Sin(angleStep * subdivisions - 1) * v.x;
        float FirstyPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.y + radius * Mathf.Sin(angleStep * subdivisions -1) * v.y;
        float FirstzPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.z + radius * Mathf.Sin(angleStep * subdivisions -1) * v.z;

        PreviousPointInCircle = new Vector3(FirstxPosition, FirstyPosition, FirstzPosition);

        GameObject obj = Instantiate(pointTest);
        obj.GetComponent<MeshRenderer>().material.color = Color.black;
        obj.transform.position = PreviousPointInCircle;
        
        for (int i = 0; i < subdivisions; i++)
        {
            // ezek majd delegate-ekkel lesznek
            float xPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.x + radius * Mathf.Sin(angleStep * i) * v.x;
            float yPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.y + radius * Mathf.Sin(angleStep * i) * v.y;
            float zPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.z + radius * Mathf.Sin(angleStep * i) * v.z;

            Vector3 pointInCircle = new Vector3(xPosition, yPosition, zPosition);

            //itt ki kéne extrude-olni több irányba aztán hozzá adni a vertices-be õket
            // a normál vektroól meg van a sík
            //azon a sikon a kör és hasonlóan mint itt megrajzolni és a ponotkat avertexbe tenni

            //LineRenderer lineRenderer = this.gameObject.AddComponent<LineRenderer>();
            obj = Instantiate(pointTest);
            obj.GetComponent<MeshRenderer>().material.color = Color.black;
            obj.transform.position = pointInCircle;

            
            for (int j = 0; j < extrudes; j++)
			{
                float extrudeStep = 2f * Mathf.PI / extrudes;

                Vector3 n = pointInCircle - PreviousPointInCircle;
				Vector3 a = new Vector3(PreviousPointInCircle.z - pointInCircle.z, 0, -(PreviousPointInCircle.x - pointInCircle.x)) + pointInCircle;
				Vector3 b = Vector3.Cross(n, a);
				a.Normalize();
				b.Normalize();

                float vertexXPosition = pointInCircle.x + tubeWidth * Mathf.Cos(extrudeStep * j) * a.x + tubeWidth * Mathf.Sin(extrudeStep * j) * b.x;
                float vertexYPosition = pointInCircle.y + tubeWidth * Mathf.Cos(extrudeStep * j) * a.y + tubeWidth * Mathf.Sin(extrudeStep * j) * b.y;
                float vertexZPosition = pointInCircle.z + tubeWidth * Mathf.Cos(extrudeStep * j) * a.z + tubeWidth * Mathf.Sin(extrudeStep * j) * b.z;

                Vector3 vertex = new Vector3(vertexXPosition, vertexYPosition, vertexZPosition);

                vertices.Add(vertex);

                //teszt
                 obj = Instantiate(pointTest);
                 obj.transform.position = vertex;

                //lineRenderer.SetPosition(j, vertex);
            }
            
			PreviousPointInCircle = pointInCircle;
        }

        //  itt újjabb for amiben a vertex-eket õszzekötni háromszögekbe úgyh hogy jó legyen
        // ki számolni melyik indexwk melyikkel vannak


        for (int i = 0; i < subdivisions - 1; i++)
        {
            int index1;
            int index2;
            int index3;
            int index4;

            for (int j = 0; j < extrudes - 1; j++)
            {
                index1 = i * extrudes + j;
                index2 = index1 + extrudes;
                index3 = index1 + 1;
                index4 = index2 + 1;

                triangles.Add(index3);
                triangles.Add(index2);
                triangles.Add(index1);

                triangles.Add(index4);
                triangles.Add(index2);
                triangles.Add(index3);
            }

            index1 = i * extrudes + extrudes - 1;
            index2 = index1 + extrudes;
            index3 = index1 + 1 - extrudes;
            index4 = index2 + 1 - extrudes;

            triangles.Add(index3);
            triangles.Add(index2);
            triangles.Add(index1);

            triangles.Add(index4);
            triangles.Add(index2);
            triangles.Add(index3);
        }

		parametricCurveScript.CreateMesh(vertices.ToArray(), triangles.ToArray());

    }
}