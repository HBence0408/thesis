using UnityEngine;

public class SaveState : AppState
{
    private ISaveManager saveManager;
    private string fileName = "";

    public SaveState(AppCore appCore, ISaveManager saveManager, string fileName) : base(appCore)
    {
        this.saveManager = saveManager;
        this.fileName = fileName;
    }

    public override void OnEnter()
    {
        saveManager.Save(fileName);
        appCore.SetEditorState();
    }

    public override void OnExit()
    {
        
    }
}
