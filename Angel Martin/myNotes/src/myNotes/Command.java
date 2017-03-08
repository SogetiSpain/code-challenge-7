package myNotes;


public enum Command {
	NEW("new"),
	SHOW("show"),
	DELETE("delete"),
	CONNECTOR("connector"),
	HELP("help"),
	EXIT("exit"),
	UNKNOWN("unkown");
	
	private String command;

	static Command find(String command){
	    for (Command cmd : Command.values()) {
	        if (command.equalsIgnoreCase(cmd.toString())) {
	            return cmd;
	        }
	    }
	    return UNKNOWN;
	}
	
	Command(String command){
		this.command = command;
	}
	
	@Override
	public String toString() {
		return this.command;
	}
	
}
