using UnityEngine;

public class SaveState : IAppState
{
    private ISaveManager saveManager;
    private string fileName = "";

    public SaveState(ISaveManager saveManager, string fileName)
    {
        this.saveManager = saveManager;
        this.fileName = fileName;
    }

    public void OnEnter()
    {
        saveManager.Save(fileName);
        AppCore.Instance.SetEditorState();
    }

    public void OnExit() { }
}
