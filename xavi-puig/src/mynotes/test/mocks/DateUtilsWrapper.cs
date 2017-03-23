using System;

namespace mynotes.test.mocks {
	public class DateUtilsWrapper : DateUtils {
		public static string wrappedPadWithZero(int value) {
			return padWithZero(value);
		}
		
		public static string wrappedFormattedCreationDate(DateTime creationDate, string separator = "-") {
			return formattedCreationDate(creationDate, separator);
		}
	}
}
