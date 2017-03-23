using System;
using System.Collections.Generic;
using mynotes.database;

namespace mynotes.test.mocks {
	
	public class DataBaseMock : AbstractDataBase {
		
		public DataBaseMock() {}

		public override string save(Note note) {
			if (note.text == "ok") return "1a2d4f5g";
			return "ko";
		}

		public override Note[] all() {
			return new [] {
				new Note("aaa", "20-20-2020", null, "1", 0),
				new Note("bbb", "21-21-2121", new List<String> {"TAG"}, "2", 1),
				new Note("ccc", "22-22-2222", new List<String> {"TAG1", "TAG2"}, "3", 2),
			};
		}

		public override Note[] findByDate(string date) {
			return new [] {
				new Note("bbb", "21-21-2121", new List<String> {"TAG"}, "2", 3), 
				new Note("ccc", "21-21-2121", new List<String> {"TAG1", "TAG2"}, "3", 8)
			};
		}

		public override Note[] findByText(string text) {
			return new [] {
				new Note("aaaa", "21-21-2121", new List<String> {"TAG"}, "2", 3), 
				new Note("aaaa bbbb aaaa", "21-21-2121", new List<String> {"TAG1", "TAG2"}, "3", 8)
			};
		}
		
		public override Note[] findByTag(string text) {
			return new [] {
				new Note("aaaa", "21-21-2121", new List<String> {"TAG"}, "2", 3), 
				new Note("aaaa bbbb aaaa", "21-21-2121", new List<String> {"TAG", "TAG2"}, "3", 8)
			};
		}

		public override string delete(int index) {
			if (index == 0) return "ok";
			if (index == 1) return "Index is too large.";
			return "ko";
		}
		
		public override string deleteAll() {
			return "ok";
		}

		public override string tag(int index, string tag) {
			if (tag == "ok") return "ok";
			if (tag == "index") return "Index is too large.";
			if (tag == "contains") return "The note already contains that tag.";
			return "ko";
		}

		public override string untag(int index, string tag) {
			if (tag == "ok") return "ok";
			if (tag == "index") return "Index is too large.";
			if (tag == "contains") return "The note does not contain that tag.";
			return "ko";
		}

		public override string edit(int index, string newText) {
			if (newText == "ok") return "ok";
			if (index == 99) return "Index is too large.";
			return "ko";
		}

		public override string xtag(int index, string oldTag, string newTag) {
			if (newTag == "ok") return "ok";
			if (newTag == "index") return "Index is too large.";
			if (newTag == "untag") return "The note does not contain that tag.";
			if (newTag == "tag") return "The note already contains that tag.";
			return "ko";
		}
	}
}
