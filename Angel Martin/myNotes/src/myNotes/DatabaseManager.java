package myNotes;

import java.util.List;

public class DatabaseManager {
	
	private Connector connector;
	
	
	public DatabaseManager(Connector connector) {
		super();
		this.connector = connector;
	}
	
	
	public boolean addNote(Note note) {
		return connector.addNote(note);
	}

	public List<Note> getNotes(String toSeek) {
		return connector.getNotes(toSeek);
	}
	
	public List<Note> getNotes() {
		return connector.getNotes();
	}

	public void deleteNotes(String toSeek, boolean deleteAll) {
		connector.deleteNotes(toSeek, deleteAll);
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
				this.connector = new MongoConnector();
				System.out.println(">>>: Using MongoDB RESP API connection...");
				break;
		}
	}
	
	
}
