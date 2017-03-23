using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using mynotes.database;
using Newtonsoft.Json;

namespace mynotes {

	class FireBase : AbstractDataBase {
		
		FireBaseConfig config;
		
		public FireBase(FireBaseConfig config) {
			this.config = config;
		}

		string doSave(string url, string method, Note note) {			
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = method;
			
			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
				streamWriter.Write(note.toJSON());
				streamWriter.Flush();
				streamWriter.Close();
			}
			
			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();			
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
				var result = streamReader.ReadToEnd();
				
				if (method == "POST") {
					try {
						Dictionary<string, string> responseNotes = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
						return responseNotes["name"];
					}
					catch {
						return "ko";
					}
				}
			}
			return "ok";
		}
		
		public override string save(Note note) {
			string url = config.baseUrl + "notes.json?auth=" + config.apiKey;
			return doSave(url, "POST", note);
		}
		
		public override Note[] all() {
			WebClient webClient = new WebClient();
			string json = webClient.DownloadString(config.baseUrl + "notes.json?auth=" + config.apiKey);
		    
			Dictionary<string, Note> responseNotes = JsonConvert.DeserializeObject<Dictionary<string, Note>>(json);
						
			int i = -1;
		    List<Note> notes = new List<Note>();
		    if (responseNotes != null) {
			    foreach(KeyValuePair<string, Note> entry in responseNotes) {
			    	Note n = entry.Value;		    	
			    	notes.Add(new Note(n.text, n.creationDate, n.tags, entry.Key, ++i));
				}
		    }
		    return notes.ToArray();
		}
		
		public override Note[] findByDate(string date) {
			Note[] allNotes = all();
			List<Note> matches = new List<Note>();
			foreach (Note n in allNotes) {
				if (n.creationDate == date)
					matches.Add(n);			
			}	
			return matches.ToArray();
		}
		
		public override Note[] findByText(string text) {
			Note[] allNotes = all();
			List<Note> matches = new List<Note>();
			foreach (Note n in allNotes) {
				if (n.text.Contains(text))
					matches.Add(n);			
			}
			
			return matches.ToArray();
		}
		
		string doDelete(string url) {
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "DELETE";
			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			return httpResponse.StatusCode.ToString().ToLower();
			
		}
		
		public override string delete(int index) {
			Note[] allNotes = all();
			if (allNotes.Length <= index) return "Index is too large.";
			string url = config.baseUrl + "notes/" + allNotes[index].Id +".json?auth=" + config.apiKey;
			return doDelete(url);
		}
		
		public override string deleteAll() {
			string url = config.baseUrl + "notes.json?auth=" + config.apiKey;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "DELETE";
			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			return httpResponse.StatusCode.ToString().ToLower();
		}
		
		string canTag(Note[] notes, int index, string tag) {
			if (index >= notes.Length) return "Index is too large.";
			if (notes[index].tags.Contains(tag)) return "The note already contains that tag.";
			return "ok";
		}
		
		public override string tag(int index, string tag) {
			Note[] notes = all();
			string possibleToTag = canTag(notes, index, tag);
			if (possibleToTag != "ok") return possibleToTag;
			Note note = notes[index];				
			note.tags.Add(tag);
			
			return doSave(config.baseUrl + "notes/" + note.Id + ".json?auth=" + config.apiKey, "PATCH", note);
		}
		
		string canUntag(Note[] notes, int index, string tag) {
			if (index >= notes.Length) return "Index is too large.";
			if (!notes[index].tags.Contains(tag)) return "The note does not contain that tag.";
			return "ok";
		}

		public override string untag(int index, string tag)	{
			Note[] notes = all();
			string possibleToUntag = canUntag(notes, index, tag);
			if (possibleToUntag != "ok") return possibleToUntag;
			Note note = notes[index];				
			note.tags.Remove(tag);
			
			return doSave(config.baseUrl + "notes/" + note.Id + ".json?auth=" + config.apiKey, "PATCH", note);
		}

		public override Note[] findByTag(string text) {
			Note[] allNotes = all();
			List<Note> matches = new List<Note>();
			foreach (Note n in allNotes) {
				if (n.tags.Contains(text))
					matches.Add(n);
			}
			return matches.ToArray();
		}

		public override string edit(int index, string newText) {
			Note[] allNotes = all();
			if (index >= allNotes.Length) return "Index is too large.";
			Note note = allNotes[index];
			note.text = newText;
			return doSave(config.baseUrl + "notes/" + note.Id + ".json?auth=" + config.apiKey, "PATCH", note);
		}

		public override string xtag(int index, string oldTag, string newTag) {
			Note[] notes = all();
			string possibleToTag = canTag(notes, index, newTag);
			if (possibleToTag != "ok") return possibleToTag;
			string possibleToUntag = canUntag(notes, index, oldTag);
			if (possibleToUntag != "ok") return possibleToUntag;
			string tagRemoved = untag(index, oldTag);
			if (tagRemoved != "ok") return tagRemoved;
			string tagAdded = tag(index, newTag);
			if (tagAdded != "ok") return tagAdded;
			return "ok";
		}
	}
}

