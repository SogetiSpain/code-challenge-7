using System;

namespace mynotes{
	public class DateUtils {
		
		public static string getCurrentTime() {
			return formattedCreationDate(DateTime.Now);
		}
		
		protected static string padWithZero(int value) {
			return value.ToString().PadLeft(2, '0');
		}
		
		protected static string formattedCreationDate(DateTime creationDate, String separator = "-") {
			return padWithZero(creationDate.Day) + separator + padWithZero(creationDate.Month) + separator + creationDate.Year;
		}
		
	}
}
