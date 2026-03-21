using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action OnLeftMouseButtonDown;
    public event Action OnLeftMouseButtonUp;
    public event Action OnLeftMouseButtonHold;

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftMouseButtonDown?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnLeftMouseButtonUp?.Invoke();
        }
        else if (Input.GetMouseButton(0))
        {
            OnLeftMouseButtonHold?.Invoke();
        }
    }
}
