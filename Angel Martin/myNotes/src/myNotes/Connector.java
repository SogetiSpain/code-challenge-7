package myNotes;

import java.util.List;

public abstract class Connector {
	
	protected Note note;
	
	
	public abstract boolean addNote(Note note);

	public abstract List<Note> getNotes(String toSeek);
	
	public abstract List<Note> getNotes();

	public abstract void deleteNotes(String toSeek, boolean deleteAll);
	
	protected void displayDeleteMessage(boolean deleteAll, boolean somethingDeleted) {
		if (deleteAll && somethingDeleted) {
			
			
			System.out.println("       _.-^^---....,,--        ");
			System.out.println("   _--                  --_    ");
			System.out.println("  <                        >)  ");
			System.out.println("  |                         |  ");
			System.out.println("   \\._                   _./   ");
			System.out.println("      ```--. . , ; .--'''      ");
			System.out.println("            | |   |            ");
			System.out.println("         .-=||  | |=-.         ");
			System.out.println("         `-=#$%&%$#=-'         ");
			System.out.println("            | ;  :|            ");
			System.out.println("   _____.,-#%&$@%#&#~,._____   ");
		    
		    
		} else if (!somethingDeleted) {
			System.out.println(">>>: There is nothing to delete!");
		}
	}
	
	
}
