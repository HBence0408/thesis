using UnityEngine;

public class ColorState : DrawingState
{
    private Color color;
    private CommandInvoker commandInvoker;

    public ColorState(DrawManager manager, CommandInvoker commandInvoker, Color color) : base(manager)
    {
        this.commandInvoker = commandInvoker;
        this.color = color;
    }

    public override void OnLeftMouseDown()
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
