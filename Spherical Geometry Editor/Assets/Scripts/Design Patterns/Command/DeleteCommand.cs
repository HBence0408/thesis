using UnityEngine;

public class DeleteCommand : ICommand
{
    IGeometryObject geometryObject;
    bool isExecuted;

    public DeleteCommand(IGeometryObject geometryObject)
    {
        this.geometryObject = geometryObject;
    }

    public void Execute()
    {
        geometryObject.SoftDelete();
        isExecuted = true;
    }

    public void ReExecute()
    {
        geometryObject.SoftDelete();
        isExecuted = true;
    }

    public void UnExecute()
    {
        
        geometryObject.Restore();
        isExecuted= false;
    }

    public void Delete()
    {
        if (isExecuted && geometryObject != null)
        {
            geometryObject.HardDelete();
        }
        geometryObject = null;
    }
}
