using UnityEngine;

public class EditorState : IAppState
{
    private IInputHandler inputHandler;
    private SideMenu sideMenu;
    private IDrawManager drawManager;
    private ICommandInvoker commandInvoker;
    private CameraMovement cameraMovement;
    private IHighlighter highlighter;

    public EditorState(IInputHandler inputHandler, SideMenu sideMenu, IDrawManager drawManager, ICommandInvoker commandInvoker, CameraMovement cameraMovement, IHighlighter highlighter)
    {
        this.inputHandler = inputHandler;
        this.sideMenu = sideMenu;
        this.drawManager = drawManager;
        this.commandInvoker = commandInvoker;
        this.cameraMovement = cameraMovement;
        this.highlighter = highlighter;
    }

    public void OnEnter()
    {
        Debug.Log("editor state on enter");

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

    public void OnExit()
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

    private void OnLeftMouseDown(IGeometryObject geometryObject, Vector3 hitpoint)
    {
        drawManager.OnLeftMouseDown(geometryObject, hitpoint);
    }

    private void OnLeftMousenHold(IGeometryObject geometryObject, Vector3 hitpoint)
    {
        drawManager.OnLeftMouseHold(geometryObject, hitpoint);
    }

    private void OnLeftMouseUp(IGeometryObject geometryObject, Vector3 hitpoint)
    {
        drawManager.OnLeftMouseUp(geometryObject, hitpoint);
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
        AppCore.Instance.SetColorPickState();
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
        highlighter.Highlight(geometryObject);
    }

    public void NotHover()
    {
        highlighter.UnHighlight();
    }

    public void Escape()
    {
        drawManager.Idle();
        OnExit();
        AppCore.Instance.SetEscapeMenuState();
    }
}
