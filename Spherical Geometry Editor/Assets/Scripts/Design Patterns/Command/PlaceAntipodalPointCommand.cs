using UnityEngine;

public class PlaceAntipodalPointCommand : ICommand
{
    private ControllPoint point;
    private AntipodalPoint pointScript;
    private SphericalGeometryFactory factory;
    private IRepository repository;
    private bool isExecuted;
    public PlaceAntipodalPointCommand(ControllPoint point, SphericalGeometryFactory factory, IRepository repository)
    {
        this.point = point;
        this.factory = factory;
        this.repository = repository;
    }
    public void Execute()
    {
        pointScript = factory.CreateAntipodalpoint(-point.transform.position.normalized);
        pointScript.SetPoint(point);
        repository.Store(pointScript);
        isExecuted = true;
    }
    public void UnExecute()
    {
        pointScript.SoftDelete(repository.Delete);
        repository.Delete(pointScript.Id);
        isExecuted = false;
    }
    public ControllPoint GetPoint()
    {
        return pointScript;
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
        point = null;
        factory = null;
        repository = null;
    }
}