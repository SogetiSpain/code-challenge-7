using MyNotes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes
{
    public class Operations
    {
        private readonly INotesRepository _repository;
        public Operations(INotesRepository repository)
        {
            this._repository = repository;
        }

        public void AddNote(string note)
        {
            if(String.IsNullOrEmpty(note))
            {
                throw new ArgumentNullException("Empty note text");
            }
            this._repository.AddNote(note);
            Console.WriteLine("Your note was saved.");
        }

        public int ShowNotes()
        {
            var notes = this._repository.GetNotes();
            foreach(var note in notes)
            {
                Console.WriteLine($"{note.Date.ToShortDateString()} - {note.Text}.");
            }
            return notes.Count();
        }
    }
}
