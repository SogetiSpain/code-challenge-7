using System;
using mynotes.config;
using mynotes.database;

namespace mynotes {
	class Program {
		
		public static void Main(string[] args) {
			Config config = readConfig();
			IDatabase database = null;
			try {
				database = DataBaseFactory.getDatabase(config);
			}
			catch {
				XConsole.line("The configuration readed from config.txt is wrong,\nplease review it and try again.", ConsoleColor.Red);	
			}
			if (database != null) {
				XConsole.writeLines(new MyNotes(database, args).execute());
			}
		}
		
		public static Config readConfig() {
			Config config = Config.read();
			if (config != null) return config;
			XConsole.line("config.txt was not found or doesn't have the correct format.", ConsoleColor.Red);
			XConsole.line("Do you want to create a new configuration file? [y/n]", ConsoleColor.Yellow);
			if (Console.ReadLine() == "y") {
				newConfigFile();
				return readConfig();
			}
			else {
				Environment.Exit(-1);
				return null;
			}
		}
		
		public static void newConfigFile() {
			string type = getType();
			string result;
			if (type == "mongo") result = createMongoConfig(type);
			else result = createFirebaseConfig(type);
			
			if (result == "ok") {
				XConsole.line("Configuration saved! Retrying operation...", ConsoleColor.Green);
			}
			else {
				XConsole.line(result, ConsoleColor.Red);
			}
		}
		
		static string getType() {
			XConsole.line();
			XConsole.line("- Select the number of the option you want to use");
			XConsole.line("\t1 - Firebase (REST)");
			XConsole.line("\t2 - Firebase (Fire#)");
			XConsole.line("\t3 - MongoDB");
			return readType();
		}
		
		static string readType() {
			switch (Console.ReadLine()) {
				case "1": 
					return "firebase";
				case "2":
					return "thirdparty";
				case "3":
					return "mongo";
				default : 
					return null;
			}
		}
		
		static string createFirebaseConfig(string type) {
			XConsole.line();
			XConsole.line("- Enter the Firebase URL");
			string url = Console.ReadLine();
			XConsole.line();
			XConsole.line("- Enter the API auth key");
			string api = Console.ReadLine();
			XConsole.line();
			return Config.saveFirebase(type, url, api);
		}
		
		static string createMongoConfig(string type) {
			XConsole.line();
			XConsole.line("- Enter the hostname and the port (format hostName:port)");
			string hostNameAndPort = Console.ReadLine();
			XConsole.line();
			XConsole.line("- Enter the database name");
			string database = Console.ReadLine();
			XConsole.line();
			return Config.saveMongo(type, hostNameAndPort, database);
		}
	}
}