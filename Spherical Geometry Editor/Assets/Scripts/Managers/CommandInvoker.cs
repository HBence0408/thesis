using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker 
{
    private Stack<ICommand> undoStack = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
    }

    public void Undo()
    {
        ICommand command = undoStack.Pop();
        command.UnExecute();
    }
}
