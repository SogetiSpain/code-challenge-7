using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace mynotes.test {
	[TestFixture]
	public class NoteTest {
		
		[Test]
		public void TestJSONWithoutTags() { 
			Note note = new Note("Text", "05-03-2017", null);
			const string expect = "{\"text\":\"Text\", \"creationDate\": \"05-03-2017\", \"tags\": []}";
			Assert.AreEqual(expect, note.toJSON());
		}
		
		[Test]
		public void TestJSONWithOneTag() { 
			Note note = new Note("Text", "05-03-2017", new List<string> {"TAG"});
			const string expect = "{\"text\":\"Text\", \"creationDate\": \"05-03-2017\", \"tags\": [\"TAG\"]}";
			Assert.AreEqual(expect, note.toJSON());
		}
		
		[Test]
		public void TestJSONWithMoreThanOneTag() { 
			Note note = new Note("Text", "05-03-2017", new List<string> {"TAG1", "TAG2", "TAG3"});
			const string expect = "{\"text\":\"Text\", \"creationDate\": \"05-03-2017\", \"tags\": [\"TAG1\", \"TAG2\", \"TAG3\"]}";
			Assert.AreEqual(expect, note.toJSON());
		}
	}
}
