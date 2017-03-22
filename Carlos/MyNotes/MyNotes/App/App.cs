using MyNotes.App;
using MyNotes.Client;
using MyNotes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.App
{
    public class App
    {
        private readonly IClient<Note> _notes;
        private readonly ICommandService _commandService;
        private readonly ICollectionService<Note> _notesCollection;

        public App()
        {
            //Firebase Info:
            var resourceUrl = "https://mynotes-87fd2.firebaseio.com/";
            var securityToken = "XUCrsUoS3lxsQlYMcsBuIhGI21OH1N5iYM3sJCUD";
            _notes = new FirebaseClient<Note>(resourceUrl,securityToken);

            //If you want to switch to another client you have to set here the 

            _notesCollection = new NoteCollectionService(_notes);
            _commandService = new CommandService(_notesCollection);
        }

        public void Start()
        {
            Console.WriteLine("Welcome to MyNotes");
            while (true)
            {
                Console.WriteLine("Avaible commands: notes push text | notes remove id | notes update id text | notes get text | notes get id | exit");
                var command = Console.ReadLine();
                _commandService.HandleCommand(command);
            }            
        }
    }
}
