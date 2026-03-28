using UnityEngine;

public class GreatCircle : ParametricCurve
{
    public override Vector3 GetClosestPoint(Vector3 pos)
    {
        float distance = normaleOfPlane.x * pos.x + normaleOfPlane.y * pos.y + normaleOfPlane.z * pos.z - (normaleOfPlane.x * center.x + normaleOfPlane.y * center.y + normaleOfPlane.z * center.z);
        distance = distance / normaleOfPlane.magnitude;
        return (pos - distance * normaleOfPlane.normalized).normalized;
    }

    public override void OnChanged()
    {
        ParametricCurveMeshGenerator.Instance.CreateGreatCircleMesh(point1.transform.position.normalized, point2.transform.position.normalized, this.CreateMesh);
        Notify();
    }
}
