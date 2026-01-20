using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ControllPoint : MonoBehaviour, Observable
{
    private List<Observer> observers = new List<Observer>();
    private Vector3 previousPos;

    public void Notify()
    {
        foreach (Observer o in observers)
        {
            o.OnChanged();
        }
    }

    public void Subscirbe(Observer o)
    {
        observers.Add(o);
    }

    public void Unsubscirbe(Observer o)
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