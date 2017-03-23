using System;
using mynotes.lines;
using mynotes.test.mocks;
using NUnit.Framework;

namespace mynotes.test {
	[TestFixture]
	public class MyNotesTest {
		
		Line incorrectParameterEnd() {
			Line line = new Line("Type '");
			line.add("mynotes help", ConsoleColor.Red);
			line.add("' to see the available commands and their format.");
			return line;
		}
		
		[Test]
		public void NewNoteCreatedOkTest() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"new", "ok"}).execute();
			Lines expected = new Lines();
			expected.add("Your note was saved.", ConsoleColor.Green);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void NewNoteFailTest() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"new", "ko"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error saving the note.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void CantCreateEmptyNote() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"new"}).execute();
			Lines expected = new Lines();
			expected.add("You can't create an empty note.", ConsoleColor.Yellow);
			expected.add(incorrectParameterEnd());
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void ShowAllNotes() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"show"}).execute();
			Lines expected = new Lines();
			expected.add("0: 20-20-2020 - aaa");
			expected.add("1: 21-21-2121 - bbb [TAG]");
			expected.add("2: 22-22-2222 - ccc [TAG1 TAG2]");
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void ShowOneNote() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"show", "2"}).execute();
			Lines expected = new Lines();
			expected.add("2: 22-22-2222 - ccc [TAG1 TAG2]");
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void ShowOneNoteWithWrongIndex() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"show", "99"}).execute();
			Lines expected = new Lines();
			expected.add("Wrong index '99' specified.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void ShowWithoutNotes() {
			Lines lines = new MyNotes(new EmptyDataBaseMock(), new [] {"show"}).execute();
			Lines expected = new Lines();
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void SearchWithMissingParameters() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"search"}).execute();
			Lines expected = new Lines();
			expected.add("You need to specify a search mode and a search criteria.", ConsoleColor.Yellow);
			expected.add(incorrectParameterEnd());
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void SearchWithWrongModeFails() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"search", "blah", "blah"}).execute();
			Lines expected = new Lines();
			expected.add("Unknown search mode.", ConsoleColor.Yellow);
			expected.add(incorrectParameterEnd());
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void SearchByDate() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"search", "date", "21-21-2121"}).execute();
			Lines expected = new Lines();
			expected.add("3: 21-21-2121 - bbb [TAG]");
			expected.add("8: 21-21-2121 - ccc [TAG1 TAG2]");
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void SearchByText() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"search", "text", "aaaa"}).execute();
			Lines expected = new Lines();
			Line line1 = new Line("3: 21-21-2121 - ");
			line1.add("aaaa", ConsoleColor.Yellow);
			line1.add(" [TAG]");
			line1.add("\n");
			Line line2 = new Line("8: 21-21-2121 - ");
			line2.add("aaaa", ConsoleColor.Yellow);
			line2.add(" bbbb ");
			line2.add("aaaa", ConsoleColor.Yellow);
			line2.add(" [TAG1 TAG2]");
			line2.add("\n");
			expected.add(line1);
			expected.add(line2);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void SearchByTag() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"search", "tag", "TAG"}).execute();
			Lines expected = new Lines();
			Line line1 = new Line("3: 21-21-2121 - aaaa [");
			line1.add("TAG", ConsoleColor.Yellow);
			line1.add("]");
			line1.add("\n");
			Line line2 = new Line("8: 21-21-2121 - aaaa bbbb aaaa [");
			line2.add("TAG", ConsoleColor.Yellow);
			line2.add("TAG2");
			line2.add("]");
			line2.add("\n");
			expected.add(line1);
			expected.add(line2);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void TagFailsWithoutIndex() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"tag"}).execute();
			Lines expected = new Lines();
			expected.add("You need to specify the index of a note and the tag to add.", ConsoleColor.Yellow);
			expected.add(incorrectParameterEnd());
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void TagCorrect() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"tag", "0", "ok"}).execute();
			Lines expected = new Lines();
			expected.add("The tag was added.", ConsoleColor.Green);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void TagIncorrect() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"tag", "0", "ko"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: ko", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void TagAlreadyExistingTagFails() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"tag", "0", "contains"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: The note already contains that tag.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void TagWithIncorrectIndexFails() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"tag", "0", "index"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: Index is too large.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void UntagFailsWithoutIndex() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"untag"}).execute();
			Lines expected = new Lines();
			expected.add("You need to specify the index of a note and the tag to remove.", ConsoleColor.Yellow);
			expected.add(incorrectParameterEnd());
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void UntagCorrect() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"untag", "0", "ok"}).execute();
			Lines expected = new Lines();
			expected.add("The tag was removed.", ConsoleColor.Green);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void UntagIncorrect() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"untag", "0", "ko"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: ko", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void UntagNonExistingTagFails() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"untag", "0", "contains"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: The note does not contain that tag.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void UntagWithIncorrectIndexFails() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"untag", "0", "index"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: Index is too large.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		
		[Test]
		public void XtagCorrect() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"xtag", "0", "A", "ok"}).execute();
			Lines expected = new Lines();
			expected.add("The tag was changed.", ConsoleColor.Green);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void XtagIncorrect() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"xtag", "0", "A", "ko"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: ko", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void XtagUntagNonExistingTagFails() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"xtag", "0", "A", "untag"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: The note does not contain that tag.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void XtagTagAlreadyExistingTagFails() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"xtag", "0", "A", "tag"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: The note already contains that tag.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void XtagWithIncorrectIndexFails() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"xtag", "0", "A", "index"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: Index is too large.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		
		[Test]
		public void DeleteOneOk() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"delete", "0"}).execute();
			Lines expected = new Lines();
			expected.add("The note was deleted.", ConsoleColor.Green);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void DeleteOneWithIncorrectIndex() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"delete", "1"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error while deleting the note: Index is too large.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void DeleteOneWithGenericError() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"delete", "2"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error while deleting the note.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void DeleteAllCorrect() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"delete", "all"}).execute();
			Lines expected = new Lines();
			expected.add("All notes were deleted.", ConsoleColor.Green);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void DeleteWithIncorrectParameters() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"delete"}).execute();
			Lines expected = new Lines();
			expected.add("You need to specify 'all' or an index", ConsoleColor.Yellow);
			expected.add(incorrectParameterEnd());
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void DeleteAllIncorrect() {
			Lines lines = new MyNotes(new EmptyDataBaseMock(), new [] {"delete", "all"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error while deleting the notes.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void editWithIncorrectParameters() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"edit", "0"}).execute();
			Lines expected = new Lines();
			expected.add("You need to specify the index of the note to edit and the new text.", ConsoleColor.Yellow);
			expected.add(incorrectParameterEnd());
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void editWithWrongParameterIndex() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"edit", "A", "index"}).execute();
			Lines expected = new Lines();
			expected.add("Wrong index specified.", ConsoleColor.Yellow);
			expected.add(incorrectParameterEnd());
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void editWithLargeIndex() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"edit", "99", "index"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note: Index is too large.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void cantEditWithEmptyText() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"edit", "0", ""}).execute();
			Lines expected = new Lines();
			expected.add("You can't set an empty text.", ConsoleColor.Yellow);
			expected.add(incorrectParameterEnd());
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void editOk() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"edit", "0", "ok"}).execute();
			Lines expected = new Lines();
			expected.add("Your note was updated.", ConsoleColor.Green);
			Assert.AreEqual(expected, lines);
		}
		
		[Test]
		public void editKo() {
			Lines lines = new MyNotes(new DataBaseMock(), new [] {"edit", "0", "ko"}).execute();
			Lines expected = new Lines();
			expected.add("There was an error updating the note.", ConsoleColor.Red);
			Assert.AreEqual(expected, lines);
		}
	}
}
