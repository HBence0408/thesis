using Unity.Mathematics;
using UnityEngine;

public class SmallCircleSmallCircleIntersectCommand : ICommand
{
    SmallCircle smallCircle1;
    SmallCircle smallCircle2;
    GameObject prefab;
    IntersectionPoint point1Script;
    IntersectionPoint point2Script;

    public SmallCircleSmallCircleIntersectCommand(SmallCircle smallCircle1, SmallCircle smallCircle2, GameObject prefab)
    {
        this.smallCircle1 = smallCircle1;
        this.smallCircle2 = smallCircle2;
        this.prefab = prefab;
    }

    public void Execute()
    {
        Vector3[] intersections = CalculateIntersections(smallCircle1, smallCircle2);

        if(intersections.Length == 0)
        {
            return;
        }

        GameObject point1 = MonoBehaviour.Instantiate(prefab);
        point1.transform.position = intersections[0];
        point1Script = point1.GetComponent<IntersectionPoint>();
        point1.name = "intersection point 1";

        point1Script.SetRecalculate(smallCircle1, smallCircle2, (curve1, curve2) =>
        {
            Vector3[] intersections = CalculateIntersections(curve1 as SmallCircle, curve2 as SmallCircle);
            if (intersections.Length == 0)
            {
                return Vector3.zero;
            }
            else
            {
                return intersections[0];
            }
        });
        smallCircle1.Subscirbe(point1Script);
        smallCircle2.Subscirbe(point1Script);
        GameObject point2 = MonoBehaviour.Instantiate(prefab);
        point2.transform.position = intersections[1];
        point2.name = "intersection point 2";
        point2Script = point2.GetComponent<IntersectionPoint>();
        point2Script.SetRecalculate(smallCircle1, smallCircle2, (curve1, curve2) =>
        {
            Vector3[] intersections = CalculateIntersections(curve1 as SmallCircle, curve2 as SmallCircle);
            if (intersections.Length == 0)
            {
                return Vector3.zero;
            }
            else
            {
                return intersections[1];
            }
        });
        smallCircle1.Subscirbe(point2Script);
        smallCircle2.Subscirbe(point2Script);
    }

    private Vector3[] CalculateIntersections(SmallCircle smallCircle1, SmallCircle smallCircle2)
    {
        Vector3 smallCircle1Center = this.smallCircle1.Center;
        Vector3 smallCircle1Normal = this.smallCircle1.NormalOfPlane.normalized;
        Vector3 smallCircle2Center = this.smallCircle2.Center;
        Vector3 smallCircle2Normal = this.smallCircle2.NormalOfPlane.normalized;

        // Intersection of Two Planes John Krumm Microsoft Research Redmond, WA, USA 5 / 15 / 00

        // csak akkor lehet 0 a determinánsa az A-nak ha párhuzamos a 2 sík vagy egybe esik
        float[,] A = {
            {2, 0, 0, smallCircle1Normal.x, smallCircle2Normal.x},
            {0, 2, 0, smallCircle1Normal.y, smallCircle2Normal.y},
            {0, 0, 2, smallCircle1Normal.z, smallCircle2Normal.z},
            {smallCircle1Normal.x, smallCircle1Normal.y, smallCircle1Normal.z, 0, 0},
            {smallCircle2Normal.x, smallCircle2Normal.y, smallCircle2Normal.z, 0, 0}
        };

        float[] B = { 0, 0, 0, Vector3.Dot(smallCircle1Center, smallCircle1Normal), Vector3.Dot(smallCircle2Center, smallCircle2Normal) };

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


        Vector3 d = Vector3.Cross(smallCircle1Normal, smallCircle2Normal).normalized;
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
        Vector3[] intersections = {
            new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z),
            new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z)
        };

        return intersections;
    }

    private float Determinant(float[,] matrix, int n)
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
