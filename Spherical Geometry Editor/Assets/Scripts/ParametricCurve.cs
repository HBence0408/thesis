using UnityEngine;

public class ParametricCurve : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    public void CreateMesh(Vector3[] vertices, int[] triangles)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;
    }
}
