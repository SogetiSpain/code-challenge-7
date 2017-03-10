package myNotes;

import java.util.List;
import java.util.Scanner;

public class MyNotes {
	
	private static boolean running = true;
	private static boolean hideTags = false;
	private static boolean deleteAll = false;
	
	
	public static void main(String[] args) throws InterruptedException {
		
		CommandInterpreter interpreter = new CommandInterpreter();
		DatabaseManager conn = new DatabaseManager(new FirebaseClientConnector());
		List<Note> notes = null;
		Scanner sc = new Scanner(System.in);
		boolean correctParsing = false;
		
		System.out.println(":::::::::::::::::::::           ");
		System.out.println(":::::::Welcome to mynotes::     ");
		System.out.println("::::::::::::::::::::::::::::::::");


		System.out.println("");
		System.out.println("_|      _|                  _|      _|                _|                                ");
		System.out.println("_|_|  _|_|   _|    _|       _|_|    _|     _|_|     _|_|_|_|     _|_|       _|_|_|      ");
		System.out.println("_|  _|  _|   _|    _|       _|  _|  _|   _|    _|     _|       _|_|_|_|   _|_|          ");
		System.out.println("_|      _|   _|    _|       _|    _|_|   _|    _|     _|       _|             _|_|      ");
		System.out.println("_|      _|     _|_|_|       _|      _|     _|_|         _|_|     _|_|_|   _|_|_|        ");
		System.out.println("                   _|                                                                   ");
		System.out.println("                 _|_|                                                                   "); 
		System.out.println("::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
		System.out.println("      type: <mynotes help> for a list of options");
		System.out.println(" ");
		
		
		
		while (running) {
			correctParsing = interpreter.parse(sc.nextLine());
			
			if (correctParsing) {
				switch (interpreter.getCommand()) {
					case NEW:
						Note newNote = new Note(interpreter.getData());
						applyArguments(interpreter, newNote);
						conn.addNote(newNote);
						break;
					case SHOW:
						notes = conn.getNotes(interpreter.getData());
						applyArguments(interpreter);
						if (notes != null && !notes.isEmpty()) {
							displayNotes(notes);
						}
						break;
					case DELETE:
						applyArguments(interpreter);
						conn.deleteNotes(interpreter.getData(), deleteAll);
						break;
					case CONNECTOR:
						conn.changeConnector(interpreter.getData());
						break;
					case HELP:
						displayHelp();
						break;
					case EXIT:
						running = false;
						break;
					case UNKNOWN:
						break;
				}
				restoreArgumentsVariables();
			}
			
			System.out.println(" ");
		}
		
		sc.close();
		System.out.println("::::::::::::::::::::::::::::::::");
		System.out.println(":::::::::See you soon!!::::     ");
		System.out.println(":::::::::::::::::::::           ");
		System.exit(0);
		
	}
	
	private static void displayNotes(List<Note> notes) {
		for (Note i : notes) {
			if (hideTags)
				System.out.println(">>>: " + i.getContent());
			else {
				System.out.println(">>>: " + i.getContent() + "   " + i.formatTags());
			}
				
		}
	}
	
	private static void applyArguments(CommandInterpreter interpreter, Note note) {
		for (String i : interpreter.getArgumentsAsList()) {
			String arg = "";
			String argData = "";
			
			if (!i.trim().equals("") && i.trim().split(" ").length == 2) {
				arg = i.trim().split(" ")[0];
				argData = i.trim().split(" ")[1];
			} else if (!i.trim().equals("") && i.trim().split(" ").length == 1) {
				arg = i.trim();
			}
			
			switch (interpreter.getCommand()) {
				case NEW:
					computeNewArguments(arg, argData, note);
					break;
				case SHOW:
					computeShowArguments(arg);
					break;
				case DELETE:
					computeDeleteArguments(arg);
					break;
			}
		}
	}
	
	private static void applyArguments(CommandInterpreter interpreter) {
		applyArguments(interpreter, null);
	}
	
	private static void computeNewArguments(String arg, String argData, Note note) {
		switch (arg) {
			case "t":
				note.addTag(argData);
				break;
				
		}
	}
	
	private static void computeShowArguments(String arg) {
		switch (arg) {
			case "h":
				hideTags = true;
				break;
		}
	}
	
	private static void computeDeleteArguments(String arg) {
		switch (arg) {
			case "a":
				deleteAll = true;
				break;		
		}
	}
	
	private static void restoreArgumentsVariables() {
		hideTags = false;
		deleteAll = false;
	}
	
	
	public static void displayHelp() {
		System.out.println(" ");
		System.out.println("...............................................................................");
		System.out.println("...............................................................................");
		System.out.println("                                                                               ");
		System.out.println("           ---  mynotes [command] [option] [data]  ---                         ");
		System.out.println("                                                                               ");
		System.out.println("    COMAND:                    OPTION:     EXPLANATION:                        ");
		System.out.println("      new       [new note]                   add a new note to de DB           ");
		System.out.println("                                 -t          add a new tag for each <-t>       ");
		System.out.println("                                                                               ");
		System.out.println("      show      <text to seek>               show notes based on a pattern     ");
		System.out.println("                                 -h          hide tags when showing notes      ");
		System.out.println("                                                                               ");
		System.out.println("      delete    [text to seek]               delete notes based on a pattern   ");
		System.out.println("                                 -a          delete all notes                  ");
		System.out.println("                                                                               ");
		System.out.println("      connector [connector]                  change to one of the followings:  ");
		System.out.println("                                             <fbRest>                          ");
		System.out.println("                                             <fbClient>                        ");
		System.out.println("                                             <mongoRest>                       ");
		System.out.println("                                                                               ");
		System.out.println("      help                                   show possible actions             ");
		System.out.println("                                                                               ");
		System.out.println("      exit                                   terminates the program            ");
		System.out.println("                                                                               ");
		System.out.println("...............................................................................");
		System.out.println(" ");
		System.out.println(" ");
	}

}








