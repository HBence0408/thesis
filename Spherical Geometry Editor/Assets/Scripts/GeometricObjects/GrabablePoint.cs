using UnityEngine;

public class GrabablePoint : ControllPoint
{
    public override void Reposition(Vector3 vector3)
    {
        this.transform.position = vector3.normalized;
    }
}
