using System;
using mynotes.config;

namespace mynotes.database {

	public static class DataBaseFactory {
		
		public static IDatabase getDatabase(Config config) {
			if (config is FireBaseConfig)
				return new FireBase((FireBaseConfig)config);
			if (config is ThirdPartyConfig)
				return new FireBaseThirdParty((ThirdPartyConfig)config);
			if (config is MongoDBConfig)
				return new MongoDatabase((MongoDBConfig)config);
			return null;
		}
	}
}
