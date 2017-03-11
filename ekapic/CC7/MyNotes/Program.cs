using MyNotes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes
{
    class Program
    {
        private static INotesRepository _repository;
        private static Operations _ops;

        static void Main(string[] args)
        {
            InitializeDependencies();
            var command = CheckParameters(args);

            if(command != null)
            {
                switch(command)
                {
                    case "show":
                        _ops.ShowNotes();
                        break;
                    case "add":
                        var text = CombineAddArgumentsIntoString(args);
                        _ops.AddNote(text);
                        break;
                }
                Console.ReadLine();
            }
        }



        private static void InitializeDependencies()
        {
            _repository = new FirebaseNotesRepository();
            _ops = new Operations(_repository);
        }

        private static string CheckParameters(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Please use the 'show' or 'new' command.");
                return null;
            }

            var arg1 = args[0];
            if(arg1.ToLower() == "show")
            {
                return "show";
            }
            if(arg1.ToLower() == "add")
            {
                if (args.Length >= 2)
                {
                    return "add";
                }
            }
            return null;    
        }

        private static string CombineAddArgumentsIntoString(string[] args)
        {
            var result = String.Join(" ", args.Skip(1).ToArray());
            return result;
        }
    }
}
