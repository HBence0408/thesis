using UnityEngine;

public class PlacePolesCommand : ICommand
{
    private GreatCircle greatCircle;
    private PolePoint point1Script;
    private PolePoint point2Script;
    private ISphericalGeometryFactory factory;
    private IRepository repository;
    private bool isExecuted;

    public PlacePolesCommand(GreatCircle greatCircle, ISphericalGeometryFactory factory, IRepository repository)
    {
        this.greatCircle = greatCircle;
        this.factory = factory;
        this.repository = repository;
    }

    public void Execute()
    {
        point1Script = factory.CreatePolepoint(greatCircle.NormalOfPlane);
        point1Script.SetCurve(greatCircle, true);
        repository.Store(point1Script);

        point2Script = factory.CreatePolepoint(-greatCircle.NormalOfPlane);
        point2Script.SetCurve(greatCircle, false);
        repository.Store(point2Script);

        isExecuted = true;
    }

    public void UnExecute()
    {
        point1Script.SoftDelete(repository.Delete);
        repository.Delete(point1Script.Id);

        point2Script.SoftDelete(repository.Delete);
        repository.Delete(point2Script.Id);

        isExecuted = false;
    }

    public PolePoint[] GetPoints()
    {
        return new PolePoint[] {point1Script, point2Script};
    }

    public void ReExecute()
    {
        point1Script.Restore(repository.Store);
        repository.Store(point1Script);

        point2Script.Restore(repository.Store);
        repository.Store(point2Script);

        isExecuted = true;
    }

    public void Delete()
    {
        if (!isExecuted)
        {
            point1Script.HardDelete();
            point2Script.HardDelete();
        }
        point1Script = null;
        point2Script = null;
        greatCircle = null;
        factory = null;
        repository = null;
    }
}
