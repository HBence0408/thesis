using UnityEngine;

public class EditorState : AppState
{
    private IInputHandler inputHandler;
    private SideMenu sideMenu;
    private IDrawManager drawManager;
    private ICommandInvoker commandInvoker;
    private CameraMovement cameraMovement;
    private IHighlighter highlighter;
   // private ColorMenu colorMenu;

    public EditorState(AppCore appCore, IInputHandler inputHandler, SideMenu sideMenu, IDrawManager drawManager, ICommandInvoker commandInvoker, CameraMovement cameraMovement, IHighlighter highlighter) : base(appCore)
    {
        this.inputHandler = inputHandler;
        this.sideMenu = sideMenu;
        this.drawManager = drawManager;
        this.commandInvoker = commandInvoker;
        this.cameraMovement = cameraMovement;
        this.highlighter = highlighter;
    }

    public override void OnEnter()
    {
        inputHandler.OnLeftMouseButtonUp += OnLeftMouseUp;
        inputHandler.OnLeftMouseButtonDown += OnLeftMouseDown;
        inputHandler.OnLeftMouseButtonHold += OnLeftMousenHold;
        inputHandler.OnWHoldDown += MoveUp;
        inputHandler.OnSHoldDown += MoveDown;
        inputHandler.OnAHoldDown += MoveLeft;
        inputHandler.OnDHoldDown += MoveRight;
        inputHandler.OnQHoldDown += TiltLeft;
        inputHandler.OnEHoldDown += TiltRight;
        inputHandler.OnHover += CurrentlyHovered;
        inputHandler.OnNotHover += NotHover;
        inputHandler.OnEscapeKeyDown += Escape;

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
        sideMenu.OnPoleButtonClicked += PlacePolePoints;
        sideMenu.OnMidPointButtonClicked += PlaceMidPoint;
        sideMenu.OnRightAngleButtonClicked += DrawRightAngleGreatCircle;
        sideMenu.OnColorButtonClicked += Color;
    }

    public override void OnExit()
    {
        inputHandler.OnLeftMouseButtonUp -= OnLeftMouseUp;
        inputHandler.OnLeftMouseButtonDown -= OnLeftMouseDown;
        inputHandler.OnLeftMouseButtonHold -= OnLeftMousenHold;
        inputHandler.OnWHoldDown -= MoveUp;
        inputHandler.OnSHoldDown -= MoveDown;
        inputHandler.OnAHoldDown -= MoveLeft;
        inputHandler.OnDHoldDown -= MoveRight;
        inputHandler.OnQHoldDown -= TiltLeft;
        inputHandler.OnEHoldDown -= TiltRight;
        inputHandler.OnHover -= CurrentlyHovered;
        inputHandler.OnNotHover -= NotHover;
        inputHandler.OnEscapeKeyDown -= Escape;


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
        sideMenu.OnPoleButtonClicked -= PlacePolePoints;
        sideMenu.OnMidPointButtonClicked -= PlaceMidPoint;
        sideMenu.OnRightAngleButtonClicked -= DrawRightAngleGreatCircle;
        sideMenu.OnColorButtonClicked -= Color;
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
        highlighter.HighlightControllPointsState();
    }

    private void DrawCircle()
    {
        drawManager.DrawCircle();
        highlighter.HighlightControllPointsState();
    }

    private void DrawSegment()
    {
        drawManager.DrawSegment();
        highlighter.HighlightControllPointsState();
    }

    private void MovePoint()
    {
        drawManager.MovePoint();
        highlighter.HighlightMoveAblePointsState();
    }
     private void DrawPoint()
    {
        drawManager.DrawPoint();
        highlighter.HighlightCurvesState();
    }

    public void Intersect()
    {
        drawManager.Intersect();
        highlighter.HighlightCurvesState();
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
        highlighter.HighlightEverythingState();
    }

    public void PlacePolePoints()
    {
        drawManager.PlacePolePoints();
        highlighter.HighlightGreatCirclesState();
    }

    public void PlaceAntipodalPoint()
    {
        drawManager.PlaceAntipodalPoint();
        highlighter.HighlightControllPointsState();
    }

    public void PlaceMidPoint()
    {
        drawManager.PlaceMidPoint();
        highlighter.HighlightControllPointsState();
    }

    public void DrawRightAngleGreatCircle()
    {
       drawManager.DrawRightAngleGreatCircle();
       highlighter.HighlightGreatCirclesState();
    }

    public void Color()
    {
        drawManager.Idle();
        highlighter.HighlightEverythingState();
        appCore.SetColorPickState();
    }

    public void MoveUp()
    {
        cameraMovement.MoveUp();
    }

    public void MoveDown()
    {
        cameraMovement.MoveDown();
    }

    public void MoveRight()
    {
        cameraMovement.MoveRight();
    }

    public void MoveLeft()
    {
        cameraMovement.MoveLeft();
    }

    public void TiltLeft()
    {
        cameraMovement.TiltLeft();
    }

    public void TiltRight()
    {
        cameraMovement.TiltRight();
    }

    public void CurrentlyHovered(IGeometryObject geometryObject)
    {
        //Debug.Log(geometryObject.Id);
        highlighter.Highlight(geometryObject);
    }

    public void NotHover()
    {
        highlighter.UnHighlight();
    }

    public void Escape()
    {
        drawManager.Idle();
        appCore.SetEscapeMenuState();
    }
}
