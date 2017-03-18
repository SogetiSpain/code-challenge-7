using System;
using mynotes.database;

namespace mynotes.test.mocks {
	
	public class EmptyDataBaseMock : AbstractDataBase {
		
		public EmptyDataBaseMock() {}

		public override string save(Note note) {
			throw new NotImplementedException();
		}

		public override Note[] all() {
			return new Note[0];
		}

		public override Note[] findByDate(string date) {
			return new Note[0];
		}

		public override Note[] findByText(string text) {
			return new Note[0];
		}

		public override string delete(int index) {
			throw new NotImplementedException();
		}
		
		public override string deleteAll() {
			return "ko";
		}

		public override string tag(int index, string tag) {
			throw new NotImplementedException();
		}

		public override string untag(int index, string tag) {
			throw new NotImplementedException();
		}

		public override Note[] findByTag(string text) {
			throw new NotImplementedException();
		}

		public override string edit(int index, string newText) {
			throw new NotImplementedException();
		}

		public override string xtag(int index, string oldTag, string newTag) {
			throw new NotImplementedException();
		}
	}
}
