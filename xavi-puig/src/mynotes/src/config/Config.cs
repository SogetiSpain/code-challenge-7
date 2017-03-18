using System;
using System.IO;

namespace mynotes.config {
	public abstract class Config {
	
		public static string[] get() {
			return File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\config.txt");
		}
		
		public static Config read() {
			try {
				string[] parameters = get();
				
				switch (parameters[0]) {
					case "firebase":
						return new FireBaseConfig(parameters[1], parameters[2]);
					case "thirdparty":
						return new ThirdPartyConfig(parameters[1], parameters[2]);
					case "mongo":						
						return new MongoDBConfig(parameters[1], parameters[2]);
					default:
						return null;
				}
				
			} catch {
				return null;
			}
		}
		
		public static string saveFirebase(string type, string url, string api) {
			try {
				StreamWriter file = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + @"\config.txt");
				file.WriteLine(type.Trim());
				file.WriteLine(url.Trim());
				file.WriteLine(api.Trim());
				file.Flush();
				file.Close();
				return "ok";
			} catch (Exception e) {
				return e.Message;				
			}			
		}
		
		public static string saveMongo(string type, string hostAndPort, string database) {
			try {
				StreamWriter file = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + @"\config.txt");
				file.WriteLine(type.Trim());
				file.WriteLine(hostAndPort.Trim());
				file.WriteLine(database.Trim());
				file.Flush();
				file.Close();
				return "ok";
			} catch (Exception e) {
				return e.Message;				
			}			
		}
		
		public static string delete() {
			try {
				File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\config.txt");
				return "ok";
			} catch (Exception e) {
				return e.Message;
			}
		}
		
		protected string formatUrl(string url) {
			url = url.Trim();
			if (!url.StartsWith(@"https://")) url = @"https://" + url;
			if (!url.EndsWith(@"/")) url = url + @"/";
			return url;
		}
	}
}
