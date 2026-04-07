using System;
using System.Security.Cryptography;
using UnityEngine;

public class MidPoint : ControllPoint, IObserver
{
    private ControllPoint point1;
    private ControllPoint point2;

    public Guid ControllPoint1 => point1.Id;
    public Vector3 ControllPoint1Pos => point1.transform.position;

    public Guid ControllPoint2 => point2.Id;
    public Vector3 ControllPoint2Pos => point2.transform.position;

    public void SetPoints(ControllPoint point1, ControllPoint point2)
    {
        this.point1 = point1;
        this.point2 = point2;
    }

    public void OnChanged()
    {
        if (!IsActive)
        {
            return;
        }

        Vector3 point1Pos = point1.transform.position;
        Vector3 point2Pos = point2.transform.position;
        Vector3 chordM = new Vector3((point1Pos.x + point2Pos.x) / 2, (point1Pos.y + point2Pos.y) / 2, (point1Pos.z + point2Pos.z) / 2);
        this.transform.position = chordM.normalized;
    }
}
