using System;
using mynotes.test.mocks;
using NUnit.Framework;

namespace mynotes.test {
	[TestFixture]
	public class DateUtilsTest {
		[Test]
		public void PadWithZeroTest() {
			Assert.AreEqual("00", DateUtilsWrapper.wrappedPadWithZero(0));
			Assert.AreEqual("02", DateUtilsWrapper.wrappedPadWithZero(2));
			Assert.AreEqual("11", DateUtilsWrapper.wrappedPadWithZero(11));
			Assert.AreEqual("111", DateUtilsWrapper.wrappedPadWithZero(111));
		}
		
		[Test]
		public void FormattedCreationDateTest() {
			DateTime dt = DateTime.Parse("2008-05-01T07:34:42-5:00");
			Assert.AreEqual("01-05-2008", DateUtilsWrapper.wrappedFormattedCreationDate(dt));
			Assert.AreEqual("01#05#2008", DateUtilsWrapper.wrappedFormattedCreationDate(dt, "#"));
		}
	}
}
