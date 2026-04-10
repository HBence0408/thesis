using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;

public interface IDrawManager
{
    event Action OnDown;
    event Action OnUp;
    event Action OnHold;

    public void OnLeftMouseDown();

    public void OnLeftMouseUp();

    public void OnLeftMouseHold();

    public void SetState(DrawingState state);

    public void Idle();

    public void ResetState();

    public void DrawLine();

    public void DrawSegment();

    public void DrawPoint();

    public void DrawCircle();

    public void MovePoint();

    public void Intersect();

    public void Delete();

    public void PlaceAntipodalPoint();

    public void PlacePolePoints();

    public void PlaceMidPoint();

    public void DrawRightAngleGreatCircle();

    public void ColorBlack();

    public void ColorGrey();

    public void ColorBlue();

    public void ColorRed();

    public void ColorGreen();

    public void ColorMagenta();
}

