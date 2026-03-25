using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Repository : IRepository
{
    private Dictionary<Guid, IGeometryObject> entites = new Dictionary<Guid, IGeometryObject>();

    public void Delete(Guid id)
    {
        entites.Remove(id);
    }

    public IGeometryObject GetById(Guid id)
    {
        return entites[id];
    }

    public List<T> GetByType<T>()
    {
        return entites.Values.OfType<T>().ToList();
    }

    public void Store(IGeometryObject geometryObject)
    {
        entites.Add(geometryObject.Id, geometryObject);
        Debug.Log("added " + geometryObject.Id + " to repository");
    }
}
