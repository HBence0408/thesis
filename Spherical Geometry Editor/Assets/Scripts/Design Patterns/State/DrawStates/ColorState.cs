using UnityEngine;

public class ColorState : DrawingState
{
    private Color color;
    private ICommandInvoker commandInvoker;

    public ColorState(IDrawManager manager, ICommandInvoker commandInvoker, Color color) : base(manager)
    {
        this.commandInvoker = commandInvoker;
        this.color = color;
    }

    public override void OnEnter()
    {
        manager.OnDown += OnDown;
    }

    public override void OnExit()
    {
        manager.OnDown -= OnDown;
    }

    private void OnDown()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.transform.TryGetComponent<IGeometryObject>(out IGeometryObject obj))
            {
                ColorCommand command = new ColorCommand(color, obj);
                commandInvoker.ExecuteCommand(command);
                manager.SetState(this);
            }
        }
    }

}
