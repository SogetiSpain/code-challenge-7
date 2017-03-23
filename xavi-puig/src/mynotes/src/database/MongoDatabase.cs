using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using mynotes.config;

namespace mynotes.database {
	
	public class MongoDatabase : AbstractDataBase {
		
		MongoDBConfig config;
		MongoClient client;
		IMongoDatabase database;
		readonly IMongoCollection<Note> notesCollection;
		
		public MongoDatabase(MongoDBConfig config) {
			this.config = config;
			this.client = new MongoClient("mongodb://" + config.hostName + ":" + config.port);
			this.database = client.GetDatabase(config.database);
			this.notesCollection = database.GetCollection<Note>("notes");
		}

		public override string save(Note note) {
			try {
				notesCollection.InsertOne(note);
				return "ok";
			}
			catch {
				return "ko";
			}			
		}

		public override Note[] all() {
			List<Note> notes = new List<Note>();
			int i = -1;
			var filter = new BsonDocument();
			using (var cursor = notesCollection.Find(filter).ToCursor()) {
			    while (cursor.MoveNext()) {
			        foreach (var doc in cursor.Current) {
						doc.index = ++i;
			            notes.Add(doc);
			        }
			    }
			}
			return notes.ToArray();
		}

		public override Note[] findByDate(string date) {
			List<Note> notes = new List<Note>();
			int i = -1;
			var filter = new BsonDocument("creationDate", date);
			using (var cursor = notesCollection.Find(filter).ToCursor()) {
				while (cursor.MoveNext()) {
			        foreach (var doc in cursor.Current) {
						doc.index = ++i;
			            notes.Add(doc);
			        }
			    }
			}
			return notes.ToArray();			
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

		public override string delete(int index) {
			Note[] allNotes = all();
			if (allNotes.Length <= index) return "Index is too large.";
			Note toDelete = allNotes[index];
			try {
				var a = notesCollection.DeleteOne(new BsonDocument("_id", toDelete.Id));
				return "ok";
			} catch {
				return "ko";
			}
		}

		public override string deleteAll() {
			try {
				notesCollection.DeleteMany(new BsonDocument());	
				return "ok";
			} catch {
				return "ko";
			}
			
		}

		string canTag(Note[] notes, int index, string tagToAdd) {
			if (index >= notes.Length) return "Index is too large.";
			if (notes[index].tags.Contains(tagToAdd)) return "The note already contains that tag.";
			return "ok";
		}
		
		public override string tag(int index, string tag) {
			Note[] notes = all();
			string possibleToTag = canTag(notes, index, tag);
			if (possibleToTag != "ok") return possibleToTag;
			Note note = notes[index];
			note.tags.Add(tag);
			try {
				notesCollection.ReplaceOne(new BsonDocument("_id", note.Id), note);	
				return "ok";
			} catch {
				return "ko";
			}
		}

		string canUntag(Note[] notes, int index, string tagToRemove) {
			if (index >= notes.Length) return "Index is too large.";
			if (!notes[index].tags.Contains(tagToRemove)) return "The note does not contain that tag.";
			return "ok";
		}
		
		public override string untag(int index, string tag)	{
			Note[] notes = all();	
			string possibleToUntag = canUntag(notes, index, tag);
			if (possibleToUntag != "ok") return possibleToUntag;
			Note note = notes[index];
			note.tags.Remove(tag);
			try {
				notesCollection.ReplaceOne(new BsonDocument("_id", note.Id), note);	
				return "ok";
			} catch {
				return "ko";
			}
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
			Note[] notes = all();
			if (index >= notes.Length) return "Index is too large.";
			Note note = notes[index];
			note.text = newText;
			try {
				notesCollection.ReplaceOne(new BsonDocument("_id", note.Id), note);	
				return "ok";
			} catch {
				return "ko";
			}
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
