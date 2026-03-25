using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

public interface IRepository
{
    public IGeometryObject GetById(Guid id);
    public void Delete(Guid id);
    public void Store(IGeometryObject geometryObject);
    public List<T> GetByType<T>();
}
