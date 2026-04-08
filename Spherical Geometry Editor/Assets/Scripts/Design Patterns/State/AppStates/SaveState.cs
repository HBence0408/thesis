using UnityEngine;

public class SaveState : AppState
{
    SaveManager saveManager;

    public SaveState(AppCore appCore, SaveManager saveManager) : base(appCore)
    {
        this.saveManager = saveManager;
    }

    public override void OnEnter()
    {
        saveManager.Save("teszt");
        appCore.SetEditorState();
    }

    public override void OnExit()
    {
        //throw new System.NotImplementedException();
    }

}
