using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Data
{
    public interface INotesRepository
    {
        IEnumerable<Note> GetNotes();
        void AddNote(string text);
    }
}
