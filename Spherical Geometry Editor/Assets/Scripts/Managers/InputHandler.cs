using System;
using UnityEngine;

public class InputHandler : MonoBehaviour, IInputHandler
{
    public event Action<IGeometryObject, Vector3> OnLeftMouseButtonDown;
    public event Action<IGeometryObject, Vector3> OnLeftMouseButtonUp;
    public event Action<IGeometryObject, Vector3> OnLeftMouseButtonHold;
    public event Action OnWHoldDown;
    public event Action OnSHoldDown;
    public event Action OnDHoldDown;
    public event Action OnAHoldDown;
    public event Action OnEHoldDown;
    public event Action OnQHoldDown;
    public event Action<IGeometryObject> OnHover;
    public event Action OnNotHover;
    public event Action OnEscapeKeyDown;
    private IGeometryObject currentlyHovered;
    private Vector3 lastValidHitPoint;

    private void Update()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            lastValidHitPoint = hit.point;
            if (hit.collider.gameObject.TryGetComponent<IGeometryObject>(out IGeometryObject geometryObject))
            {
                OnHover?.Invoke(geometryObject);
                currentlyHovered = geometryObject;
            }
            else
            {
                OnNotHover?.Invoke();
                currentlyHovered = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnLeftMouseButtonDown?.Invoke(currentlyHovered, lastValidHitPoint);
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnLeftMouseButtonUp?.Invoke(currentlyHovered, lastValidHitPoint);
        }
        else if (Input.GetMouseButton(0))
        {
            OnLeftMouseButtonHold?.Invoke(currentlyHovered, lastValidHitPoint);
        }

        if (Input.GetKey(KeyCode.W))
        {
            OnWHoldDown?.Invoke();
        }
        if (Input.GetKey(KeyCode.S))
        {
            OnSHoldDown?.Invoke();
        }
        if (Input.GetKey(KeyCode.A))
        {
            OnAHoldDown?.Invoke();
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnDHoldDown?.Invoke();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            OnQHoldDown?.Invoke();
        }
        if (Input.GetKey(KeyCode.E))
        {
            OnEHoldDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapeKeyDown?.Invoke();
        }


    }
}
