using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionPoint : ControllPoint, IObserver
{
    public delegate Vector3 RecalculatePositionDelegate(ParametricCurve curve1, ParametricCurve curve2);
    private ParametricCurve curve1;
    private ParametricCurve curve2;
    private RecalculatePositionDelegate recalculatePosition;
    private IntersectionType intersectionType;

    public Guid Curve1 =>  curve1.Id;
    public Guid Curve2 =>  curve2.Id;
    public IntersectionType IntersectionType => intersectionType;

    public void OnChanged()
    {
        if (!IsActive)
        {
            return;
        }

        Vector3 zero = new Vector3(0, 0, 0);

        Debug.Log(curve1);
        Debug.Log(curve2);

        Vector3 newPos = recalculatePosition(curve1, curve2).normalized;
        if (newPos == zero)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.transform.position = newPos;
            this.gameObject.SetActive(true);
        }
    }

    public void SetRecalculate(IntersectionType intersectionType, ParametricCurve curve1, ParametricCurve curve2, RecalculatePositionDelegate recalculate)
    {
        this.intersectionType = intersectionType;
        this.curve1 = curve1;
        curve1.Subscirbe(this);
        this.curve2 = curve2;
        curve2.Subscirbe(this);
        this.recalculatePosition = recalculate;

        
    }

    //public new void Destroy()
    //{
    //    curve1.Unsubscirbe(this);
    //    curve2.Unsubscirbe(this);
    //    base.Destroy();
    //}
}
