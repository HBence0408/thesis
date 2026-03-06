using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IntersectCommand : ICommand
{
    private List<Vector3> positions = new List<Vector3>();
    private List<GameObject> points = new List<GameObject>();
    private List<GameObject> intersectedCurves = new List<GameObject>();

    public IntersectCommand(List<Vector3> positions, List<GameObject> points, List<GameObject> intersectedCurves)
    {
        this.positions = positions;
        this.points = points;
        this.intersectedCurves = intersectedCurves;
    }

    public void Execute()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            points[i].transform.position = positions[i];

        }
    }

    public void UnExecute()
    {
        throw new System.NotImplementedException();
    }
}
