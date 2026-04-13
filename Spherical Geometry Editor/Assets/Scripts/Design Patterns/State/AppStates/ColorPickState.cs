using UnityEngine;

public class ColorPickState : IAppState
{
    private SideMenu sideMenu;
    private IDrawManager drawManager;
    private ColorMenu colorMenu;

    public ColorPickState(SideMenu sideMenu, IDrawManager drawManager, ColorMenu colorMenu)
    {
        this.sideMenu = sideMenu;
        this.drawManager = drawManager;
        this.colorMenu = colorMenu;
    }

    public void OnEnter()
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

    public void OnExit()
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
        drawManager.ColorRed();
        AppCore.Instance.SetEditorState();
    }

    private void SetToBlue()
    {
        drawManager.ColorBlue();
        AppCore.Instance.SetEditorState();
    }

    private void SetToGreen()
    {
        drawManager.ColorGreen();
        AppCore.Instance.SetEditorState();
    }

    private void SetToBlack()
    {
        drawManager.ColorBlack();
        AppCore.Instance.SetEditorState();
    }

    private void SetToGrey()
    {
        drawManager.ColorGrey();
        AppCore.Instance.SetEditorState();
    }

    private void SetToMagenta()
    {
        drawManager.ColorMagenta();
        AppCore.Instance.SetEditorState();
    }

    private void Cancel()
    {
        AppCore.Instance.SetEditorState();
    }
}
