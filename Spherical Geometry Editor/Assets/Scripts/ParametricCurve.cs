using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ParametricCurve : MonoBehaviour, Observer
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    public Vector3[] PointsInCircle;
    private ControllPoint point1;
    private ControllPoint point2;

    public void CreateMesh(Vector3[] vertices, int[] triangles, Vector3[] pointsInCircle)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;
        PointsInCircle = pointsInCircle;
    }

    public void AddContollPoints(ControllPoint point1, ControllPoint point2)
    {
        this.point1 = point1;
        this.point2 = point2;
    }

    public void OnChanged()
    {
        
    }
}
