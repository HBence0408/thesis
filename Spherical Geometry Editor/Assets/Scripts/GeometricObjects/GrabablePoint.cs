using UnityEngine;

public class GrabablePoint : ControllPoint
{
    private void OnMouseDrag()
    {
        if (!DrawManager.Instance.MoveState.IsActive)
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(-Camera.main.transform.forward, transform.position);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            transform.position = point.normalized;
        }
    }
}
