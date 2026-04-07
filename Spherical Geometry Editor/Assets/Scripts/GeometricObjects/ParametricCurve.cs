using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public abstract class ParametricCurve : MonoBehaviour, IObserver, IObservable, IGeometryObject
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshCollider meshCollider;
    private Vector3[] pointsOnCurve;
    protected ControllPoint point1;
    protected ControllPoint point2;
    protected List<IObserver> observers = new List<IObserver>();
    protected Vector3 normaleOfPlane;
    protected Vector3[] orthogonalVectrosOfthePlane = new Vector3[2];
    protected Vector3 center;
    private Guid id = Guid.Empty;
    private bool isActive = true;
    private Color color = Color.red;

    public Guid Id
    {
        get => id;
        set
        {
            if (id == Guid.Empty)
            {
                id = value;
            }
        }
    }

    public Guid ControllPoint1 => point1.Id;
    public Vector3 ControllPoint1Pos => point1.Position;

    public Guid ControllPoint2 => point2.Id;
    public Vector3 ControllPoint2Pos => point2.Position;

    public Color Color => color;

    public bool IsActive => isActive;

    public List<Guid> Observers
    {
        get
        {
            List<Guid> observerIds = new List<Guid>();
            foreach (IObserver o in observers)
            {
                if (o is IGeometryObject)
                {
                    observerIds.Add(((IGeometryObject)(o)).Id);
                }
            }
            return observerIds;
        }
    }

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
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
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

    public abstract Vector3 GetClosestPoint(Vector3 pos);
   
        //Vector3 closestPoint = pointsOnCurve[0];
        //float minDistance = Vector3.Distance(pos, closestPoint);
        //foreach (Vector3 point in pointsOnCurve)
        //{
        //    float distance = Vector3.Distance(pos, point);
        //    if (distance < minDistance)
        //    {
        //        minDistance = distance;
        //        closestPoint = point;
        //    }
        //}
        //return closestPoint;

        //float distance = normaleOfPlane.x * pos.x + normaleOfPlane.y * pos.y + normaleOfPlane.z * pos.z - (normaleOfPlane.x * center.x - normaleOfPlane.y * center.y - normaleOfPlane.z * center.z);
        //distance = distance / normaleOfPlane.magnitude;
        //return pos - distance * normaleOfPlane.normalized;

    

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
        if (!isActive)
        {
            return;
        }
        foreach (IObserver o in observers)
        {
            o.OnChanged();
        }
    }

    public void SoftDelete()
    {
        foreach (IObserver o in observers)
        {
            if (o is IGeometryObject)
            {
                ((IGeometryObject)(o)).SoftDelete();
                Debug.Log("soft deleting observer");
            }
        }
        isActive = false;
        this.gameObject.SetActive(false);
    }

    public void HardDelete()
    {
        foreach (IObserver o in observers)
        {
            if (o is IGeometryObject)
            {
                ((IGeometryObject)(o)).HardDelete();
            }
        }
        Destroy(this.gameObject);
    }

    public void Restore()
    {
        foreach (IObserver o in observers)
        {
            if (o is IGeometryObject)
            {
                ((IGeometryObject)(o)).Restore();
            }
            o.OnChanged();
        }
        isActive = true;
        this.gameObject.SetActive(true);
    }

    private void OnMouseOver()
    {
        if (!isActive) 
        { 

        }
    }

    public void Highlight()
    {
        meshRenderer.material.color = Color.yellow;
    }

    public void UnHighlight()
    {
        meshRenderer.material.color = this.color;
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }
}
