using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private VisualElement ui;
    private Button backButton;
    private Button saveButton;
    private Button quitButton;


    public event Action OnBackButtonClicked;
    public event Action OnSaveButtonClicked;
    public event Action OnQuitButtonClicked;



    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        Hide();
    }

    private void OnEnable()
    {
        backButton = ui.Q<Button>("BackButton");
        saveButton = ui.Q<Button>("SaveButton");
        quitButton = ui.Q<Button>("QuitButton");



        backButton.clicked += OnBackButtonClick;
        saveButton.clicked += OnSaveButtonClick;
        quitButton.clicked += OnQuitButtonClick;
    }



    public void OnDisable()
    {
        backButton.clicked -= OnBackButtonClick;
        saveButton.clicked -= OnSaveButtonClick;
        quitButton.clicked -= OnQuitButtonClick;

    }

    private void OnBackButtonClick()
    {
        OnBackButtonClicked?.Invoke();
    }

    private void OnSaveButtonClick()
    {
        OnSaveButtonClicked?.Invoke();
    }

    private void OnQuitButtonClick()
    {
        OnQuitButtonClicked?.Invoke();
    }

    public void Hide()
    {
        ui.visible = false;
    }

    public void Reveal()
    {
        ui.visible = true;
    }
}
