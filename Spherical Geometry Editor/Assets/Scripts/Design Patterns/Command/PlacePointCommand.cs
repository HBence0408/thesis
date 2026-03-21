using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlacePointCommand : ICommand
{
    private Vector3 pos;
    private ControllPoint pointScript;
    private GameObject prefab;
    private Action<GameObject> select;

    public PlacePointCommand(Vector3 pos, GameObject prefab , Action<GameObject> select)
    {
        this.pos = pos.normalized;
        this.select = select;
        this.prefab = prefab;
        Debug.Log(prefab);
    }

    public PlacePointCommand(Vector3 pos, GameObject prefab)
    {
        this.pos = pos.normalized;
        this.prefab = prefab;
    }

    public void Execute()
    {
        Debug.Log(prefab);
        GameObject point = MonoBehaviour.Instantiate(prefab);
        select?.Invoke(point);
        pointScript = point.GetComponent<ControllPoint>();
        pointScript.transform.position = pos;
    }

    public void UnExecute()
    {
        pointScript.Destroy();
    }
}
