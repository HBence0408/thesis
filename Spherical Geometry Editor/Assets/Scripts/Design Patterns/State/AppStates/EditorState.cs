using UnityEngine;

public class EditorState : AppState
{
    private InputHandler inputHandler;
    private SideMenu sideMenu;
    private DrawManager drawManager;
    private CommandInvoker commandInvoker;

    public EditorState(AppCore appCore, InputHandler inputHandler, SideMenu sideMenu, DrawManager drawManager, CommandInvoker commandInvoker) : base(appCore)
    {
        this.inputHandler = inputHandler;
        this.sideMenu = sideMenu;
        this.drawManager = drawManager;
        this.commandInvoker = commandInvoker;
    }

    public override void OnEnter()
    {
        inputHandler.OnLeftMouseButtonUp += OnLeftMouseUp;
        inputHandler.OnLeftMouseButtonDown += OnLeftMouseDown;
        inputHandler.OnLeftMouseButtonHold += OnLeftMousenHold;

        sideMenu.OnLineButtonClicked += DrawLine;
        sideMenu.OnCircleButtonClicked += DrawCircle;
        sideMenu.OnSegmentButtonClicked += DrawSegment;
        sideMenu.OnMoveButtonClicked += MovePoint;
        sideMenu.OnIntersectButtonClicked += Intersect;
        sideMenu.OnPointButtonClicked += DrawPoint;
        sideMenu.OnUndoButtonClicked += Undo;
        sideMenu.OnRedoButtonClicked += Redo;
        sideMenu.OnDeleteButtonClicked += Delete;
        sideMenu.OnAntipodalButtonClicked += PlaceAntipodalPoint;
        sideMenu.OnpoleButtonClicked += PlacePolePoints;

    }

    public override void OnExit()
    {
        inputHandler.OnLeftMouseButtonUp -= OnLeftMouseUp;
        inputHandler.OnLeftMouseButtonDown -= OnLeftMouseDown;
        inputHandler.OnLeftMouseButtonHold -= OnLeftMousenHold;

        sideMenu.OnLineButtonClicked -= DrawLine;
        sideMenu.OnCircleButtonClicked -= DrawCircle;
        sideMenu.OnSegmentButtonClicked -= DrawSegment;
        sideMenu.OnMoveButtonClicked -= MovePoint;
        sideMenu.OnIntersectButtonClicked -= Intersect;
        sideMenu.OnPointButtonClicked -= DrawPoint;
        sideMenu.OnUndoButtonClicked -= Undo;
        sideMenu.OnRedoButtonClicked -= Redo;
        sideMenu.OnDeleteButtonClicked -= Delete;
        sideMenu.OnAntipodalButtonClicked -= PlaceAntipodalPoint;
        sideMenu.OnpoleButtonClicked -= PlacePolePoints;
    }

    private void OnLeftMouseDown()
    {
        drawManager.OnLeftMouseDown();
    }

    private void OnLeftMousenHold()
    {
        drawManager.OnLeftMouseHold();
    }

    private void OnLeftMouseUp()
    {
        drawManager.OnLeftMouseUp();
    }

    private void DrawLine()
    {
        drawManager.DrawLine();
    }

    private void DrawCircle()
    {
        drawManager.DrawCircle();
    }

    private void DrawSegment()
    {
        drawManager.DrawSegment();
    }

    private void MovePoint()
    {
        drawManager.MovePoint();
    }
     private void DrawPoint()
    {
        drawManager.DrawPoint();
    }

    public void Intersect()
    {
        drawManager.Intersect();
    }

    public void Undo()
    {
        commandInvoker.Undo();
    }

    public void Redo()
    {
        commandInvoker.Redo();
    }

    public void Delete()
    {
        drawManager.Delete();
    }

    public void PlacePolePoints()
    {
        drawManager.PlacePolePoints();
    }

    public void PlaceAntipodalPoint()
    {
        drawManager.PlaceAntipodalPoint();
    }
}
