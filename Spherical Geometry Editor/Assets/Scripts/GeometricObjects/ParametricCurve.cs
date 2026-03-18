using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public abstract class ParametricCurve : MonoBehaviour, IObserver, IObservable
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshCollider meshCollider;
    private Vector3[] pointsOnCurve;
    protected ControllPoint point1;
    protected ControllPoint point2;
    protected List<IObserver> observers = new List<IObserver>();
    private Vector3 normaleOfPlane;
    private Vector3[] orthogonalVectrosOfthePlane = new Vector3[2];
    private Vector3 center;

    public void CreateMesh(Vector3[] vertices, int[] triangles, Vector3[] pointsOnCurve, Vector3 normalOfPlane, Vector3 u, Vector3 v, Vector3 center)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;
        this.pointsOnCurve = pointsOnCurve;
        meshCollider.sharedMesh = mesh;
        this.normaleOfPlane = normalOfPlane;
        this.orthogonalVectrosOfthePlane[0] = u;
        this.orthogonalVectrosOfthePlane[1] = v;
        this.center = center;
      //  Debug.Log(center);
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

    public Vector3 Center
    {
        get { return center; }
        //set { center = value; }
    }

    public Vector3[] PointsOnCurve
    {
        get
        {
            return pointsOnCurve;
        }
    }

    public Vector3 NormalOfPlane
    {
        get { return normaleOfPlane; }
    }


    public Vector3[] OrthogonalVectrosOfthePlane 
    { 
        get 
        { 
            return orthogonalVectrosOfthePlane;
        } 
    }

    public abstract void OnChanged();

    public void Subscirbe(IObserver o)
    {
        observers.Add(o);
    }

    public void Unsubscirbe(IObserver o)
    {
        observers.Remove(o);
    }

    public void Notify()
    {
        foreach (IObserver o in observers)
        {
            o.OnChanged();
        }
    }
    //{
    //    ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, this.CreateMesh);
    //}

    //private void Update()
    //{
    //    //ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, this.CreateMesh);
    //}
    // ha abstract scriptet akarunk használni akkor "does not fall with in range" hibát kapunk
}
