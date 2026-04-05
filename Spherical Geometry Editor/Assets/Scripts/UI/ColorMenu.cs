using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ColorMenu : MonoBehaviour
{
    [SerializeField] private VisualElement ui;
    private Button redButton;
    private Button blueButton;
    private Button blackButton;
    private Button greenButton;
    private Button magentaButton;
    private Button greyButton;
    private Button cancelButton;

    public event Action OnRedButtonClicked;
    public event Action OnBlueButtonClicked;
    public event Action OnBlackButtonClicked;
    public event Action OnGreenButtonClicked;
    public event Action OnMagentaButtonClicked;
    public event Action OnGreyButtonClicked;
    public event Action OnCancelButtonClicked;


    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        Hide();
    }

    private void OnEnable()
    {
        redButton = ui.Q<Button>("redButton");
        blueButton = ui.Q<Button>("blueButton");
        blackButton = ui.Q<Button>("blackButton");
        greenButton = ui.Q<Button>("greenButton");
        magentaButton = ui.Q<Button>("magentaButton");
        greyButton = ui.Q<Button>("greyButton");
        cancelButton = ui.Q<Button>("cancelButton");


        redButton.clicked += OnRedButtonClick;
        blueButton.clicked += OnBlueButtonClick;
        greenButton.clicked += OnGreenButtonClick;
        blackButton.clicked += OnBlackButtonClick;
        magentaButton.clicked += OnMagentaButtonClick;
        greyButton.clicked += OnGreyButtonClick;
        cancelButton.clicked += OnCancelButtonClick;
    }



    public void OnDisable()
    {
        redButton.clicked -= OnRedButtonClick;
        blueButton.clicked -= OnBlueButtonClick;
        greenButton.clicked -= OnGreenButtonClick;
        blackButton.clicked -= OnBlackButtonClick;
        magentaButton.clicked -= OnMagentaButtonClick;
        greyButton.clicked -= OnGreyButtonClick;
        cancelButton.clicked -= OnCancelButtonClick;
    }

    private void OnRedButtonClick()
    {
        OnRedButtonClicked?.Invoke();
    }

    private void OnBlueButtonClick()
    {
        OnBlueButtonClicked?.Invoke();
    }

    private void OnBlackButtonClick()
    {
        OnBlackButtonClicked?.Invoke();
    }

    private void OnMagentaButtonClick()
    {
        OnMagentaButtonClicked?.Invoke();
    }

    private void OnGreyButtonClick()
    {
        OnGreyButtonClicked?.Invoke();
    }

    private void OnCancelButtonClick()
    {
        OnCancelButtonClicked?.Invoke();
    }

    private void OnGreenButtonClick()
    {
        OnGreenButtonClicked?.Invoke();
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
