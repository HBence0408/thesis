using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EscapeMenuState : AppState
{

    private SideMenu sideMenu;
    private EscapeMenu escapeMenu;

    public EscapeMenuState(AppCore appCore, SideMenu sideMenu, EscapeMenu escapeMenu) : base(appCore)
    {
        this.sideMenu = sideMenu;
        this.escapeMenu = escapeMenu;
    }

    public override void OnEnter()
    {
        sideMenu.Hide();
        escapeMenu.Reveal();
        escapeMenu.OnBackButtonClicked += Back;
        escapeMenu.OnSaveButtonClicked += Save;
        escapeMenu.OnQuitButtonClicked += Quit;
    }

    public override void OnExit()
    {
        sideMenu.Reveal();
        escapeMenu.Hide();
        escapeMenu.OnBackButtonClicked -= Back;
        escapeMenu.OnSaveButtonClicked -= Save;
        escapeMenu.OnQuitButtonClicked -= Quit;
    }

    private void Back()
    {
        appCore.SetEditorState();
    }

    private void Save()
    {
        appCore.SetSaveState();
    }

    private void Quit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
