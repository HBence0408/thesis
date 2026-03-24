using UnityEditor;
using UnityEngine;

public class DrawGreatCircleCommand :ICommand
{
    private GameObject point1;
    private GameObject point2;
    private GreatCircle greatCircle;
    private SphericalGeometryFactory factory;
    private bool isExecuted;

    public DrawGreatCircleCommand(GameObject point1, GameObject point2, SphericalGeometryFactory factory)
    { 
        this.point1 = point1;
        this.point2 = point2;
        this.factory = factory;
    }

    public void Execute()
    {
        greatCircle = factory.CreateGreatCircle(point1.transform.position.normalized, point2.transform.position.normalized);
        greatCircle.AddContollPoints(point1.GetComponent<ControllPoint>(), point2.GetComponent<ControllPoint>());
        isExecuted = true;
    }

    public void ReExecute()
    {
        greatCircle.Restore();
        isExecuted = true;
    }

    public void UnExecute()
    {
        greatCircle.SoftDelete();
        isExecuted = false;
    }

    public void Delete()
    {
        if (!isExecuted && greatCircle != null)
        {
            greatCircle.HardDelete();
        }
        point1 = null;
        point2 = null;
        greatCircle = null;
        factory = null;
    }
}
