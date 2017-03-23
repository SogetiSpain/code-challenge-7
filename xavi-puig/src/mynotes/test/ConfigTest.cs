using System;
using mynotes.config;
using mynotes.test.mocks;
using NUnit.Framework;

namespace mynotes.test {
	[TestFixture]
	public class ConfigTests {
		
		[Test]
		public void FireBaseConfigTest() {
			FireBaseConfig fbc = new FireBaseConfig("aaaaa", "bbbbb");
			Assert.AreEqual(@"https://aaaaa/", fbc.baseUrl);
			Assert.AreEqual("bbbbb", fbc.apiKey);
		}
		
		[Test]
		public void ThirdPartyConfigTest() {
			ThirdPartyConfig tpc = new ThirdPartyConfig("aaaaa", "bbbbb");
			Assert.AreEqual("https://aaaaa/", tpc.config.BasePath);
			Assert.AreEqual("bbbbb", tpc.config.AuthSecret);
		}
		
		[Test]
		public void MongoDBConfigTest1() {
			MongoDBConfig mdb = new MongoDBConfig("aaaaa:2121", "bbbbb");
			Assert.AreEqual("aaaaa", mdb.hostName);
			Assert.AreEqual("2121", mdb.port);
			Assert.AreEqual("bbbbb", mdb.database);
		}
		
		[Test]
		public void MongoDBConfigTest2() {
			MongoDBConfig mdb = new MongoDBConfig("aaaaa", "2121", "bbbbb");
			Assert.AreEqual("aaaaa", mdb.hostName);
			Assert.AreEqual("2121", mdb.port);
			Assert.AreEqual("bbbbb", mdb.database);
		}
		
		[Test]
		public void FirebaseConfigFormatsURLcorrectly() {
			ConfigWrapper cw = new ConfigWrapper();
			Assert.AreEqual(@"https://aaaaaaaa/", cw.wrappedFormatUrl(@"https://aaaaaaaa/"));
			Assert.AreEqual(@"https://aaaaaaaa/", cw.wrappedFormatUrl(@"https://aaaaaaaa"));
			Assert.AreEqual(@"https://aaaaaaaa/", cw.wrappedFormatUrl(@"aaaaaaaa"));
		}
	}
}
