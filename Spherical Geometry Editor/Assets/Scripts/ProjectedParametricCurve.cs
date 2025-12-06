using System.Linq;
using UnityEngine;

public class ProjectedParametricCurve : MonoBehaviour
{
    [SerializeField] private LineRenderer linerederer;
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject origo;
    
    public void SetPoints(Vector3[] points)
    {
        linerederer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            //linerederer.SetPosition(i, points[i]);
            GameObject obj = Instantiate(pointPrefab);
            obj.GetComponent<Transform>().SetParent(this.gameObject.transform);
            obj.transform.position = points[i];
        }

        this.transform.position = origo.transform.position;
    }

}
