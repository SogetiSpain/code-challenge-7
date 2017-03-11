using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public IEnumerable<Note> GetNotes()
        {
            throw new NotImplementedException();
        }
    }
}
