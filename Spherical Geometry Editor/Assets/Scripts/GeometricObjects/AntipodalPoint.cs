using UnityEngine;

public class AntipodalPoint : ControllPoint, IObserver
{
    private ControllPoint point;

    public void SetPoint(ControllPoint point)
    {
        this.point = point;
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
