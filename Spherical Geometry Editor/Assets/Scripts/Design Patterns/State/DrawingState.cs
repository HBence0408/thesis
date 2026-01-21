using Unity.VisualScripting;
using UnityEngine;

public abstract class DrawingState : ScriptableObject
{
    protected DrawManager manager;
    protected DrawingMode drawingMode = null;

    public virtual void OnEnter(DrawingMode mode)
    {
        this.drawingMode = mode;
    }

    public virtual void OnExit()
    {
        drawingMode = null;
    }

    public void SetUp(DrawManager manager)
    {
        this.manager = manager;
    }

    public virtual void OnLeftMouseDown() { }

}
