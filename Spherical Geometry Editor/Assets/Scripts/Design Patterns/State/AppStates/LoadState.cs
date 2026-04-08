using UnityEngine;

public class LoadState : AppState
{
    private LoadManager loadManager;
    private string fileName;

    public LoadState(AppCore appCore, LoadManager loadManager, string fileName) : base(appCore)
    {
        this.loadManager = loadManager;
        this.fileName = fileName;
    }

    public override void OnEnter()
    {
        if (fileName != string.Empty)
        {
            try
            {
               loadManager.Load(fileName);
            }
            catch (System.Exception e)
            {

                Debug.LogError(e.Message);
                appCore.SetEditorState();
            }

            
        }

        appCore.SetEditorState();
    }

    public override void OnExit()
    {
        fileName = string.Empty;
    }
}
