using MyNotes.Client;
using MyNotes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.App
{
    public class NoteCollectionService : ICollectionService<Note>
    {
        private readonly string _collectionId = "notes/";
        private readonly IClient<Note> _client;

        public NoteCollectionService(IClient<Note> client)
        {
            _client = client;
        }

        public async Task<Note> AddAsync(Note note)
        {
            return await _client.AddAsync($"{_collectionId}{note.Id}", note);
        }

        public async Task<Note> GetAllAsync(string text)
        {
            return await _client.GetAll(text, null);
        }

        public async Task<Note> GetByKeyAsync(string id)
        {
            return await _client.GetByKey(id);
        }

        public async Task<Note> UpdateAsync(Note value)
        {
            return await _client.UpdateAsync(value.Id, value);
        }
        public async Task RemoveAsync(string id)
        {
            await _client.Remove(id);
        }
    }
}
