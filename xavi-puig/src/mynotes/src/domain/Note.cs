using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace mynotes{

	public class Note {
		[JsonIgnore]
		[BsonId]
		public string Id {get; set;}
		[JsonIgnore]
		[BsonIgnore]
		public int index {get; set;}
		public string text {get; set;}		
		public string creationDate {get; set;}
		public List<string> tags {get; set;}
		
		public Note() {
			
		}
		
		public Note(string text) {
			this.text = text;
			this.creationDate = DateUtils.getCurrentTime();
			this.tags = new List<string>();
			this.Id = ObjectId.GenerateNewId().ToString();
		}

		public Note(string text, string creationDate, List<string> tags) {
			this.text = text;
			this.creationDate = creationDate;
			this.tags = tags ?? new List<string>();
			this.Id = ObjectId.GenerateNewId().ToString();
		}
		
		public Note(string text, string creationDate, List<string> tags, string id, int index) {
			this.text = text;
			this.creationDate = creationDate;
			this.tags = tags ?? new List<string>();
			this.Id = id;
			this.index = index;
		}
		
		public string toJSON() {
			
			string s = "{\"text\":\""+text+"\", \"creationDate\": \"" + creationDate + "\", \"tags\": [";
			for (int i=0; i<tags.Count; ++i) {				
				s += "\"" + tags[i] + "\"";
				if (i < tags.Count -1) s += ", ";
			}
			return s + "]}";
		}
		
		override
		public string ToString() {
			string s = index + ": " + creationDate + " - " + text;
			if (tags.Count > 0) s += " [" + string.Join(" ", tags) + "]";
			return s;
		}
	}
}
