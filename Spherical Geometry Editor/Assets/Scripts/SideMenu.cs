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

    private void OnLineButtonClick()
    {
        DrawManager.Instance.DrawLine();
    }

    private void OnSegmentButtonClick()
    {
        DrawManager.Instance.DrawSegment();
    }

    private void OnCircleButtonClick() 
    {
        DrawManager.Instance.DrawCircle();
    }

    private void OnPointButtonClick() 
    { 
        DrawManager.Instance.DrawPoint();
    }

    private void OnUndoButtonClick()
    { 
        DrawManager.Instance.Undo();
    }

    private void OnMoveButtonClick()
    {
        DrawManager.Instance.MovePoint();
    }

    private void OnIntersectButtonClick() 
    { 
        DrawManager.Instance.Intersect();
    }
}
