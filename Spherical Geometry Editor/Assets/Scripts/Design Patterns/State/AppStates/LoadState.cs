using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadState : IAppState
{
    private ILoadManager loadManager;
    private string fileName;

    public LoadState(ILoadManager loadManager, string fileName)
    {
        this.loadManager = loadManager;
        this.fileName = fileName;
    }

    public void OnEnter()
    {
        if (fileName == string.Empty)
        {
            SceneManager.LoadScene("MainMenuScene");
        }

        loadManager.Load(fileName);
        AppCore.Instance.SetEditorState();
    }

    public void OnExit()
    {
        fileName = string.Empty;
    }
}
