using System;
using UnityEngine;

public class LimitedPoint : ControllPoint, IObserver, IMoveablePoint
{
    private ParametricCurve curve;

    public Guid Curve => curve.Id;

    private void Start()
    {
        if (curve != null)
        {
            Reposition(this.transform.position);
        }
    }

    public void SetCurve(ParametricCurve curve)
    {
        Debug.Log(curve);
        this.curve = curve;
        curve.Subscirbe(this);
    }

    public override void Reposition(Vector3 newPos)
    {
        Debug.Log(curve);
        if (curve != null)
        {
            Vector3 closestPoint = curve.GetClosestPoint(newPos);
            transform.position = closestPoint;
        }
    }

    public void OnChanged()
    {
        if (!IsActive)
        {
            return;
        }

        Reposition(this.transform.position);
    }
}
