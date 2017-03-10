package myNotes;

import java.util.List;

public class DatabaseManager {
	
	private Connector connector;
	
	
	public DatabaseManager(Connector connector) {
		super();
		this.connector = connector;
	}
	
	
	public boolean addNote(Note note) {
		return displaySaveResult(connector.addNote(note));
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
			case "mongorest":
				this.connector = new MongoRESTConnector();
				System.out.println(">>>: Using MongoDB RESP API connection...");
				break;
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
