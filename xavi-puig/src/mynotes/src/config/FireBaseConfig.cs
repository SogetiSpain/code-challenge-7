using System;
using mynotes.config;

namespace mynotes {
	public class FireBaseConfig : Config {
		
		public string baseUrl {get; set;}
		public string apiKey {get; set;}
				
		public FireBaseConfig(string baseURL, string apiKey) {
			this.baseUrl = formatUrl(baseURL);
			this.apiKey = apiKey;
		}
	}
}
