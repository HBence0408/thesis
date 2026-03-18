using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionPoint : ControllPoint, IObserver
{

    public delegate Vector3 RecalculatePositionDelegate(ParametricCurve curve1, ParametricCurve curve2);
    ParametricCurve curve1;
    ParametricCurve curve2;
    RecalculatePositionDelegate recalculatePosition;

    public void OnChanged()
    {
        Vector3 zero = new Vector3(0, 0, 0);
        Vector3 newPos = recalculatePosition(curve1, curve2).normalized;
        if (newPos == zero)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.transform.position = newPos;
            Debug.Log("newPos: " + newPos);
            this.gameObject.SetActive(true);
        }
    }

    public void SetRecalculate(ParametricCurve curve1, ParametricCurve curve2, RecalculatePositionDelegate recalculate)
    {
        this.curve1 = curve1;
        this.curve2 = curve2;
        this.recalculatePosition = recalculate;
    }

    public new void Destroy()
    {
        curve1.Unsubscirbe(this);
        curve2.Unsubscirbe(this);
        base.Destroy();
    }
}
