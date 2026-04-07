using UnityEngine;

public class GrabablePoint : ControllPoint, IMoveablePoint
{
    public override void Reposition(Vector3 vector3)
    {
        this.transform.position = vector3.normalized;
    }
}
