using System.Security.Cryptography;
using UnityEngine;

public class PlaceLimitedPointCommand : ICommand
{
    private Vector3 pos;
    private LimitedPoint pointScript;
    private ISphericalGeometryFactory factory;
    private IRepository repository;
    private ParametricCurve curve;
    private bool isExecuted;

    public PlaceLimitedPointCommand(Vector3 pos, ParametricCurve curve, ISphericalGeometryFactory factory, IRepository repository)
    {
        this.pos = pos.normalized;
        this.factory = factory;
        this.repository = repository;
        this.curve = curve;
    }

    public void Execute()
    {
        pointScript = factory.CreateLimitedpoint(pos);
        pointScript.transform.position = pos;
        pointScript.SetCurve(curve);
        repository.Store(pointScript);
        isExecuted = true;
    }

    public ControllPoint GetPoint()
    {
        return pointScript;
    }

    public void UnExecute()
    {
        pointScript.SoftDelete(repository.Delete);
        repository.Delete(pointScript.Id);
        isExecuted = false;
    }

    public void ReExecute()
    {
        pointScript.Restore(repository.Store);
        repository.Store(pointScript);
        isExecuted = true;
    }

    public void Delete()
    {
        if (!isExecuted)
        {
            pointScript.HardDelete();
        }
        pointScript = null;
        curve = null;
        factory = null;
        repository = null;
    }
}
