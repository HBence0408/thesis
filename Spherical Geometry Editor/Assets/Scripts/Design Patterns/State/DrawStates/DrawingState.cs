using Unity.VisualScripting;
using UnityEngine;

public abstract class DrawingState
{
    protected DrawManager manager;
    private bool isActive;

    public bool IsActive
    {
        get { return isActive; }
    }

    protected DrawingState(DrawManager manager)
    {
        this.manager  = manager;
    }

    public virtual void OnEnter()
    {
        isActive = true;
    }

    public virtual void OnExit()
    {
        isActive = false;
    }

    public virtual void OnLeftMouseUp() { }
    public virtual void OnLeftMouseHold() { }
    public virtual void OnLeftMouseDown() { }
}
