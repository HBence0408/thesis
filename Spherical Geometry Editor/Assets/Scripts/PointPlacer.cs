using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPlacer : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("a");
            PlacePoint(pointA);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("b");
            PlacePoint(pointB);
        }

    }

    private void PlacePoint(GameObject point)
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");
            point.transform.position = hit.point;
        }
    }
}
