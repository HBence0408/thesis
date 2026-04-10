using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public delegate void CreateMesh(Vector3[] vertices, int[] triangles, Vector3[] pointsInCircle, Vector3 normalOfPlane, Vector3 u, Vector3 v, Vector3 center);

public interface IParametricCurveMeshGenerator
{
    /// <summary>
    /// Létrehoz egy teljes főkör (Great Circle) mesht két pont alapján.
    /// </summary>
    void CreateGreatCircleMesh(Vector3 point1, Vector3 point2, CreateMesh callback);

    /// <summary>
    /// Létrehoz egy főkörívet (Great Circle Segment) két pont között.
    /// </summary>
    void CreateGreatCircleSegmentMesh(Vector3 point1, Vector3 point2, CreateMesh callback);

    /// <summary>
    /// Létrehoz egy kiskört (Small Circle) a középpont és egy kerületi pont alapján.
    /// </summary>
    void CreateSmallCircleMesh(Vector3 point1, Vector3 point2, CreateMesh callback);
}

