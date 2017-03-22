using MyNotes.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json.Linq;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;
using MyNotes.App;

namespace MyNotes
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test().Wait();
            new App.App().Start();
        }

        static async Task Test()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "XUCrsUoS3lxsQlYMcsBuIhGI21OH1N5iYM3sJCUD",
                BasePath = "https://mynotes-87fd2.firebaseio.com/"
            };

            IFirebaseClient client = new FirebaseClient(config);
            var testNote = new Note()
            {
                Id = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                Tags = new List<string>() { "gulp", "front", "sass", "minify" },
                Text = "Set up gulp task in javascript projects to improve quality in front end architechture"
            };

            await client.PushAsync<Note>($"notes/{testNote.Id}", testNote);
            
        }

    }
}
