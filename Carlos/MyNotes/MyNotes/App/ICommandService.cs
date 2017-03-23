using MyNotes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.App
{
    public interface ICommandService
    {
        Task HandleCommand(string command);
    }
}
