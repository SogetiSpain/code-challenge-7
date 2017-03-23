using System;
using System.Text.RegularExpressions;
using mynotes.config;
using mynotes.lines;

namespace mynotes {
	
	public class MyNotes {
		
		IDatabase database;
		string instruction;
		string[] arguments;
		Lines output = new Lines();
		
		public MyNotes(IDatabase database, string[] args) {
			this.database = database;
			
			if (args.Length == 0) this.instruction = "";
			else {
				this.instruction = args[0];
				this.arguments = new string[args.Length -1];
				Array.Copy(args, 1, this.arguments, 0, args.Length -1);
			}
		}
		
		public Lines execute() {
			switch (instruction) {
				case "new":
					newNote(string.Join(" ", arguments));
					break;
				case "show":
					showNotes(arguments);
					break;
				case "search":
					searchNotes(arguments);
					break;
				case "tag":
					tagNote(arguments);
					break;
				case "untag":
					untagNote(arguments);
					break;
				case "delete":
					deleteNotes(arguments);
					break;
				case "config":
					config(arguments);
					break;
				case "xtag":
					xtagNote(arguments);
					break;
				case "edit":
					editNote(arguments);
					break;
				default:
					showHelp();
					break;
			}
			return output;
		}
		
		public void newNote(string text) {
			if (text.Length == 0) incorrectParameters("You can't create an empty note.");
			else {
				if (database.save(new Note(text)) != "ko")
					output.add("Your note was saved.", ConsoleColor.Green);
				else 
					output.add("There was an error saving the note.", ConsoleColor.Red);
			}
		}
		
		public void showNotes(string[] arguments) {
			Note[] notes = database.all();
			if (arguments.Length == 0) {
				foreach (Note note in notes) {
					output.add(note.ToString());
				}
			}
			else {
				int index;
				if (Int32.TryParse(arguments[0], out index) && notes.Length > index)
					output.add(notes[index].ToString());
				else output.add("Wrong index '" + arguments[0] + "' specified.", ConsoleColor.Red);
			}
		}

		public void searchNotes(string[] arguments) {
			if (arguments.Length < 2) incorrectParameters("You need to specify a search mode and a search criteria.");
			else {
				string[] criteriaVector = new string[arguments.Length -1];
				Array.Copy(arguments, 1, criteriaVector, 0, arguments.Length -1);				
				string criteria = string.Join(" ", criteriaVector);
				Note[] notes;
				if (arguments[0] == "date") {
					notes = searchByDate(criteria);
					printMatches(notes, arguments[0], criteria);
				}
				else if (arguments[0] == "text") {
					notes = searchByText(criteria);
					printMatches(notes, arguments[0], criteria);
				}
				else if (arguments[0] == "tag") {
					notes = searchByTag(criteria);
					printMatches(notes, arguments[0], criteria);
				}
				else incorrectParameters("Unknown search mode.");		
			}
		}

		Note[] searchByDate(string criteria) {
			return database.findByDate(criteria);
		}

		Note[] searchByText(string criteria) {
			return database.findByText(criteria);
		}

		Note[] searchByTag(string criteria) {
			return database.findByTag(criteria);
		}
		
		void editNote(string[] args) {
			if (args.Length < 2) {
				incorrectParameters("You need to specify the index of the note to edit and the new text.");
			} else {
				int index;
				Int32.TryParse(args[0], out index);
				if (!Int32.TryParse(args[0], out index)) {
					incorrectParameters("Wrong index specified.");
				} else {
					string[] textArray = new string[args.Length -1];
					Array.Copy(args, 1, textArray, 0, args.Length -1);
					string text = string.Join(" ", textArray);
					if (text.Length == 0) incorrectParameters("You can't set an empty text.");
					else {
						string result = database.edit(index, text);
						if (result == "ok")
							output.add("Your note was updated.", ConsoleColor.Green);
						else if (result != "ko")
							output.add("There was an error updating the note: " + result, ConsoleColor.Red);
						else
							output.add("There was an error updating the note.", ConsoleColor.Red);
					}
				}
			}
		}
		
		void deleteNotes(string[] args) {
			int i;
			if (args.Length == 0) incorrectParameters("You need to specify 'all' or an index");
			else if (Int32.TryParse(args[0], out i)) {
				string result = database.delete(i);
				if (result == "ok") output.add("The note was deleted.", ConsoleColor.Green);
				else output.add("There was an error while deleting the note" + (result != "ko" ? (": " + result) : "."), ConsoleColor.Red);
			}
			else if (args[0] == "all") {
				string result = database.deleteAll();
				if (result == "ok") output.add("All notes were deleted.", ConsoleColor.Green);
				else output.add("There was an error while deleting the notes" + (result != "ko" ? (": " + result) : "."), ConsoleColor.Red);
			}
		}
		
		bool checkParameters(string[] args) {
			int i;
			return args.Length >= 2 && Int32.TryParse(args[0], out i) && string.Join(" ", args, 1, args.Length -1).Trim().Length > 0;
		}
		
		void tagNote(string[] args) {
			if (checkParameters(args)) {
				string result = database.tag(Int32.Parse(args[0]), string.Join(" ", args, 1, args.Length -1).Trim());
				if (result == "ok") output.add("The tag was added.", ConsoleColor.Green);
				else output.add("There was an error updating the note: " + result, ConsoleColor.Red);
		    }
		    else incorrectParameters("You need to specify the index of a note and the tag to add.");
		}
		
		void untagNote(string[] args) {
			if (checkParameters(args)) {
		    	string result = database.untag(Int32.Parse(args[0]), args[1]);
		    	if (result == "ok") output.add("The tag was removed.", ConsoleColor.Green);
				else output.add("There was an error updating the note: " + result, ConsoleColor.Red);
		    }
		    else incorrectParameters("You need to specify the index of a note and the tag to remove.");
		}
	
		bool checkXTagParameters(string[] args) {
			int i;
			return args.Length >= 3 && Int32.TryParse(args[0], out i);
		}
		
		void xtagNote(string[] args) {
			if (checkXTagParameters(args)) {
				string result = database.xtag(Int32.Parse(args[0]), args[1], args[2]);
		    	if (result == "ok") output.add("The tag was changed.", ConsoleColor.Green);
				else output.add("There was an error updating the note: " + result, ConsoleColor.Red);
		    }
		    else incorrectParameters("You need to specify the index of a note the old tag and the new tag.");
		}
		
		void config(string[] args) {
			if (args.Length > 0 && args[0] != "delete") incorrectParameters("Unknown option '" + args[0] + "'");
			else if (args.Length > 0) {
				string result = Config.delete();
				if (result == "ok") output.add("The configuration was removed.", ConsoleColor.Green);
				else output.add("There was an error deleting the configuration: " + result, ConsoleColor.Red);
			}
			else {
				string[] readedConfig = Config.get();
				formatConfig(readedConfig);
			}
		}
		
		void formatConfig(string[] readedConfig) {
			switch (readedConfig[0]) {
				case "mongo":
					output.add("Database: MongoDB");
					output.add("Hostname and port: " + readedConfig[1]);
					output.add("Database name: " + readedConfig[2]);
					break;
				case "firebase":
					output.add("Database: Firebase (using REST)");
					output.add("URL: " + readedConfig[1]);
					output.add("Authentication token: " + readedConfig[2]);
					break;
				case "thirdparty":
					output.add("Database: Firebase (using Fire#)");
					output.add("URL: " + readedConfig[1]);
					output.add("Authentication token: " + readedConfig[2]);
					break;
			}			
		}
		
		void printMatches(Note[] notes, string mode, string criteria) {
			foreach (Note note in notes) {
				if (mode == "text") highLightTextMatches(note, criteria);
				else if (mode == "tag") highLightTagMatches(note, criteria);
				else output.add(note.ToString());
			}
		}
		
		void highLightTagMatches(Note note, string criteria) {
			Line line = new Line(note.index + ": " + note.creationDate + " - " + note.text + " [");
			foreach(string tag in note.tags) {
				if (tag == criteria) line.add(tag, ConsoleColor.Yellow);
				else line.add(tag);
			}
			line.add("]");
			line.add("\n");
			output.add(line);
		}
		
		void highLightTextMatches(Note note, string criteria) {
			Line line = new Line(note.index + ": " + note.creationDate + " - ");
			string[] splits = Regex.Split(note.text, @"("+ criteria +")");
			foreach(string split in splits) {
				if (split.Trim() == criteria) line.add(split, ConsoleColor.Yellow);
				else if (split.Length > 0) line.add(split);
			}
			if (note.tags.Count > 0) line.add(" [" + string.Join(" ", note.tags) + "]");
			line.add("\n");
			output.add(line);
		}
		
		public void showHelp() {
			output.add("Command list: ");
			
			output.add("\tmynotes new <text for the note>", ConsoleColor.DarkYellow);
			output.add("\t\tCreates a new note with the text specified.");
			
			output.add("\tmynotes show [index]", ConsoleColor.DarkYellow);
			output.add("\t\tLists all notes, if index is specified, only the \n\t\tcorresponding note is shown.");
			
			output.add("\tmynotes edit <index> <new text for the note>", ConsoleColor.DarkYellow);
			output.add("\t\tUpdates the text of the note corresponding .to the index");
			
			output.add("\tmynotes search (date | text | tag) <text, tag or date to search for>", ConsoleColor.DarkYellow);
			output.add("\t\tShows the notes that matches by date, tag or text the specified criteria.");
			
			output.add("\tmynotes tag <index> <tag>", ConsoleColor.DarkYellow);
			output.add("\t\tAdds a tag to the note corresponding to the index.");
			
			output.add("\tmynotes untag <index> <tag>", ConsoleColor.DarkYellow);
			output.add("\t\tRemoves a tag from the note corresponding to the index.");
			
			output.add("\tmynotes xtag <index> <old tag> <new tag>", ConsoleColor.DarkYellow);
			output.add("\t\tChanges a tag to another in the note corresponding to the index.");
			
			output.add("\tmynotes delete (<index> | all)", ConsoleColor.DarkYellow);
			output.add("\t\tRemoves a the note corresponding to the index or all.");
			
			output.add("\tmynotes config [delete]", ConsoleColor.DarkYellow);
			output.add("\t\tShows the current configuration. If delete is specified, the configuration is deleted.");
			
			output.add("\tmynotes help", ConsoleColor.DarkYellow);
			output.add("\t\tShows this help.");
		}
		
		public void incorrectParameters(string text) {
			output.add(text, ConsoleColor.Yellow);
			Line line = new Line("Type '");
			line.add("mynotes help", ConsoleColor.Red);
			line.add("' to see the available commands and their format.");
			output.add(line);
		}
	}
}
