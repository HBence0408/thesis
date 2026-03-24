using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlacePointCommand : ICommand
{
    private Vector3 pos;
    private ControllPoint pointScript;
    private SphericalGeometryFactory factory;
    private bool isExecuted;

    public PlacePointCommand(Vector3 pos, SphericalGeometryFactory factory)
    {
        this.pos = pos.normalized;
        this.factory = factory;
    }

    public void Execute()
    {
        pointScript = factory.CreateGrabablepoint(pos);
        pointScript.transform.position = pos;
        isExecuted = true;
    }

    public void UnExecute()
    {
        pointScript.SoftDelete();
        isExecuted = false;
    }

    public ControllPoint GetPoint()
    {
        return pointScript;
    }

    public void ReExecute()
    {
        pointScript.Restore();
        isExecuted = true;
    }

    public void Delete()
    {
        if (!isExecuted)
        {
            pointScript.HardDelete();
        }
        pointScript = null;
    }
}
