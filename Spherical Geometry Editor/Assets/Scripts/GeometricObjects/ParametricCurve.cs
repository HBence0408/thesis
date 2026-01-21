using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParametricCurve : MonoBehaviour, IObserver
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    public Vector3[] PointsInCircle;
    protected ControllPoint point1;
    protected ControllPoint point2;

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
        this.point1.Subscirbe(this);
        this.point2.Subscirbe(this);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public abstract void OnChanged();
    //{
    //    ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, this.CreateMesh);
    //}

    //private void Update()
    //{
    //    //ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, this.CreateMesh);
    //}
    // ha abstract scriptet akarunk használni akkor "does not fall with in range" hibát kapunk
}
