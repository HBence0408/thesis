using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;


public class SaveManager 
{
    private IRepository repository;
    private Mapper mapper;

    public SaveManager(IRepository repository, Mapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public void Save(string fileName)
    {
        SaveFile saveFile = new SaveFile
        {
            FileName = fileName,
            AntipodalPoints = mapper.Map(repository.GetByType<AntipodalPoint>().ToArray()),
            GrabablePoints = mapper.Map(repository.GetByType<GrabablePoint>().ToArray()),
            GreatCircles = mapper.Map(repository.GetByType<GreatCircle>().ToArray()),
            GreatCircleSegments = mapper.Map(repository.GetByType<GreatCircleSegment>().ToArray()),
            IntersectionPoints = mapper.Map(repository.GetByType<IntersectionPoint>().ToArray()),
            LimitedPoints = mapper.Map(repository.GetByType<LimitedPoint>().ToArray()),
            MidPoints = mapper.Map(repository.GetByType<MidPoint>().ToArray()),
            PolePoints = mapper.Map(repository.GetByType<PolePoint>().ToArray()),
            ShadowPolePoints = mapper.Map(repository.GetByType<ShadowPolePoint>().ToArray()),
            SmallCircles = mapper.Map(repository.GetByType<SmallCircle>().ToArray())
        };

        Debug.Log(saveFile.GrabablePoints.Length);
        string json = JsonConvert.SerializeObject(saveFile);
        // string json = JsonUtility.ToJson(saveFile, true);
        //  DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SaveFile));
        Debug.Log(json);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + saveFile.FileName + ".json", json);
        Debug.Log("Saved to: " + Application.persistentDataPath + "/" + saveFile.FileName + ".json");
    }
}
