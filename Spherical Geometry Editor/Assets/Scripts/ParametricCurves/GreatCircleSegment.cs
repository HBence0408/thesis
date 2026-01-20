using UnityEngine;

public class GreatCircleSegment : ParametricCurve
{
    public override void OnChanged()
    {
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleSegmentMesh(point1.transform.position.normalized, point2.transform.position.normalized, this.CreateMesh);
    }
}
