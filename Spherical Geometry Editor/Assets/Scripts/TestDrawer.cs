using UnityEngine;

public class TestDrawer : MonoBehaviour
{
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TubeRenderer.Instance.Draw(5, point1.transform.position, point2.transform.position);
        }
    }
}
