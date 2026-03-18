using Unity.Mathematics;
using UnityEngine;

public class GreatCircleSegmentSmallCircleIntersectCommand : ICommand
{
    GreatCircleSegment greatCircleSegment;
    SmallCircle smallCircle;
    GameObject prefab;
    IntersectionPoint point1Script;
    IntersectionPoint point2Script;
    float epsilon = 0.000001f;

    public GreatCircleSegmentSmallCircleIntersectCommand(GreatCircleSegment greatCircle, SmallCircle smallCircle, GameObject prefab)
    {
        this.greatCircleSegment = greatCircle;
        this.smallCircle = smallCircle;
        this.prefab = prefab;
    }

    public void Execute()
    {
        Vector3[] intersections = CalculateIntersections(greatCircleSegment, smallCircle);

        if (intersections.Length == 0)
        {
            return;
        }
        if (intersections[0] != Vector3.zero)
        {
            GameObject point1 = MonoBehaviour.Instantiate(prefab);
            point1.transform.position = intersections[0];
            point1Script = point1.GetComponent<IntersectionPoint>();
            point1.name = "intersection point 1";
            point1Script.SetRecalculate(greatCircleSegment, smallCircle, (curve1, curve2) =>
            {
                Vector3[] intersections = CalculateIntersections(curve1 as GreatCircleSegment, curve2 as SmallCircle);
                if (intersections.Length == 0)
                {
                    return Vector3.zero;
                }
                else
                {
                    return intersections[0];
                }
            });
            greatCircleSegment.Subscirbe(point1Script);
            smallCircle.Subscirbe(point1Script);
        }

        if (intersections[1] != Vector3.zero)
        {
            GameObject point2 = MonoBehaviour.Instantiate(prefab);
            point2.transform.position = intersections[1];
            point2.name = "intersection point 2";
            point2Script = point2.GetComponent<IntersectionPoint>();
            point2Script.SetRecalculate(greatCircleSegment, smallCircle, (curve1, curve2) =>
            {
                Vector3[] intersections = CalculateIntersections(curve1 as GreatCircleSegment, curve2 as SmallCircle);
                if (intersections.Length == 0)
                {
                    return Vector3.zero;
                }
                else
                {
                    return intersections[1];
                }
            });
            greatCircleSegment.Subscirbe(point2Script);
            smallCircle.Subscirbe(point2Script);
        }
    }

    private Vector3[] CalculateIntersections(GreatCircleSegment greatCircleSegment, SmallCircle smallCircle)
    {
        Vector3 greatCircleSegmentCenter = greatCircleSegment.Center;
        Vector3 greatCircleSegmentNormal = greatCircleSegment.NormalOfPlane.normalized;
        Vector3[] greatCircleSegmentEndPoints = greatCircleSegment.GetEndpoints();
        Vector3 smallCircleCenter = smallCircle.Center;
        Vector3 smallCircleNormal = smallCircle.NormalOfPlane.normalized;

        // Intersection of Two Planes John Krumm Microsoft Research Redmond, WA, USA 5 / 15 / 00

        // csak akkor lehet 0 a determinánsa az A-nak ha párhuzamos a 2 sík vagy egybe esik
        float[,] A = {
            {2, 0, 0, greatCircleSegmentNormal.x, smallCircleNormal.x},
            {0, 2, 0, greatCircleSegmentNormal.y, smallCircleNormal.y},
            {0, 0, 2, greatCircleSegmentNormal.z, smallCircleNormal.z},
            {greatCircleSegmentNormal.x, greatCircleSegmentNormal.y, greatCircleSegmentNormal.z, 0, 0},
            {smallCircleNormal.x, smallCircleNormal.y, smallCircleNormal.z, 0, 0}
        };

        float[] B = { 0, 0, 0, Vector3.Dot(greatCircleSegmentCenter, greatCircleSegmentNormal), Vector3.Dot(smallCircleCenter, smallCircleNormal) };

        float DetA = Determinant(A, 5);

        if (DetA == 0)
        {
            Debug.Log("paralell");
            return new Vector3[0];
        }

        //cramer szabály, Juhász Tibor linalg jegyzet

        float[,] delta0 = {
            {B[0], A[0, 1], A[0, 2], A[0, 3], A[0, 4]},
            {B[1], A[1, 1], A[1, 2], A[1, 3], A[1, 4]},
            {B[2], A[2, 1], A[2, 2], A[2, 3], A[2, 4]},
            {B[3], A[3, 1], A[3, 2], A[3, 3], A[3, 4]},
            {B[4], A[4, 1], A[4, 2], A[4, 3], A[4, 4]}
        };

        float[,] delta1 = {
            {A[0, 0], B[0], A[0, 2], A[0, 3], A[0, 4]},
            {A[1, 0], B[1], A[1, 2], A[1, 3], A[1, 4]},
            {A[2, 0], B[2], A[2, 2], A[2, 3], A[2, 4]},
            {A[3, 0], B[3], A[3, 2], A[3, 3], A[3, 4]},
            {A[4, 0], B[4], A[4, 2], A[4, 3], A[4, 4]}
        };

        float[,] delta2 = {
            {A[0, 0], A[0, 1], B[0], A[0, 3], A[0, 4]},
            {A[1, 0], A[1, 1], B[1], A[1, 3], A[1, 4]},
            {A[2, 0], A[2, 1], B[2], A[2, 3], A[2, 4]},
            {A[3, 0], A[3, 1], B[3], A[3, 3], A[3, 4]},
            {A[4, 0], A[4, 1], B[4], A[4, 3], A[4, 4]}
        };


        Vector3 d = Vector3.Cross(greatCircleSegmentNormal, smallCircleNormal).normalized;
        float x0 = Determinant(delta0, 5) / DetA;
        float y0 = Determinant(delta1, 5) / DetA;
        float z0 = Determinant(delta2, 5) / DetA;

        // gömb egyenes metszés pont 

        float a = d.x * d.x + d.y * d.y + d.z * d.z;
        float b = 2 * (d.x * x0 + d.y * y0 + d.z * z0);
        float c = x0 * x0 + y0 * y0 + z0 * z0 - 1;

        if ((b * b - 4 * a * c) < 0)
        {
            Debug.Log((b * b - 4 * a * c));
            Debug.Log("no intersection");
            return new Vector3[0];
        }

        float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
        float p2 = (-b - math.sqrt(b * b - 4 * a * c)) / (2 * a);

        Vector3[] possibleIntersections = {
            new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z),
            new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z)
        };

        Vector3[] intersections ={
            Vector3.zero,
            Vector3.zero
        };

        if (Vector3.Angle(greatCircleSegmentEndPoints[0], greatCircleSegmentEndPoints[1]) + epsilon >= Vector3.Angle(greatCircleSegmentEndPoints[0], possibleIntersections[0]) + Vector3.Angle(greatCircleSegmentEndPoints[1], possibleIntersections[0]))
        {
            intersections[0] = possibleIntersections[0];
        }

        if (Vector3.Angle(greatCircleSegmentEndPoints[0], greatCircleSegmentEndPoints[1]) + epsilon >= Vector3.Angle(greatCircleSegmentEndPoints[0], possibleIntersections[1]) + Vector3.Angle(greatCircleSegmentEndPoints[1], possibleIntersections[1]))
        {
            intersections[1] = possibleIntersections[1];
        }

        return intersections;
    }

    public float Determinant(float[,] matrix, int n)
    {
        float det = 0;
        if (n == 1)
        {
            det = matrix[0, 0];
        }
        else if (n == 2)
        {
            det = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                float[,] subMatrix = new float[n - 1, n - 1];
                for (int j = 1; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (k < i)
                        {
                            subMatrix[j - 1, k] = matrix[j, k];
                        }
                        else if (k > i)
                        {
                            subMatrix[j - 1, k - 1] = matrix[j, k];
                        }
                    }
                }
                det += Mathf.Pow(-1, i) * matrix[0, i] * Determinant(subMatrix, n - 1);
            }
        }

        return det;
    }

    public void UnExecute()
    {
        point1Script.Destroy();
        point2Script.Destroy();
    }
}
