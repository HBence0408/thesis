using Unity.VisualScripting;
using UnityEngine;

public abstract class DrawingState : ScriptableObject
{
    protected DrawManager manager;
    protected DrawingMode drawingMode = null;
    private bool isActive;

    public bool IsActive
    {
        get { return isActive; }
    }


    public virtual void OnEnter(DrawingMode mode)
    {
        this.drawingMode = mode;
        isActive = true;
    }

    public virtual void OnExit()
    {
        drawingMode = null;
        isActive = false;
    }

    public void SetUp(DrawManager manager)
    {
        this.manager = manager;
    }

    public virtual void OnLeftMouseUp() { }
    public virtual void OnLeftMouseDown() { }

}
