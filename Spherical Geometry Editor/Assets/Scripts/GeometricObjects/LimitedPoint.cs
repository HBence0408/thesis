using UnityEngine;

public class LimitedPoint : ControllPoint, IObserver
{
    private ParametricCurve curve;

    public void SetCurve(ParametricCurve curve)
    {
        this.curve = curve;
    }

    public override void Reposition(Vector3 newPos)
    {
        if (curve != null)
        {
            Vector3 closestPoint = curve.GetClosestPoint(newPos);
            transform.position = closestPoint;
        }
    }

    public void OnChanged()
    {
        Reposition(this.transform.position);
    }
}
