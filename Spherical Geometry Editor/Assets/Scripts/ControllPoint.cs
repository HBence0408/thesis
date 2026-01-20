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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != previousPos)
        {
            Notify();
            previousPos = transform.position;
        }
    }
}