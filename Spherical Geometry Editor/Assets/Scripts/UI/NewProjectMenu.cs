using System;
using UnityEngine;
using UnityEngine.UIElements;

public class NewProjectMenu : MonoBehaviour
{
    private TextField textField;
    private Button createButton;
    public Action<string> OnCreateButtonClicked;
    private VisualElement ui;

    private void Start()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        Hide();
        textField = ui.Q<TextField>("NewFileNameField");
        createButton = ui.Q<Button>("CreateButton");
        createButton.clicked += OnCreateButtonClick;
    }

    private void OnCreateButtonClick()
    {
         OnCreateButtonClicked?.Invoke(textField.text);
    }

    public void Hide()
    {
        ui.visible = false;
    }

    public void Reveal()
    {
        ui.visible = true;
        textField.Focus();
    }

}
