using MyNotes.Client;
using MyNotes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.App
{
    // This service is a little coupled to Notes, if you want to extend the application to support more collections and commands 
    // can be done easily implementing a little more of abstraction here ;)
    public class CommandService : ICommandService
    {
        private readonly Dictionary<string, Func<string, Task>> _commands = new Dictionary<string, Func<string, Task>>();
        private readonly ICollectionService<Note> _notesCollection;


        public CommandService(ICollectionService<Note> notesCollection)
        {
            _notesCollection = notesCollection;

            _commands.Add("notes push", Push);
            _commands.Add("notes remove", Remove);
            _commands.Add("notes update", Update);
            _commands.Add("notes get", GetById);
            _commands.Add("notes list", List);

            _commands.Add("exit", Exit);
        }
    

        public async Task HandleCommand(string command)
        {
            await _commands.Where(x => command.Contains(x.Key)).First().Value(command);
        }

        private async Task Exit(string command)
        {
            Console.WriteLine("¿Exit Sure? Y/N");
            var confirm = Console.ReadLine();
            if (confirm == "Y")
                await Task.Run(() => Environment.Exit(0));
        }

        private async Task Push(string command)
        {
            await _notesCollection.AddAsync(new Model.Note() { Id = Guid.NewGuid().ToString(), Date = DateTime.Now, Text = command.Replace("notes push","") });
        }

        private async Task Remove(string command)
        {
            await _notesCollection.RemoveAsync(command.Replace("notes remove", ""));
        }

        private async Task Update(string command)
        {
            var id = command.Replace("notes remove", "").Split(' ')[0];
            var text = command.Replace("notes remove", "").Split(' ')[1];
            var note = new Note()
            {
                Id = id,
                Date = DateTime.Now,
                Text = text
            };
             
            await _notesCollection.UpdateAsync(note);
        }

        private async Task GetById(string command)
        {
            await _notesCollection.GetByKeyAsync(command.Replace("notes get", ""));
        }

        private async Task List(string command)
        {
            await _notesCollection.GetAllAsync(command.Replace("notes list", ""));
        }

    }
}
