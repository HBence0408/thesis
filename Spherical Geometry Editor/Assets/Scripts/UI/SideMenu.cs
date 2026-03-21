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

    public event Action OnLineButtonClicked;
    public event Action OnSegmentButtonClicked;
    public event Action OnCircleButtonClicked;
    public event Action OnPointButtonClicked;
    public event Action OnUndoButtonClicked;
    public event Action OnMoveButtonClicked;
    public event Action OnIntersectButtonClicked;


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

        lineButton.clicked += OnLineButtonClick;
        segmentButton.clicked += OnSegmentButtonClick;
        circleButton.clicked += OnCircleButtonClick;
        pointButton.clicked += OnPointButtonClick;
        undoButton.clicked += OnUndoButtonClick;
        moveButton.clicked += OnMoveButtonClick;
        intersectButton.clicked += OnIntersectButtonClick;
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
}
