using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using MyNotes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Client
{
    public class DocumentDbClient<T> : IClient<T> where T : Item
    {
        private readonly string _resourceUrl;
        private readonly string _securityToken;
        private readonly HttpClient _client;

        public DocumentDbClient(string resourceUrl, string securityToken)
        {
            _resourceUrl = resourceUrl;
            _securityToken = securityToken;

            //Implement a connection via sdk to azure document db.
        }

        public async Task<T> AddAsync(string id,T value)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(string id, T value)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByKey(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAll(string text, string[] tags)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAll(string text)
        {
            throw new NotImplementedException();
        }
    }
}
