using UnityEngine;

public class PlaceMidPointCommand : ICommand
{
    private ControllPoint point1;
    private ControllPoint point2;
    private MidPoint pointScript;
    private SphericalGeometryFactory factory;
    private IRepository repository;
    private bool isExecuted;

    public PlaceMidPointCommand(ControllPoint point1, ControllPoint point2, SphericalGeometryFactory factory, IRepository repository)
    {
        this.point1 = point1;
        this.point2 = point2;
        this.factory = factory;
        this.repository = repository;
    }

    public void Execute()
    {
        Vector3 point1Pos = point1.transform.position;
        Vector3 point2Pos = point2.transform.position;
        Vector3 chordM = new Vector3((point1Pos.x + point2Pos.x)/2, (point1Pos.y + point2Pos.y) / 2, (point1Pos.z + point2Pos.z) / 2);

        pointScript = factory.CreateMidpoint(chordM.normalized);
        pointScript.SetPoints(point1, point2);
        repository.Store(pointScript);
        isExecuted = true;
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
        point1 = null;
        point2 = null;
        factory = null;
        repository = null;
    }
}
