using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SideMenu : MonoBehaviour
{
    [SerializeField] private VisualElement ui;
    private Button lineButton;
    private Button segmentButton;
    private Button circleButton;
    private Button pointButton;
    private Button undoButton;
    private Button moveButton;
    private Button intersectButton;
    private Button redoButton;
    private Button deleteButton;
    private Button antipodalButton;
    private Button poleButton;
    private Button midPointButton;

    public event Action OnLineButtonClicked;
    public event Action OnSegmentButtonClicked;
    public event Action OnCircleButtonClicked;
    public event Action OnPointButtonClicked;
    public event Action OnUndoButtonClicked;
    public event Action OnMoveButtonClicked;
    public event Action OnIntersectButtonClicked;
    public event Action OnRedoButtonClicked;
    public event Action OnDeleteButtonClicked;
    public event Action OnAntipodalButtonClicked;
    public event Action OnpoleButtonClicked;
    public event Action OnMidPointButtonClicked;


    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        lineButton = ui.Q<Button>("lineButton");
        segmentButton = ui.Q<Button>("segmentButton");
        circleButton = ui.Q<Button>("circleButton");
        pointButton = ui.Q<Button>("pointButton");
        undoButton = ui.Q<Button>("undoButton");
        moveButton = ui.Q<Button>("moveButton");
        intersectButton = ui.Q<Button>("intersectButton");
        redoButton = ui.Q<Button>("redoButton");
        deleteButton = ui.Q<Button>("deleteButton");
        antipodalButton = ui.Q<Button>("antipodalButton");
        poleButton = ui.Q<Button>("poleButton");
        midPointButton = ui.Q<Button>("midPointButton");

        lineButton.clicked += OnLineButtonClick;
        segmentButton.clicked += OnSegmentButtonClick;
        circleButton.clicked += OnCircleButtonClick;
        pointButton.clicked += OnPointButtonClick;
        undoButton.clicked += OnUndoButtonClick;
        moveButton.clicked += OnMoveButtonClick;
        intersectButton.clicked += OnIntersectButtonClick;
        redoButton.clicked += OnRedoButtonClick;
        deleteButton.clicked += OnDeleteButtonClick;
        antipodalButton.clicked += OnAntipodalButtonClick;
        poleButton.clicked += OnPoleButtonClick;
        midPointButton.clicked += OnMidPointButtonClick;
    }

    public void OnDisable()
    {
        lineButton.clicked -= OnLineButtonClick;
        segmentButton.clicked -= OnSegmentButtonClick;
        circleButton.clicked -= OnCircleButtonClick;
        pointButton.clicked -= OnPointButtonClick;
        undoButton.clicked -= OnUndoButtonClick;
        moveButton.clicked -= OnMoveButtonClick;
        intersectButton.clicked -= OnIntersectButtonClick;
        redoButton.clicked -= OnRedoButtonClick;
        deleteButton.clicked -= OnDeleteButtonClick;
        antipodalButton.clicked -= OnAntipodalButtonClick;
        poleButton.clicked -= OnPoleButtonClick;
        midPointButton.clicked -= OnMidPointButtonClick;
    }

    private void OnLineButtonClick()
    {
        OnLineButtonClicked?.Invoke();
    }

    private void OnSegmentButtonClick()
    {
        OnSegmentButtonClicked?.Invoke();
    }

    private void OnCircleButtonClick()
    {
        OnCircleButtonClicked?.Invoke();
    }

    private void OnPointButtonClick()
    {
        OnPointButtonClicked?.Invoke();
    }

    private void OnUndoButtonClick()
    {
        OnUndoButtonClicked?.Invoke();
    }

    private void OnMoveButtonClick()
    {
        OnMoveButtonClicked?.Invoke();
    }

    private void OnIntersectButtonClick()
    {
        OnIntersectButtonClicked?.Invoke();
    }

    private void OnRedoButtonClick()
    {
        OnRedoButtonClicked?.Invoke();
    }

    private void OnDeleteButtonClick()
    {
        OnDeleteButtonClicked?.Invoke();
    }

    private void OnAntipodalButtonClick()
    {
        OnAntipodalButtonClicked?.Invoke();
    }

    private void OnPoleButtonClick()
    {
        OnpoleButtonClicked?.Invoke();
    }

    public void OnMidPointButtonClick()
    {
        OnMidPointButtonClicked?.Invoke();
    }
}
