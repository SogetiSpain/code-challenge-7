using System;
using System.Collections.Generic;
using mynotes.lines;

namespace mynotes {

	public static class XConsole {
		
		const ConsoleColor defaultColor = ConsoleColor.Gray;
		
		public static void line(string text = "", ConsoleColor color = defaultColor) {
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ForegroundColor = defaultColor;
		}
		
		public static void line(Part part) {
			line(part.text, part.color);
		}
		
		public static void line(List<Part> parts) {
			foreach (Part part in parts) {
				write(part);
			}
			line();
		}
		
		public static void write(string text, ConsoleColor color = defaultColor) {
			Console.ForegroundColor = color;
			Console.Write(text);
			Console.ForegroundColor = defaultColor;
		}
		
		public static void write(Part part) {
			write(part.text, part.color);
		}
		
		public static void writeLines(Lines lines) {
			foreach(Line outputLine in lines.lines) {
				var parts = outputLine.parts;
				if (parts.Count == 1) {
					line(parts[0]);
				}
				else line(parts);
			}
		}
	}
}
