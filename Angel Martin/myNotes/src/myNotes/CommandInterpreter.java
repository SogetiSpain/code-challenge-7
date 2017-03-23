package myNotes;

import java.util.ArrayList;
import java.util.List;

public class CommandInterpreter {
	
	private String base;
	private String command;
	private String data;
	private String arguments;
	private String singleArguments  = "-h -a";
	
	
	
	public boolean parse(String rawArguments) {
		String[] auxArgs = rawArguments.split(" ");
		List<String> args = new ArrayList<>();
		base = "";
		command = "";
		data = "";
		arguments = "";
		boolean correctParsing = true;
		boolean isArgumentData = false;
		
		for (String i : auxArgs) {
			if (i.matches("-[a-z]")) {
				arguments = arguments + i + " ";
				if (!singleArguments.contains(i))
					isArgumentData = true;
			} else if (!isArgumentData) {
				args.add(i);
			} else {
				arguments = arguments + i + " ";
				isArgumentData = false;
			}
		}
		arguments = arguments.trim();
		
		if (args.size() == 1) {
			command = args.get(0);
		} else {
			base = args.get(0);
			command = args.get(1);
		}
		
		
		for (int i=2; i<args.size(); i++) {
			data = data + args.get(i) + " ";
		}
		data = data.trim();
		
		if (!base.equals(Command.EXIT.toString())) {
			if (!isValidBase()) {
				System.out.println(":::[ERROR] Unkown " + base + ":::");
				correctParsing = false;
			} else if (!isValidCommand()) {
				System.out.println(":::[ERROR] Unkown command " + command + ":::");
				correctParsing = false;
			}
		}
		
		return correctParsing;
	}
	
	
	private boolean isValidBase() {
		String validBase = "mynotes";
		
		if (validBase.contains(base))
			return true;
		
		return false;
	}
	
	private boolean isValidCommand() {
		if (!Command.find(command).equals(Command.UNKNOWN))
			return true;
		
		return false;
	}
	
	
	public String getBase() {
		return base;
	}
	
	public Command getCommand() {
		return Command.find(command);
	}
	
	public String getData() {
		return data;
	}
	
	public String getArguments() {
		return arguments;
	}
	
	public String[] getArgumentsAsList() {
		return arguments.trim().split("-");
	}
	
}








