using UnityEngine;

public interface ICommand
{
    public void Execute();
    public void UnExecute();
    public void ReExecute();
    public void Delete();
}
