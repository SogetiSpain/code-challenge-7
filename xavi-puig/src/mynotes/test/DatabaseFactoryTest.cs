using mynotes.config;
using mynotes.database;

using System;
using mynotes.test.mocks;
using NUnit.Framework;

namespace mynotes.test {
	[TestFixture]
	public class DatabaseFactoryTest {
		[Test]
		public void ReturnsFirebaseInstanceIfFirebaseConfig() {
			FireBaseConfig fbc = new FireBaseConfig("aaaa", "aaaa");
			Assert.IsInstanceOf<FireBase>(DataBaseFactory.getDatabase(fbc));
		}
		
		[Test]
		public void ReturnsThirdPartyInstanceIfThirdPartyConfig() {
			ThirdPartyConfig fbtp = new ThirdPartyConfig("aaaa", "aaaa");
			Assert.IsInstanceOf<FireBaseThirdParty>(DataBaseFactory.getDatabase(fbtp));
		}
		
		[Test]
		public void ReturnsMongoDBInstanceIfMongoDBConfig() {
			MongoDBConfig mdb = new MongoDBConfig("aaaa:2121", "aaaa");
			Assert.IsInstanceOf<MongoDatabase>(DataBaseFactory.getDatabase(mdb));
		}
		
		[Test]
		public void ReturnsNullWithMockConfig() {
			MockConfig mc = new MockConfig();
			Assert.IsNull(DataBaseFactory.getDatabase(mc));
		}
	}
}
