using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParametricCurveMeshGenerator : MonoBehaviour
{
    [SerializeField] private int subdivisions = 10;
    [SerializeField] private int extrudes = 3;
    [SerializeField] private float radius;
	[SerializeField] private GameObject parametricCurvePrefab;
	[SerializeField] private ParametricCurve parametricCurveScript;
    private static ParametricCurveMeshGenerator instance;
    [SerializeField] private GameObject pointTest;
    [SerializeField] private float tubeWidth;

    public delegate void CreateMesh(Vector3[] vertices, int[] triangles, Vector3[] pointsInCircle);

    public static ParametricCurveMeshGenerator Instance
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
			Debug.LogWarning("multiple ParametricCurveMeshGenerator singleton, deleting self");
			Destroy(this.gameObject);
		}
    }

	public void CreateGreatCircleMesh(Vector3 point1, Vector3 point2, CreateMesh callback)
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
        //parametricCurve.transform.position = new Vector3(0, 0, 0);

        List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
        List<Vector3> PointsInCircle = new List<Vector3>();
        
		Vector3 PreviousPointInCircle;

        float FirstxPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.x + radius * Mathf.Sin(angleStep * subdivisions - 1) * v.x;
        float FirstyPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.y + radius * Mathf.Sin(angleStep * subdivisions -1) * v.y;
        float FirstzPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.z + radius * Mathf.Sin(angleStep * subdivisions -1) * v.z;

        PreviousPointInCircle = new Vector3(FirstxPosition, FirstyPosition, FirstzPosition);

        for (int i = 0; i < subdivisions + 1; i++)
        {
            float xPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.x + radius * Mathf.Sin(angleStep * i) * v.x;
            float yPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.y + radius * Mathf.Sin(angleStep * i) * v.y;
            float zPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.z + radius * Mathf.Sin(angleStep * i) * v.z;

            Vector3 pointInCircle = new Vector3(xPosition, yPosition, zPosition);
            PointsInCircle.Add(pointInCircle);

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

                if (i != 0)
                {
                    vertices.Add(vertex);;
                }
            }
			PreviousPointInCircle = pointInCircle;
        }

        int index1;
        int index2;
        int index3;
        int index4;

        for (int i = 0; i < subdivisions - 1; i++)
        {
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

        for (int j = 0; j < extrudes - 1; j++)
        {
            index1 = (subdivisions - 1) * extrudes + j;
            index2 = (index1 % extrudes);
            index3 = index1 + 1;
            index4 = index2 + 1;

            triangles.Add(index3);
            triangles.Add(index2);
            triangles.Add(index1);

            triangles.Add(index4);
            triangles.Add(index2);
            triangles.Add(index3);
        }

        index1 = (subdivisions - 1) * extrudes + extrudes - 1;
        index2 = (index1 % extrudes);
        index3 = index1 + 1 - extrudes;
        index4 = index2 + 1 - extrudes;

        triangles.Add(index3);
        triangles.Add(index2);
        triangles.Add(index1);

        triangles.Add(index4);
        triangles.Add(index2);
        triangles.Add(index3);

        callback?.Invoke(vertices.ToArray(), triangles.ToArray(), PointsInCircle.ToArray());
    }

    public void CreateGreatCircleSegmentMesh(Vector3 point1, Vector3 point2, CreateMesh callback)
    {
        float angle = Vector3.Angle(point1, point2);

        float angleStep = (angle * Mathf.PI/180f )/ subdivisions;

        Vector3 v1 = point1;
        Vector3 v2 = point2;

        Vector3 normalOfPlane = Vector3.Cross(point1, point2);

        normalOfPlane.Normalize();

        Vector3 u = v1.normalized;
        Vector3 v = Vector3.Cross(normalOfPlane, u);
        v.Normalize();

        //GameObject parametricCurve = Instantiate(parametricCurvePrefab);
        //this.parametricCurveScript = parametricCurve.GetComponent<ParametricCurve>();
        //parametricCurve.transform.position = new Vector3(0, 0, 0);

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector3> PointsInCircle = new List<Vector3>();

        Vector3 PreviousPointInCircle;

        float FirstxPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.x + radius * Mathf.Sin(angleStep * subdivisions - 1) * v.x;
        float FirstyPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.y + radius * Mathf.Sin(angleStep * subdivisions - 1) * v.y;
        float FirstzPosition = 0 + radius * Mathf.Cos(angleStep * subdivisions - 1) * u.z + radius * Mathf.Sin(angleStep * subdivisions - 1) * v.z;

        Vector3 firstPoint;
        Vector3 lastPoint;

        PreviousPointInCircle = new Vector3(FirstxPosition, FirstyPosition, FirstzPosition);

        FirstxPosition = 0 + radius * Mathf.Cos(angleStep) * u.x + radius * Mathf.Sin(angleStep) * v.x;
        FirstyPosition = 0 + radius * Mathf.Cos(angleStep) * u.y + radius * Mathf.Sin(angleStep) * v.y;
        FirstzPosition = 0 + radius * Mathf.Cos(angleStep) * u.z + radius * Mathf.Sin(angleStep) * v.z;


        firstPoint = new Vector3(FirstxPosition, FirstyPosition, FirstzPosition);

        for (int i = 0; i < subdivisions + 1; i++)
        {
            float xPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.x + radius * Mathf.Sin(angleStep * i) * v.x;
            float yPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.y + radius * Mathf.Sin(angleStep * i) * v.y;
            float zPosition = 0 + radius * Mathf.Cos(angleStep * i) * u.z + radius * Mathf.Sin(angleStep * i) * v.z;

            Vector3 pointInCircle = new Vector3(xPosition, yPosition, zPosition);
            PointsInCircle.Add(pointInCircle);

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

                if (i != 0)
                {
                    vertices.Add(vertex); ;
                }
            }
            PreviousPointInCircle = pointInCircle;
        }

        lastPoint = PreviousPointInCircle;

        int index1;
        int index2;
        int index3;
        int index4;

        for (int i = 0; i < subdivisions - 1; i++)
        {
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

        // végeget lezárni

        vertices.Add(firstPoint);
        int firstPointIndex = vertices.Count;
        vertices.Add(lastPoint);
        int lastPointIndex = vertices.Count;

        for (int j = 0; j < extrudes - 1; j++)
        {
            index1 = (subdivisions - 1) * extrudes + j;
            //index2 = (index1 % extrudes);
            index2 = index1 + 1;
            index3 = lastPointIndex - 1;
            //index4 = index2 + 1;

            triangles.Add(index1);
            triangles.Add(index2);
            triangles.Add(index3);

            //triangles.Add(index4);
            //triangles.Add(index2);
            //triangles.Add(index3);
        }

        index1 = (subdivisions - 1) * extrudes + extrudes -1;
        index2 = index1 + 1 - extrudes;
        index3 = lastPointIndex - 1;


        triangles.Add(index1);
        triangles.Add(index2);
        triangles.Add(index3);



        for (int j = 0; j < extrudes - 1; j++)
        {
            index1 = 0 * extrudes + j;
            //index2 = (index1 % extrudes);
            index2 = index1 + 1;
            index3 = firstPointIndex - 1;
            //index4 = index2 + 1;

            triangles.Add(index2);
            triangles.Add(index1);
            triangles.Add(index3);

            //triangles.Add(index4);
            //triangles.Add(index2);
            //triangles.Add(index3);
        }

        index1 = 0 * extrudes + extrudes - 1;
        index2 = index1 + 1 - extrudes;
        index3 = firstPointIndex - 1;


        triangles.Add(index2);
        triangles.Add(index1);
        triangles.Add(index3);

        //triangles.Add(index4);
        //triangles.Add(index2);
        //triangles.Add(index3);

        //parametricCurveScript.CreateMesh(vertices.ToArray(), triangles.ToArray(), PointsInCircle.ToArray());
        callback?.Invoke(vertices.ToArray(), triangles.ToArray(), PointsInCircle.ToArray());
    }

    public void CreateSmallCircleMesh(Vector3 point1, Vector3 point2, CreateMesh callback)
    {
        float angleStep = 2f * Mathf.PI / subdivisions;

        Vector3 v1 = point1; // kör "közepe" (gömbiben) 
        Vector3 v3 = point2; // köríven a pont
        Vector3 v2 = new Vector3(0, 0, 0); // a gömb közepe

        //a = dot(P2 - P3, P2 - P1)
        //b = -dot(P1 - P3, P2 - P1)

        float a1 = Vector3.Dot(v2-v3,v2-v1);
        float b1 = -Vector3.Dot(v1-v3, v2-v1);


        //középpontja a körnek (uklidésziben)
        Vector3 v4 = a1*v1 + b1*v2;

        float r = Vector3.Distance(v3, v4);

        Vector3 u = (v3 - v4).normalized;
        Vector3 v = Vector3.Cross((v1 - v4).normalized, u);
        v.Normalize();

       


        //GameObject parametricCurve = Instantiate(parametricCurvePrefab);
        //this.parametricCurveScript = parametricCurve.GetComponent<ParametricCurve>();
        //parametricCurve.transform.position = new Vector3(0, 0, 0);

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector3> PointsInCircle = new List<Vector3>();

        Vector3 PreviousPointInCircle;

        float FirstxPosition = v4.x + r * Mathf.Cos(angleStep * subdivisions - 1) * u.x + r * Mathf.Sin(angleStep * subdivisions - 1) * v.x;
        float FirstyPosition = v4.y + r * Mathf.Cos(angleStep * subdivisions - 1) * u.y + r * Mathf.Sin(angleStep * subdivisions - 1) * v.y;
        float FirstzPosition = v4.z + r * Mathf.Cos(angleStep * subdivisions - 1) * u.z + r * Mathf.Sin(angleStep * subdivisions - 1) * v.z;

        PreviousPointInCircle = new Vector3(FirstxPosition, FirstyPosition, FirstzPosition);

        for (int i = 0; i < subdivisions + 1; i++)
        {
            float xPosition = v4.x + r * Mathf.Cos(angleStep * i) * u.x + r * Mathf.Sin(angleStep * i) * v.x;
            float yPosition = v4.y + r * Mathf.Cos(angleStep * i) * u.y + r * Mathf.Sin(angleStep * i) * v.y;
            float zPosition = v4.z + r * Mathf.Cos(angleStep * i) * u.z + r * Mathf.Sin(angleStep * i) * v.z;

            Vector3 pointInCircle = new Vector3(xPosition, yPosition, zPosition);
            PointsInCircle.Add(pointInCircle);

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

                if (i != 0)
                {
                    vertices.Add(vertex); ;
                }
            }
            PreviousPointInCircle = pointInCircle;
        }

        int index1;
        int index2;
        int index3;
        int index4;

        for (int i = 0; i < subdivisions - 1; i++)
        {
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

        for (int j = 0; j < extrudes - 1; j++)
        {
            index1 = (subdivisions - 1) * extrudes + j;
            index2 = (index1 % extrudes);
            index3 = index1 + 1;
            index4 = index2 + 1;

            triangles.Add(index3);
            triangles.Add(index2);
            triangles.Add(index1);

            triangles.Add(index4);
            triangles.Add(index2);
            triangles.Add(index3);
        }

        index1 = (subdivisions - 1) * extrudes + extrudes - 1;
        index2 = (index1 % extrudes);
        index3 = index1 + 1 - extrudes;
        index4 = index2 + 1 - extrudes;

        triangles.Add(index3);
        triangles.Add(index2);
        triangles.Add(index1);

        triangles.Add(index4);
        triangles.Add(index2);
        triangles.Add(index3);

        // parametricCurveScript.CreateMesh(vertices.ToArray(), triangles.ToArray(), PointsInCircle.ToArray());
        callback?.Invoke(vertices.ToArray(), triangles.ToArray(), PointsInCircle.ToArray());
    }
}