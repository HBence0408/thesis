using UnityEngine;

public class DeleteCommand : ICommand
{
    IGeometryObject geometryObject;
    IRepository repository;
    bool isExecuted;

    public DeleteCommand(IGeometryObject geometryObject, IRepository repository)
    {
        this.geometryObject = geometryObject;
        this.repository = repository;
    }

    public void Execute()
    {
        geometryObject.SoftDelete(repository.Delete);
        isExecuted = true;
        repository.Delete(geometryObject.Id);
    }

    public void ReExecute()
    {
        geometryObject.SoftDelete(repository.Delete);
        isExecuted = true;
        repository.Delete(geometryObject.Id);
    }

    public void UnExecute()
    {
        geometryObject.Restore(repository.Store);
        isExecuted= false;
        repository.Store(geometryObject);
    }

    public void Delete()
    {
        if (isExecuted && geometryObject != null)
        {
            geometryObject.HardDelete();
        }
        geometryObject = null;
        repository = null;
    }
}
