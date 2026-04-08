using UnityEngine;

public abstract class AppState 
{
    protected AppCore appCore;

    protected AppState(AppCore appCore)
    {
        this.appCore = appCore;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
}
