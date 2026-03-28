using UnityEngine;

public class PlaceLimitedPointCommand : ICommand
{
    private Vector3 pos;
    private LimitedPoint pointScript;
    private SphericalGeometryFactory factory;
    private IRepository repository;
    private bool isExecuted;

    public PlaceLimitedPointCommand(Vector3 pos, SphericalGeometryFactory factory, IRepository repository)
    {
        this.pos = pos.normalized;
        this.factory = factory;
        this.repository = repository;
    }

    public void Execute()
    {
        pointScript = factory.CreateLimitedpoint(pos);
        pointScript.transform.position = pos;
        repository.Store(pointScript);
        isExecuted = true;
    }

    public void UnExecute()
    {
        pointScript.SoftDelete();
        repository.Delete(pointScript.Id);
        isExecuted = false;
    }

    public LimitedPoint GetPoint()
    {
        return pointScript;
    }

    public void ReExecute()
    {
        pointScript.Restore();
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
    }
}
