using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ControllPoint : MonoBehaviour, IObservable
{
    private List<IObserver> observers = new List<IObserver>();
    private Vector3 previousPos;

    public void Notify()
    {
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
        Destroy(this.gameObject);
    }


    // ez átkerül egy draggabble pointba, ez egy ős ősztály lesz és intersection pont a "tesója" 
    private void OnMouseDrag()
    {
        if (!DrawManager.Instance.MoveState.IsActive)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(-Camera.main.transform.forward, transform.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            transform.position = point.normalized;
        }
    }
}