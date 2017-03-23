using System;
using System.Collections.Generic;
using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Response;
using mynotes.config;
using Newtonsoft.Json;

namespace mynotes.database {
	
	public class FireBaseThirdParty : AbstractDataBase {
		
		IFirebaseConfig config;
		
		public FireBaseThirdParty(ThirdPartyConfig thirdPartyConfig) {
			config = thirdPartyConfig.config;
		}

		override
		public string save(Note note) {
			IFirebaseClient  client = new FirebaseClient(config);
			try {
				PushResponse response = client.Push("notes", note);
				return response.Result.name;
			} catch {
				return "ko";
			}
		}

		override
		public Note[] all() {
			IFirebaseClient  client = new FirebaseClient(config);
			FirebaseResponse response = client.Get("notes");
			Dictionary<string, Note> responseNotes = JsonConvert.DeserializeObject<Dictionary<string, Note>>(response.Body);
			if (responseNotes == null)  return new Note[0];
			
			int i = -1;
		    List<Note> notes = new List<Note>();
		    foreach(KeyValuePair<string, Note> entry in responseNotes) {
		    	Note n = entry.Value;		    	
		    	notes.Add(new Note(n.text, n.creationDate, n.tags, entry.Key, ++i));
			}
		   
		    return notes.ToArray();
		}

		override
		public Note[] findByDate(string date) {
			Note[] allNotes = all();
			List<Note> matches = new List<Note>();
			foreach (Note n in allNotes) {
				if (n.creationDate == date)
					matches.Add(n);			
			}
			return matches.ToArray();
		}
		
		override
		public Note[] findByText(string text) {
			Note[] allNotes = all();
			List<Note> matches = new List<Note>();
			foreach (Note n in allNotes) {
				if (n.text.Contains(text))
					matches.Add(n);			
			}
			return matches.ToArray();
		}

		public override string delete(int index) {
			Note[] allNotes = all();
			if (allNotes.Length <= index) return "Index is too large.";
			IFirebaseClient  client = new FirebaseClient(config);
			var response = client.Delete("notes/" + allNotes[index].Id);
			return response.StatusCode.ToString().ToLower();
		}
		
		public override string deleteAll() {
			IFirebaseClient  client = new FirebaseClient(config);
			var response = client.Delete("notes");
			return response.StatusCode.ToString().ToLower();
		}

		string doUpdate(Note note) {
			IFirebaseClient client = new FirebaseClient(config);
			var response = client.Update("notes/" + note.Id, note);
			return response.StatusCode.ToString().ToLower();
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
			return doUpdate(note);
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
			return doUpdate(note);
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
			return doUpdate(note);
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
