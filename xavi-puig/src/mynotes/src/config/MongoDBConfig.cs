using System;

namespace mynotes.config {
	
	public class MongoDBConfig : Config{
		
		public string hostName {get; set;}
		public string port {get; set;}
		public string database {get; set;}
		
		public MongoDBConfig(string hostNameAndPort, string database) {
			string[] splitted = hostNameAndPort.Split(':');
			this.hostName = splitted[0];
			this.port = splitted[1];
			this.database = database;
		}
		
		public MongoDBConfig(string hostName, string port, string database) {
			this.hostName = hostName;
			this.port = port;
			this.database = database;
		}
	}
}
