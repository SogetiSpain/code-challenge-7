using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Data
{
    public class FirebaseNotesRepository : INotesRepository
    {
        public string _firebaseUrl;
        public string _firebaseAccessToken;

        private const string FirebaseUrlSettingsKey = "FirebaseUrl";
        private const string FirebaseAccessTokenSettingsKey = "FirebaseAccessToken";

        public FirebaseNotesRepository()
        {
            if(String.IsNullOrEmpty(ConfigurationManager.AppSettings[FirebaseUrlSettingsKey]))
            {
                throw new ArgumentNullException("Missing FirebaseUrlSettingsKey");
            }
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings[FirebaseAccessTokenSettingsKey]))
            {
                throw new ArgumentNullException("Missing FirebaseAccessToken");
            }

            this._firebaseUrl = ConfigurationManager.AppSettings[FirebaseUrlSettingsKey];
            this._firebaseAccessToken = ConfigurationManager.AppSettings[FirebaseAccessTokenSettingsKey];
        }

        public void AddNote(string text)
        {
            var requestUrl = $"{_firebaseUrl}/notes.json?auth={_firebaseAccessToken}";

            var newNote = new Note();
            newNote.Date = DateTime.Now;
            newNote.Text = text;

            this.Post(requestUrl, JsonConvert.SerializeObject(newNote));
        }

        public IEnumerable<Note> GetNotes()
        {
            var requestUrl = $"{_firebaseUrl}/notes.json?auth={_firebaseAccessToken}";

            var result = this.Get(requestUrl);

            var notes = JsonConvert.DeserializeObject<List<Note>>(result);
            return notes;
        }

        private string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

        private string Post(string url, string payload)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            try
            {
                using(Stream requestStream = request.GetRequestStream())
                {
                    StreamWriter requestWriter = new StreamWriter(requestStream);
                    requestWriter.Write(payload);
                    requestWriter.Flush();
                }
                
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}
