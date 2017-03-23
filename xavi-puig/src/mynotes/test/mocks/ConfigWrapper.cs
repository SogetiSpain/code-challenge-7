using System;
using mynotes.config;

namespace mynotes.test.mocks {
	
	public class ConfigWrapper : Config {
		
		public string wrappedFormatUrl(string url) {
			return formatUrl(url);
		}
	}
}
