using UnityEngine;

public class ColorCommand : ICommand
{
    private Color color;
    private Color previousColor;
    private IGeometryObject geometryObject;

    public ColorCommand(Color color, IGeometryObject geometryObject)
    {
        this.color = color;
        this.geometryObject = geometryObject;
        this.previousColor = geometryObject.Color;
    }

    public void Execute()
    {
        geometryObject.SetColor(color);
    }

    public void Delete()
    {
        this.color = default;
         this.previousColor = default;
         this.geometryObject = null;
    }

    public void ReExecute()
    {
        geometryObject.SetColor(color);
    }

    public void UnExecute()
    {
        geometryObject.SetColor(previousColor);
    }
}
