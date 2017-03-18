using System;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace mynotes.config {
	public class ThirdPartyConfig : Config {
		
		public IFirebaseConfig config;
		
		public ThirdPartyConfig(string baseURL, string apiKey) {
			config = new FirebaseConfig{
				AuthSecret = apiKey,
				BasePath = formatUrl(baseURL)
			};
		}
	}
}
