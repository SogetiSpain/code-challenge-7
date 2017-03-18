using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.Configuration;

namespace MyNotes.Data
{
    public class DocumentDbNotesRepository: INotesRepository
    {
        private const string EndpointUriSetting= "DocumentDBEndpointUri";
        private const string DbPrimaryKeySetting = "DocumentDBPrimaryKey";
        private DocumentClient client;

        public DocumentDbNotesRepository()
        {
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings[EndpointUriSetting]))
            {
                throw new ArgumentNullException("Missing DocumentDBEndpointUri");
            }
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings[DbPrimaryKeySetting]))
            {
                throw new ArgumentNullException("Missing DocumentDBPrimaryKey");
            }
            var endpointUrl = ConfigurationManager.AppSettings[EndpointUriSetting];
            var primaryKey = ConfigurationManager.AppSettings[DbPrimaryKeySetting];
            this.client = new DocumentClient(new Uri(endpointUrl), primaryKey);

        }

        public IEnumerable<Note> GetNotes()
        {
            var uri = UriFactory.CreateDocumentCollectionUri("cc7", "notes");
            return this.client.CreateDocumentQuery<Note>(uri).ToList();
        }

        public void AddNote(string text)
        {
            var newNote = new Note();
            newNote.Date = DateTime.Now;
            newNote.Text = text;

            var uri = UriFactory.CreateDocumentCollectionUri("cc7", "notes");
            this.client.CreateDocumentAsync(uri, newNote).Wait();            
        }
    }
}
