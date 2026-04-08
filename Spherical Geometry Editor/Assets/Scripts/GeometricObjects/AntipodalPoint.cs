using System;
using UnityEngine;

public class AntipodalPoint : ControllPoint, IObserver
{
    private ControllPoint point;
    public Guid ControllPoint => point.Id;

    public void SetPoint(ControllPoint point)
    {
        this.point = point;
        point.Subscirbe(this);
    }

    public void OnChanged()
    {
        if (!IsActive)
        {
            return;
        }

        this.transform.position = -point.transform.position;   
    }
}
