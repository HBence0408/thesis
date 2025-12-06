using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ParametricCurve : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    public Vector3[] PointsInCircle;

    public void CreateMesh(Vector3[] vertices, int[] triangles, Vector3[] pointsInCircle)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;
        PointsInCircle = pointsInCircle;
    }
}
