using UnityEngine;

public class TestDrawer : MonoBehaviour
{
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TubeRenderer.Instance.Draw(5, point1.transform.position, point2.transform.position);
        }
    }
}
