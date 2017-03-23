using MyNotes.Client;
using MyNotes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.App
{
    public interface ICollectionService<T> where T : Item
    {
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task RemoveAsync(string id);
        Task<T> GetByKeyAsync(string id);
        Task<T> GetAllAsync(string text);
    }
}
