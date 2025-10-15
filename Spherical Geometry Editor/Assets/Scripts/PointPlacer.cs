using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPlacer : MonoBehaviour
{

    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
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
