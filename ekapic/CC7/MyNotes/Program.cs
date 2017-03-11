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

        static void Main(string[] args)
        {
            InitializeDependencies();

            // https://code-challenge-7.firebaseio.com/notes.json?auth=7gx7McBjKkw1lUgyhfcmjl4pJ2bUsMjfx8Sxwdtz
            /*
              mynotes new Aprender a crear árboles binarios invertidos
                Your note was saved.

                mynotes show
                20-02-2017 - Aprender a crear árboles binarios invertidos.
                19-02-2017 - Esto de tomar notas con un programa de consola mola. 
            */

        }



        private static void InitializeDependencies()
        {
            _repository = new FirebaseNotesRepository();
        }

        private static void CheckParameters(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("Please use the 'show' or 'new' command.");
            }
        }
    }
}
