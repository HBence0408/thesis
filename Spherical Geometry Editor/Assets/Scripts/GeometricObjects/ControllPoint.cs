using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllPoint : MonoBehaviour, IObservable, IGeometryObject
{
    private List<IObserver> observers = new List<IObserver>();
    private Vector3 previousPos;
    private Guid id = Guid.Empty;
    private bool isActive = true;
    [SerializeField] private MeshRenderer meshRenderer;
    private Color color = Color.black;

    public Guid Id { 
        get => id; 
        set 
        {
            if (id == Guid.Empty)
            {
                id = value;
            }
        }
    }

    public bool IsActive => isActive;

    public virtual void Reposition(Vector3 vector3) { }

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

    public void Subscirbe(IObserver o)
    {
        observers.Add(o);
    }

    public void Unsubscirbe(IObserver o)
    {
        observers.Remove(o);
    }

    void Start()
    {
        previousPos = transform.position;
    }

    void Update()
    {
        if (transform.position != previousPos)
        {
            Notify();
            previousPos = transform.position;
        }
    }

    public void Destroy()
    {
       // Destroy(this.gameObject);
       this.gameObject.SetActive(false);
    }

    public void SoftDelete()
    {
        foreach (IObserver o in observers)
        {
            if (o is IGeometryObject)
            {
                ((IGeometryObject)(o)).SoftDelete();
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