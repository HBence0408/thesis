using System.Drawing;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DeleteState : DrawingState
{
    private ICommandInvoker commandInvoker;
    private IRepository repository;

    public DeleteState(IDrawManager manager, ICommandInvoker commandInvoker, IRepository repository) : base(manager)
    {
        this.commandInvoker = commandInvoker;
        this.repository = repository;
    }

    public override void OnEnter()
    {
        manager.OnDown += OnDown;
    }

    public override void OnExit()
    {
        manager.OnDown += OnDown;
    }

    private void OnDown()
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
