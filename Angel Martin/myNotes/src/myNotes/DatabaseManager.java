package myNotes;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

public class DatabaseManager {
	
	private Connector connector;
	
	
	public DatabaseManager(Connector connector) {
		super();
		this.connector = connector;
	}
	
	
	public boolean addNote(Note note) {
		boolean result;
		Date date = new Date();
		SimpleDateFormat df = new SimpleDateFormat("dd-MM-yyyy");
		
		if ("".equals(note.getContent())) {
			System.out.println(">>>: Unable to save an empty note...");
			result = false;
		} else {
			note.setDate(df.format(date));
			result = displaySaveResult(connector.addNote(note));
		}
		
		return result;
	}

	public List<Note> getNotes(String toSeek) {
		return displayGetResult(connector.getNotes(toSeek));
	}
	
	public List<Note> getNotes() {
		return connector.getNotes();
	}

	public void deleteNotes(String toSeek, boolean deleteAll) {
		if ("".equals(toSeek) && !deleteAll) {
			System.out.println(">>>: No text to look for...");
			System.out.println(">>>: Do you want to delete all notes? if so, use: mynotes delete -a");
		} else {
			connector.deleteNotes(toSeek, deleteAll);
		}
	}


	public void changeConnector(String conn) {
		switch(conn.toLowerCase()) {
			case "fbrest":
				this.connector = new FirebaseRESTConnector();
				System.out.println(">>>: Using Firebase REST API connection...");
				break;
			case "fbclient":
				this.connector = new FirebaseClientConnector();
				System.out.println(">>>: Using Firebase Client API connection...");
				System.out.println(">>>: Waiting for authentication, please wait...");
				break;
			case "mongoclient":
				this.connector = new MongoClientConnector();
				System.out.println(">>>: Using MongoDB Client API connection...");
				break;
			default:
				System.out.println(">>>: Unkown connector please select one of the following: ");
				System.out.println(">>>:   fbRest        <-------   Firebase REST API");
				System.out.println(">>>:   fbClient      <-------   Firebase Client API");
				System.out.println(">>>:   mongoClient   <-------   MongoDB Client API");
		}
	}
	
	private boolean displaySaveResult(boolean saved) {
		if (saved)
			System.out.println(">>>: ...saved...");
		else{
			System.out.println(">>>: there was some problem saving your note, please check your network connectivity.     |:|  =D~~~~~");
		}
		
		return saved;
	}
	
	private List<Note> displayGetResult(List<Note> notes) {
		if (notes.isEmpty()) {
			Note auxNote = new Note("Sorry, the are currently no notes at the Data Base... :(");
			auxNote.setId("-1");
			notes.add(auxNote);
		}
		
		return notes;
	}
	
}
