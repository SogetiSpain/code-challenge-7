using MyNotes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Client
{
    public interface IClient<T> where T : Item
    {
        Task<T> AddAsync(string id, T value);
        Task<T> UpdateAsync(string id, T value);
        Task Remove(string id);
        Task<T> GetByKey(string id);
        Task<T> GetAll(string text, string[] tags);
        Task<T> GetAll(string text);
    }
}
