using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker 
{
   // private Stack<ICommand> undoStack = new Stack<ICommand>();

    private LinkedList<ICommand> commandHistory = new LinkedList<ICommand>();
    int maxHistorySize = 10;
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandHistory.AddFirst(command);

        for (int i = 0; i < redoStack.Count; i++)
        {
            ICommand redoCommand = redoStack.Pop();
            redoCommand.Delete();
        }

        if (commandHistory.Count > maxHistorySize )
        {
            command = commandHistory.Last.Value;
            commandHistory.RemoveLast();
            command.Delete();
        }
    }

    public void Undo()
    {
        ICommand command = commandHistory.First?.Value;
        if (command == null)
        {
            return;
        }
        else
        {
            command.UnExecute();
            redoStack.Push(command);
            commandHistory.RemoveFirst();
        }
    }

    public void Redo()
    {
        ICommand command = redoStack.Pop();
        if (command == null)
        {
            return;
        }
        else
        {
            command.ReExecute();
            commandHistory.AddFirst(command);
        }
    }
}
