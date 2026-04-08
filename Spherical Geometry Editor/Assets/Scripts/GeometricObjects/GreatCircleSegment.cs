using UnityEngine;

public class GreatCircleSegment : ParametricCurve
{
    private ParametricCurveMeshGenerator meshGenerator;

    public override void OnChanged()
    {
        meshGenerator.CreateGreatCircleSegmentMesh(point1.transform.position.normalized, point2.transform.position.normalized, this.CreateMesh);
        Notify();
    }

    public Vector3[] GetEndpoints()
    {
        return new Vector3[] { point1.transform.position.normalized, point2.transform.position.normalized };
    }

    public override Vector3 GetClosestPoint(Vector3 pos)
    {
        float distance = normaleOfPlane.x * pos.x + normaleOfPlane.y * pos.y + normaleOfPlane.z * pos.z - (normaleOfPlane.x * center.x + normaleOfPlane.y * center.y + normaleOfPlane.z * center.z);
        distance = distance / normaleOfPlane.magnitude;
        Vector3 newPos = pos - distance * normaleOfPlane.normalized;
        Vector3[] endpoints = GetEndpoints();
        if (Vector3.Angle(endpoints[0], endpoints[1]) + 0.0001 >= Vector3.Angle(endpoints[0], newPos) + Vector3.Angle(endpoints[1], newPos))
        {
            return newPos.normalized;
        }
        if (Vector3.Distance(pos, endpoints[0]) > Vector3.Distance(pos, endpoints[1]))
        {
            return endpoints[1].normalized;
        }
        else
        {
            return endpoints[0].normalized;
        }
    }

    public void SetMeshGenerator(ParametricCurveMeshGenerator meshGenerator)
    {
        this.meshGenerator = meshGenerator;
    }
}
