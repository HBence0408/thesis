using UnityEngine;

public class TestDrawer : MonoBehaviour
{
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TubeRenderer.Instance.DrawCircle(point1.transform.position.normalized, point2.transform.position.normalized);
        }
    }
}
