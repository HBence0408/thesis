using System.Drawing;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DeleteState : DrawingState
{
    private CommandInvoker commandInvoker;
    private IRepository repository;

    public DeleteState(DrawManager manager, CommandInvoker commandInvoker, IRepository repository) : base(manager)
    {
        this.commandInvoker = commandInvoker;
        this.repository = repository;
    }

    public override void OnLeftMouseDown()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            IGeometryObject geometryObject;
            if (hit.transform.gameObject.TryGetComponent<IGeometryObject>(out geometryObject))
            {
                DeleteCommand command = new DeleteCommand(geometryObject, repository);
                commandInvoker.ExecuteCommand(command);
            }
        }
    }
}
