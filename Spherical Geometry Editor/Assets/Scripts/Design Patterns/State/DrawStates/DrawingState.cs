using Unity.VisualScripting;
using UnityEngine;

public abstract class DrawingState
{
    protected IDrawManager manager;

    protected DrawingState(IDrawManager manager)
    {
        this.manager  = manager;
    }

    public abstract void OnEnter();

    public abstract void OnExit();

}
