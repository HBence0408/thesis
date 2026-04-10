using UnityEngine;

public class LoadState : AppState
{
    private ILoadManager loadManager;
    private string fileName;

    public LoadState(AppCore appCore, ILoadManager loadManager, string fileName) : base(appCore)
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
