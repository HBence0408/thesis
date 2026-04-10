using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public interface ICommandInvoker
    {
    public void ExecuteCommand(ICommand command);
    public void Undo();
    public void Redo();
    }

