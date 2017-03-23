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
    public class FirebaseClient<T> : IClient<T> where T : Item
    {
        private readonly string _resourceUrl;
        private readonly string _securityToken;
        private readonly IFirebaseClient _client;

        public FirebaseClient(string resourceUrl, string securityToken)
        {
            _resourceUrl = resourceUrl;
            _securityToken = securityToken;

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = securityToken,
                BasePath = resourceUrl
            };

            _client = new FirebaseClient(config);
        }

        public async Task<T> AddAsync(string id,T value)
        {
            var response = await _client.PushAsync<T>(id, value);
            return response.ResultAs<T>();
        }

        public async Task<T> UpdateAsync(string id, T value)
        {
            var response = await _client.UpdateAsync<T>(id, value);
            return response.ResultAs<T>();
        }

        public async Task Remove(string id)
        {
            await _client.DeleteAsync(id);
        }

        public async Task<T> GetByKey(string id)
        {
            var response = await _client.GetAsync(id);
            return response.ResultAs<T>();
        }

        public async Task<T> GetAll(string text, string[] tags)
        {
            var response = await _client.GetAsync(text);
            return response.ResultAs<T>();
        }

        public async Task<T> GetAll(string text)
        {
            return await GetAll(text, null);
        }
    }
}
