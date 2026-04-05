using UnityEngine;

public class ColorPickState : AppState
{
    private InputHandler inputHandler;
    private SideMenu sideMenu;
    private DrawManager drawManager;
    private CommandInvoker commandInvoker;
    private ColorMenu colorMenu;

    public ColorPickState(AppCore appCore,InputHandler inputHandler, SideMenu sideMenu, DrawManager drawManager, CommandInvoker commandInvoker, ColorMenu colorMenu) : base(appCore)
    {
        this.inputHandler = inputHandler;
        this.sideMenu = sideMenu;
        this.drawManager = drawManager;
        this.commandInvoker = commandInvoker;
        this.colorMenu = colorMenu;
    }

    public override void OnEnter()
    {
        sideMenu.Hide();
        colorMenu.Reveal();
        colorMenu.OnRedButtonClicked += SetToRed;
        colorMenu.OnBlueButtonClicked += SetToBlue;
        colorMenu.OnGreenButtonClicked += SetToGreen;
        colorMenu.OnBlackButtonClicked += SetToBlack;
        colorMenu.OnGreyButtonClicked += SetToGrey;
        colorMenu.OnMagentaButtonClicked += SetToMagenta;
        colorMenu.OnCancelButtonClicked += Cancel;
    }

    public override void OnExit()
    {
        sideMenu.Reveal();
        colorMenu.Hide();
        colorMenu.OnRedButtonClicked -= SetToRed;
        colorMenu.OnBlueButtonClicked -= SetToBlue;
        colorMenu.OnGreenButtonClicked -= SetToGreen;
        colorMenu.OnBlackButtonClicked -= SetToBlack;
        colorMenu.OnGreyButtonClicked -= SetToGrey;
        colorMenu.OnMagentaButtonClicked -= SetToMagenta;
        colorMenu.OnCancelButtonClicked -= Cancel;
    }

    private void SetToRed()
    {

        appCore.SetEditorState();
    }

    private void SetToBlue()
    {

        appCore.SetEditorState();
    }

    private void SetToGreen()
    {

        appCore.SetEditorState();
    }

    private void SetToBlack()
    {

        appCore.SetEditorState();
    }

    private void SetToGrey()
    {

        appCore.SetEditorState();
    }

    private void SetToMagenta()
    {

        appCore.SetEditorState();
    }

    private void Cancel()
    {
        appCore.SetEditorState();
    }
}
