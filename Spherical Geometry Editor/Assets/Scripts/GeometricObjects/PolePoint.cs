using UnityEngine;

public class PolePoint : ControllPoint, IObserver
{
    private ParametricCurve curve;
    private bool sign;

    public void SetCurve(ParametricCurve curve, bool sign)
    {
        this.curve = curve;
        this.sign = sign;
    }

    public void OnChanged()
    {
        this.transform.position = sign ? curve.NormalOfPlane : -curve.NormalOfPlane;
    }
}
