using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public interface IDrawManager
{
    public event Action<IGeometryObject, Vector3> OnDown;
    public event Action<IGeometryObject, Vector3> OnUp;
    public event Action<IGeometryObject, Vector3> OnHold;

    public void OnLeftMouseDown(IGeometryObject geometryObject, Vector3 hitpoint);

    public void OnLeftMouseUp(IGeometryObject geometryObject, Vector3 hitpoint);

    public void OnLeftMouseHold(IGeometryObject geometryObject, Vector3 hitpoint);

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

