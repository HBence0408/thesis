using Unity.Mathematics;
using UnityEngine;

public class GreatCircleSmallCircleIntersectCommand : ICommand
{

    GreatCircle greatCircle;
    SmallCircle smallCircle;
    GameObject prefab;
    IntersectionPoint point1Script;
    IntersectionPoint point2Script;

    public GreatCircleSmallCircleIntersectCommand(GreatCircle greatCircle, SmallCircle smallCircle, GameObject prefab)
    {
        this.greatCircle = greatCircle;
        this.smallCircle = smallCircle;
        this.prefab = prefab;
    }

    public void Execute()
    {
        Vector3 greatCircleCenter =  this.greatCircle.Center;
        Vector3[] greatCircleVectors = this.greatCircle.OrthogonalVectrosOfthePlane;
        Vector3 greatCircleNormal = this.greatCircle.NormalOfPlane.normalized;
        Vector3 smallCircleCenter = this.smallCircle.Center;
        Vector3[] smallCircleVectors = this.smallCircle.OrthogonalVectrosOfthePlane;
        Vector3 smallCircleNormal = this.smallCircle.NormalOfPlane.normalized;

        // Intersection of Two Planes John Krumm Microsoft Research Redmond, WA, USA 5 / 15 / 00

        // csak akkor lehet 0 a determinánsa az A-nak ha párhuzamos a 2 sík vagy egybe esik
        float[,] A = {
            {2, 0, 0, greatCircleNormal.x, smallCircleNormal.x},
            {0, 2, 0, greatCircleNormal.y, smallCircleNormal.y},
            {0, 0, 2, greatCircleNormal.z, smallCircleNormal.z},
            {greatCircleNormal.x, greatCircleNormal.y, greatCircleNormal.z, 0, 0},
            {smallCircleNormal.x, smallCircleNormal.y, smallCircleNormal.z, 0, 0}
        };

        float[] B = {0, 0, 0, Vector3.Dot(greatCircleCenter, greatCircleNormal), Vector3.Dot(smallCircleCenter, smallCircleNormal) };

        float DetA = Determinant(A, 5);

        if (DetA == 0)
        {
            Debug.Log("paralell");
            return;
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


        Vector3 d = Vector3.Cross(greatCircleNormal, smallCircleNormal).normalized;
        float x0 = Determinant(delta0, 5)/DetA;
        float y0 = Determinant(delta1, 5)/DetA;
        float z0 = Determinant(delta2, 5)/DetA;

        // gömb egyenes metszés pont 

        float a = d.x * d.x + d.y * d.y + d.z * d.z;
        float b = 2 * (d.x * x0 + d.y * y0 + d.z * z0);
        float c = x0 * x0 + y0 * y0 + z0 * z0 - 1;

        if ((b*b - 4*a*c ) < 0)
        {
            Debug.Log((b * b - 4 * a * c));
            Debug.Log("no intersection");
            return;
        }

        float p1 = (-b + math.sqrt(b*b-4*a*c))/(2*a);
        float p2 = (-b - math.sqrt(b*b-4*a*c))/(2*a);

        GameObject point1 = MonoBehaviour.Instantiate(prefab);
        point1.transform.position = new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z);
        point1Script = point1.GetComponent<IntersectionPoint>();
        point1.name = "intersection point 1";
        //TODO
        // kiemelni külön 1 függvénybe és csak át adni azt a függvényt?
        point1Script.SetRecalculate(greatCircle, smallCircle, (curve1, curve2) =>
        {
            Vector3 greatCircleCenter = curve1.Center;
            Vector3[] greatCircleVectors = curve1.OrthogonalVectrosOfthePlane;
            Vector3 greatCircleNormal = curve1.NormalOfPlane.normalized;
            Vector3 smallCircleCenter = curve2.Center;
            Vector3[] smallCircleVectors = curve2.OrthogonalVectrosOfthePlane;
            Vector3 smallCircleNormal = curve2.NormalOfPlane.normalized;

            float[,] A = {
            {2, 0, 0, greatCircleNormal.x, smallCircleNormal.x},
            {0, 2, 0, greatCircleNormal.y, smallCircleNormal.y},
            {0, 0, 2, greatCircleNormal.z, smallCircleNormal.z},
            {greatCircleNormal.x, greatCircleNormal.y, greatCircleNormal.z, 0, 0},
            {smallCircleNormal.x, smallCircleNormal.y, smallCircleNormal.z, 0, 0}
        };

            float[] B = { 0, 0, 0, Vector3.Dot(greatCircleCenter, greatCircleNormal), Vector3.Dot(smallCircleCenter, smallCircleNormal) };

            float DetA = Determinant(A, 5);

            if (DetA == 0)
            {
                Debug.Log("paralell");
                return new Vector3(0,0,0);
            }

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


            Vector3 d = Vector3.Cross(greatCircleNormal, smallCircleNormal).normalized;
            float x0 = Determinant(delta0, 5) / DetA;
            float y0 = Determinant(delta1, 5) / DetA;
            float z0 = Determinant(delta2, 5) / DetA;

            float a = d.x * d.x + d.y * d.y + d.z * d.z;
            float b = 2 * (d.x * x0 + d.y * y0 + d.z * z0);
            float c = x0 * x0 + y0 * y0 + z0 * z0 - 1;

            if ((b * b - 4 * a * c) < 0)
            {
                Debug.Log((b * b - 4 * a * c));
                Debug.Log("no intersection");
                return new Vector3(0,0,0);
            }

            float p1 = (-b + math.sqrt(b * b - 4 * a * c)) / (2 * a);
            return new Vector3(x0 + p1 * d.x, y0 + p1 * d.y, z0 + p1 * d.z);
        });
        greatCircle.Subscirbe(point1Script);
        smallCircle.Subscirbe(point1Script);
        GameObject point2 = MonoBehaviour.Instantiate(prefab);
        point2.transform.position = new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z);
        point2.name = "intersection point 2";
        point2Script = point2.GetComponent<IntersectionPoint>();
        point2Script.SetRecalculate(greatCircle, smallCircle, (curve1, curve2) =>
        {
            Vector3 greatCircleCenter = curve1.Center;
            Vector3[] greatCircleVectors = curve1.OrthogonalVectrosOfthePlane;
            Vector3 greatCircleNormal = curve1.NormalOfPlane.normalized;
            Vector3 smallCircleCenter = curve2.Center;
            Vector3[] smallCircleVectors = curve2.OrthogonalVectrosOfthePlane;
            Vector3 smallCircleNormal = curve2.NormalOfPlane.normalized;

            float[,] A = {
            {2, 0, 0, greatCircleNormal.x, smallCircleNormal.x},
            {0, 2, 0, greatCircleNormal.y, smallCircleNormal.y},
            {0, 0, 2, greatCircleNormal.z, smallCircleNormal.z},
            {greatCircleNormal.x, greatCircleNormal.y, greatCircleNormal.z, 0, 0},
            {smallCircleNormal.x, smallCircleNormal.y, smallCircleNormal.z, 0, 0}
        };

            float[] B = { 0, 0, 0, Vector3.Dot(greatCircleCenter, greatCircleNormal), Vector3.Dot(smallCircleCenter, smallCircleNormal) };

            float DetA = Determinant(A, 5);

            if (DetA == 0)
            {
                Debug.Log("paralell");
                return new Vector3(0, 0, 0);
            }

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


            Vector3 d = Vector3.Cross(greatCircleNormal, smallCircleNormal).normalized;
            float x0 = Determinant(delta0, 5) / DetA;
            float y0 = Determinant(delta1, 5) / DetA;
            float z0 = Determinant(delta2, 5) / DetA;

            float a = d.x * d.x + d.y * d.y + d.z * d.z;
            float b = 2 * (d.x * x0 + d.y * y0 + d.z * z0);
            float c = x0 * x0 + y0 * y0 + z0 * z0 - 1;

            if ((b * b - 4 * a * c) < 0)
            {
                Debug.Log((b * b - 4 * a * c));
                Debug.Log("no intersection");
                return new Vector3(0, 0, 0);
            }

            float p2 = (-b - math.sqrt(b * b - 4 * a * c)) / (2 * a);
            return new Vector3(x0 + p2 * d.x, y0 + p2 * d.y, z0 + p2 * d.z);
        });
        greatCircle.Subscirbe(point2Script);
        smallCircle.Subscirbe(point2Script);
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
        throw new System.NotImplementedException();
    }
}
