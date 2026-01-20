using UnityEngine;

public class SmallCircle : ParametricCurve
{
    public override void OnChanged()
    {
        ParametricCurveMeshGenerator.Instance.CreateSmallCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, this.CreateMesh);
    }
}
