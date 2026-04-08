using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager 
{
    private IRepository repository;
    private SphericalGeometryLoadFactory factory;
    private Linker linker;

    public LoadManager(IRepository repository, SphericalGeometryLoadFactory factory, Linker linker)
    {
        this.factory = factory;
        this.linker = linker;
        this.repository = repository;
    }

    public void Load(string fileName)
    {
        string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + fileName + ".json");
        SaveFile saveFile = JsonConvert.DeserializeObject<SaveFile>(json);

        Debug.Log("Loaded from: " + Application.persistentDataPath + "/" + fileName + ".json");

        Debug.Log(json);

        //factory.Create(saveFile);
        List<(GrabablePoint, GrabablePointDto)> grabablePoints = new List<(GrabablePoint, GrabablePointDto)>();
        List<(LimitedPoint, LimitedPointDto)> limitedPoints = new List<(LimitedPoint, LimitedPointDto)>();
        List<(AntipodalPoint, AntipodalPointDto)> antipodalPoints = new List<(AntipodalPoint, AntipodalPointDto)>();
        List<(MidPoint, MidPointDto)> midPoints = new List<(MidPoint, MidPointDto)>();
        List<(PolePoint, PolePointDto)> polePoints = new List<(PolePoint, PolePointDto)>();
        List<(ShadowPolePoint, ShadowPolePointDto)> shadowPolePoints = new List<(ShadowPolePoint, ShadowPolePointDto)>();
        List<(IntersectionPoint, IntersectionPointDto)> intersectionPoints = new List<(IntersectionPoint, IntersectionPointDto)>();
        List<(GreatCircle, GreatCircleDto)> greatCircles = new List<(GreatCircle, GreatCircleDto)>();
        List<(SmallCircle, SmallCircleDto)> smallCircles = new List<(SmallCircle, SmallCircleDto)>();
        List<(GreatCircleSegment, GreatCircleSegmentDto)> greatCircleSegments = new List<(GreatCircleSegment, GreatCircleSegmentDto)>();

        foreach (GrabablePointDto i in saveFile.GrabablePoints)
        {
            GrabablePoint point = factory.CreateGrabablepoint(i);
            grabablePoints.Add((point, i));
            repository.Store(point);
        }

        foreach (LimitedPointDto i in saveFile.LimitedPoints)
        {
            LimitedPoint point = factory.CreateLimitedpoint(i);
            limitedPoints.Add((point, i));
            repository.Store(point);
        }

        foreach (AntipodalPointDto i in saveFile.AntipodalPoints)
        {
            AntipodalPoint point = factory.CreateAntipodalpoint(i);
            antipodalPoints.Add((point, i));
            repository.Store(point);
        }

        foreach (MidPointDto i in saveFile.MidPoints)
        {
            MidPoint point = factory.CreateMidpoint(i);
            midPoints.Add((point, i));
            repository.Store(point);
        }

        foreach (PolePointDto i in saveFile.PolePoints)
        {
            PolePoint point = factory.CreatePolepoint(i);
            polePoints.Add((point, i));
            repository.Store(point);
        }

        foreach (ShadowPolePointDto i in saveFile.ShadowPolePoints)
        {
            ShadowPolePoint point = factory.CreateShadowPolepoint(i);
            shadowPolePoints.Add((point, i));
            repository.Store(point);
        }

        foreach (GreatCircleDto i in saveFile.GreatCircles)
        {
            GreatCircle curve = factory.CreateGreatCircle(i);
            greatCircles.Add((curve, i));
            repository.Store(curve);
        }

        foreach (GreatCircleSegmentDto i in saveFile.GreatCircleSegments)
        {
            GreatCircleSegment curve = factory.CreateGreatCircleSegment(i);
            greatCircleSegments.Add((curve, i));
            repository.Store(curve);
        }

        foreach (SmallCircleDto i in saveFile.SmallCircles)
        {
            SmallCircle curve = factory.CreateSmallCircle(i);
            smallCircles.Add((curve, i));
            repository.Store(curve);
        }

        foreach (IntersectionPointDto i in saveFile.IntersectionPoints)
        {
            IntersectionPoint point = factory.CreateIntersectionpoint(i);
            intersectionPoints.Add((point, i));
            repository.Store(point);
        }

        foreach ((LimitedPoint, LimitedPointDto) i in limitedPoints)
        {
            linker.Link(i.Item1, i.Item2);
        }

        foreach ((AntipodalPoint, AntipodalPointDto) i in antipodalPoints)
        {
            linker.Link(i.Item1, i.Item2);
        }

        foreach ((MidPoint, MidPointDto) i in midPoints)
        {
            linker.Link(i.Item1, i.Item2);
        }

        foreach ((PolePoint, PolePointDto) i in polePoints)
        {
            linker.Link(i.Item1, i.Item2);
        }

        foreach ((ShadowPolePoint, ShadowPolePointDto) i in shadowPolePoints)
        {
            linker.Link(i.Item1, i.Item2);
        }

        foreach ((IntersectionPoint, IntersectionPointDto) i in intersectionPoints)
        {
            linker.Link(i.Item1, i.Item2);
        }

        foreach ((GreatCircle, GreatCircleDto) i in greatCircles)
        {
            linker.Link(i.Item1, i.Item2);
        }

        foreach ((GreatCircleSegment, GreatCircleSegmentDto) i in greatCircleSegments)
        {
            linker.Link(i.Item1, i.Item2);
        }

        foreach ((SmallCircle, SmallCircleDto) i in smallCircles)
        {
            linker.Link(i.Item1, i.Item2);
        }
    }
}
