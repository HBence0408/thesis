using System;
using UnityEngine;

public class PolePoint : ControllPoint, IObserver
{
    private ParametricCurve curve;
    private bool sign;

    public Guid Curve => curve.Id;
    public bool Sign => sign;

    public void SetCurve(ParametricCurve curve, bool sign)
    {
        this.curve = curve;
        this.sign = sign;
    }

    public void OnChanged()
    {
        if (!IsActive)
        {
            return;
        }

        this.transform.position = sign ? curve.NormalOfPlane : -curve.NormalOfPlane;
    }
}
