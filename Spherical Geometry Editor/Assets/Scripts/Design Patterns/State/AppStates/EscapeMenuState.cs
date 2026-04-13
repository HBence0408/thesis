using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EscapeMenuState : IAppState
{

    private SideMenu sideMenu;
    private EscapeMenu escapeMenu;

    public EscapeMenuState(SideMenu sideMenu, EscapeMenu escapeMenu)
    {
        this.sideMenu = sideMenu;
        this.escapeMenu = escapeMenu;
    }

    public void OnEnter()
    {
        sideMenu.Hide();
        escapeMenu.Reveal();
        escapeMenu.OnBackButtonClicked += Back;
        escapeMenu.OnSaveButtonClicked += Save;
        escapeMenu.OnQuitButtonClicked += Quit;
    }

    public void OnExit()
    {
        sideMenu.Reveal();
        escapeMenu.Hide();
        escapeMenu.OnBackButtonClicked -= Back;
        escapeMenu.OnSaveButtonClicked -= Save;
        escapeMenu.OnQuitButtonClicked -= Quit;
    }

    private void Back()
    {
        AppCore.Instance.SetEditorState();
    }

    private void Save()
    {
        AppCore.Instance.SetSaveState();
    }

    private void Quit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
