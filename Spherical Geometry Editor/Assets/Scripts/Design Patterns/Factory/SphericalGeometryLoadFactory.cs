using System;
using Unity.Mathematics;
using UnityEngine;

public class SphericalGeometryLoadFactory : ISphericalGeometryLoadFactory
{

    GameObject GrabablePointPrefab;
    GameObject IntersectionPointPrefab;
    GameObject SmallCirclePrefab;
    GameObject GreatCirclePrefab;
    GameObject GreatCircleSegmentPrefab;
    GameObject LimitedPointPrefab;
    GameObject AntipodalPointPrefab;
    GameObject PolePointPrefab;
    GameObject ShadowPolePointPrefab;
    GameObject MidPointPrefab;
    IParametricCurveMeshGenerator meshGenerator;

    public SphericalGeometryLoadFactory(IParametricCurveMeshGenerator parametricCurveMeshGenerator ,GameObject grabablePointPrefab, GameObject intersectionPointPrefab, GameObject smallCirclePrefab, GameObject greatCirclePrefab, GameObject greatCircleSegmentPrefab, GameObject limitedPointPrefab, GameObject antipodalPointPrefab, GameObject polePointPrefab, GameObject midPointPrefab, GameObject shadowPolePointPrefab)
    {
        this.GrabablePointPrefab = grabablePointPrefab;
        this.IntersectionPointPrefab = intersectionPointPrefab;
        this.SmallCirclePrefab = smallCirclePrefab;
        this.GreatCirclePrefab = greatCirclePrefab;
        this.GreatCircleSegmentPrefab = greatCircleSegmentPrefab;
        this.LimitedPointPrefab = limitedPointPrefab;
        this.AntipodalPointPrefab = antipodalPointPrefab;
        this.PolePointPrefab = polePointPrefab;
        this.ShadowPolePointPrefab = shadowPolePointPrefab;
        this.MidPointPrefab = midPointPrefab;
        this.meshGenerator = parametricCurveMeshGenerator;
    }

    public GrabablePoint CreateGrabablepoint(GrabablePointDto dto)
    {
        GameObject grabablePoint = MonoBehaviour.Instantiate(GrabablePointPrefab);
        grabablePoint.transform.position = new Vector3(dto.Position.X, dto.Position.Y, dto.Position.Z);
        GrabablePoint script = grabablePoint.GetComponent<GrabablePoint>();
        script.Id = dto.id;
        script.SetColor(new Color(dto.Color.R, dto.Color.G, dto.Color.B));
        return script;
    }

    public LimitedPoint CreateLimitedpoint(LimitedPointDto dto)
    {
        GameObject limitedPoint = MonoBehaviour.Instantiate(LimitedPointPrefab);
        limitedPoint.transform.position = new Vector3(dto.Position.X, dto.Position.Y, dto.Position.Z);
        LimitedPoint script = limitedPoint.GetComponent<LimitedPoint>();
        script.Id = dto.id;
        script.SetColor(new Color(dto.Color.R, dto.Color.G, dto.Color.B));
        return script;
    }

    public PolePoint CreatePolepoint(PolePointDto dto)
    {
        GameObject PolePoint = MonoBehaviour.Instantiate(PolePointPrefab);
        PolePoint.transform.position = new Vector3(dto.Position.X, dto.Position.Y, dto.Position.Z);
        PolePoint script = PolePoint.GetComponent<PolePoint>();
        script.Id = dto.id;
        script.SetColor(new Color(dto.Color.R, dto.Color.G, dto.Color.B));
        return script;
    }

    public ShadowPolePoint CreateShadowPolepoint(ShadowPolePointDto dto)
    {
        GameObject ShadowPolePoint = MonoBehaviour.Instantiate(ShadowPolePointPrefab);
        ShadowPolePoint.transform.position = new Vector3(dto.Position.X, dto.Position.Y, dto.Position.Z);
        ShadowPolePoint script = ShadowPolePoint.GetComponent<ShadowPolePoint>();
        script.Id = dto.id;
        script.SetColor(new Color(dto.Color.R, dto.Color.G, dto.Color.B));
        return script;
    }

    public AntipodalPoint CreateAntipodalpoint(AntipodalPointDto dto)
    {
        GameObject antipodalPoint = MonoBehaviour.Instantiate(AntipodalPointPrefab);
        antipodalPoint.transform.position = new Vector3(dto.Position.X, dto.Position.Y, dto.Position.Z);
        AntipodalPoint script = antipodalPoint.GetComponent<AntipodalPoint>();
        script.Id = dto.id;
        script.SetColor(new Color(dto.Color.R, dto.Color.G, dto.Color.B));
        return script;
    }

    public MidPoint CreateMidpoint(MidPointDto dto)
    {
        GameObject midPoint = MonoBehaviour.Instantiate(MidPointPrefab);
        midPoint.transform.position = new Vector3(dto.Position.X, dto.Position.Y, dto.Position.Z);
        MidPoint script = midPoint.GetComponent<MidPoint>();
        script.Id = dto.id;
        script.SetColor(new Color(dto.Color.R, dto.Color.G, dto.Color.B));
        return script;
    }

    public GreatCircle CreateGreatCircle(GreatCircleDto dto)
    {
        GameObject greatCircle = MonoBehaviour.Instantiate(GreatCirclePrefab);
        greatCircle.transform.position = new Vector3(0, 0, 0);
        GreatCircle script = greatCircle.GetComponent<GreatCircle>();
        Vector3 point1Pos = new Vector3(dto.ControllPoint1Pos.X, dto.ControllPoint1Pos.Y, dto.ControllPoint1Pos.Z);
        Vector3 point2Pos = new Vector3(dto.ControllPoint2Pos.X, dto.ControllPoint2Pos.Y, dto.ControllPoint2Pos.Z);
        meshGenerator.CreateGreatCircleMesh(point1Pos, point2Pos, script.CreateMesh);
        script.SetMeshGenerator(meshGenerator);
        script.Id = dto.id;
        script.SetColor(new Color(dto.Color.R, dto.Color.G, dto.Color.B));
        return script;
    }

    public SmallCircle CreateSmallCircle(SmallCircleDto dto)
    {
        GameObject smallCircle = MonoBehaviour.Instantiate(SmallCirclePrefab);
        smallCircle.transform.position = new Vector3(0, 0, 0);
        SmallCircle script = smallCircle.GetComponent<SmallCircle>();
        Vector3 point1Pos = new Vector3(dto.ControllPoint1Pos.X, dto.ControllPoint1Pos.Y, dto.ControllPoint1Pos.Z);
        Vector3 point2Pos = new Vector3(dto.ControllPoint2Pos.X, dto.ControllPoint2Pos.Y, dto.ControllPoint2Pos.Z);
        meshGenerator.CreateSmallCircleMesh(point1Pos, point2Pos, script.CreateMesh);
        script.SetMeshGenerator(meshGenerator);
        script.Id = dto.id;
        script.SetColor(new Color(dto.Color.R, dto.Color.G, dto.Color.B));
        return script;
    }

    public GreatCircleSegment CreateGreatCircleSegment(GreatCircleSegmentDto dto)
    {
        GameObject greatCircleSegment = MonoBehaviour.Instantiate(GreatCircleSegmentPrefab);
        greatCircleSegment.transform.position = new Vector3(0, 0, 0);
        GreatCircleSegment script = greatCircleSegment.GetComponent<GreatCircleSegment>();
        Vector3 point1Pos = new Vector3(dto.EndPoint1Pos.X, dto.EndPoint1Pos.Y, dto.EndPoint1Pos.Z);
        Vector3 point2Pos = new Vector3(dto.EndPoint2Pos.X, dto.EndPoint2Pos.Y, dto.EndPoint2Pos.Z);
        meshGenerator.CreateGreatCircleSegmentMesh(point1Pos, point2Pos, script.CreateMesh);
        script.SetMeshGenerator(meshGenerator);
        script.Id = dto.id;
        script.SetColor(new Color(dto.Color.R, dto.Color.G, dto.Color.B));
        return script;
    }

    public IntersectionPoint CreateIntersectionpoint(IntersectionPointDto dto)
    {
        GameObject intersectionPoint = MonoBehaviour.Instantiate(IntersectionPointPrefab);
        intersectionPoint.transform.position = new Vector3(dto.Position.X, dto.Position.Y, dto.Position.Z);
        IntersectionPoint script = intersectionPoint.GetComponent<IntersectionPoint>();
        script.Id = dto.id;
        script.SetColor(new Color(dto.Color.R, dto.Color.G, dto.Color.B));
        return script;
    }
}
