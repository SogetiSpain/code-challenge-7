package myNotes;

import java.util.List;

public abstract class Connector {
	
	protected Note note;
	
	
	public abstract boolean addNote(Note note);

	public abstract List<Note> getNotes(String toSeek);
	
	public abstract List<Note> getNotes();

	public abstract void deleteNotes(String toSeek, boolean deleteAll);
	
}
