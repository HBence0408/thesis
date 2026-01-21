using UnityEngine;

public class GreatCircle : ParametricCurve
{
    public override void OnChanged()
    {
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, this.CreateMesh);
    }

}
