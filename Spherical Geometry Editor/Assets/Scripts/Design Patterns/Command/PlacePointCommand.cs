using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlacePointCommand : ICommand
{
    private Vector3 pos;
    private ControllPoint pointScript;
    private SphericalGeometryFactory factory;


    public PlacePointCommand(Vector3 pos, SphericalGeometryFactory factory)
    {
        this.pos = pos.normalized;
        this.factory = factory;
    }

    public void Execute()
    {
        pointScript = factory.CreateGrabablepoint(pos);
        pointScript.transform.position = pos;
    }

    public void UnExecute()
    {
        pointScript.Destroy();
    }

    public ControllPoint GetPoint()
    {
        return pointScript;
    }
}
