using System;
using mynotes.lines;
using NUnit.Framework;

namespace mynotes.test {
	[TestFixture]
	public class OutputTest {
		
		#region Part
		
		[Test]
		public void PartWithoutColorTest() {
			Part part = new Part("aaaa");
			Assert.AreEqual("aaaa", part.text);
			Assert.AreEqual(ConsoleColor.Gray, part.color);
		}
		
		[Test]
		public void PartWithColorTest() {
			Part part = new Part("bbbb", ConsoleColor.Red);
			Assert.AreEqual("bbbb", part.text);
			Assert.AreEqual(ConsoleColor.Red, part.color);
		}
		
		#endregion
		
		#region Line
		
		[Test]
		public void SinglePartLineWithoutColorTest() {
			Line line = new Line("aa bb cc");
			Assert.AreEqual(1, line.parts.Count);
			Assert.AreEqual("aa bb cc", line.parts[0].text);
			Assert.AreEqual(ConsoleColor.Gray, line.parts[0].color);
		}
		
		[Test]
		public void SinglePartLineWithColorTest() {
			Line line = new Line("cc bb aa", ConsoleColor.Red);
			Assert.AreEqual(1, line.parts.Count);
			Assert.AreEqual("cc bb aa", line.parts[0].text);
			Assert.AreEqual(ConsoleColor.Red, line.parts[0].color);
		}
		
		[Test]
		public void MultiPartStringLineTest() {
			Line line = new Line();			
			Assert.AreEqual(0, line.parts.Count);
			line.add("aaaa");
			Assert.AreEqual(1, line.parts.Count);
			Assert.AreEqual("aaaa", line.parts[0].text);
			Assert.AreEqual(ConsoleColor.Gray, line.parts[0].color);
			line.add("cccc", ConsoleColor.Blue);
			Assert.AreEqual(2, line.parts.Count);
			Assert.AreEqual("cccc", line.parts[1].text);
			Assert.AreEqual(ConsoleColor.Blue, line.parts[1].color);			
		}
		
		[Test]
		public void MultiPartLineTest() {
			Line line = new Line("aaaaa", ConsoleColor.Green);
			Assert.AreEqual(1, line.parts.Count);
			Assert.AreEqual("aaaaa", line.parts[0].text);
			Assert.AreEqual(ConsoleColor.Green, line.parts[0].color);
			line.add(new Part("bbbbb"));
			Assert.AreEqual(2, line.parts.Count);
			Assert.AreEqual("bbbbb", line.parts[1].text);
			Assert.AreEqual(ConsoleColor.Gray, line.parts[1].color);
			line.add(new Part("ccccc", ConsoleColor.Yellow));
			Assert.AreEqual(3, line.parts.Count);
			Assert.AreEqual("ccccc", line.parts[2].text);
			Assert.AreEqual(ConsoleColor.Yellow, line.parts[2].color);
		}
		
		[Test]
		public void MultiPartMixedtLineTest() {
			Line line = new Line("aaaaa", ConsoleColor.Green);
			Assert.AreEqual(1, line.parts.Count);
			Assert.AreEqual("aaaaa", line.parts[0].text);
			Assert.AreEqual(ConsoleColor.Green, line.parts[0].color);
			line.add(new Part("ccccc", ConsoleColor.Yellow));
			Assert.AreEqual(2, line.parts.Count);
			Assert.AreEqual("ccccc", line.parts[1].text);
			Assert.AreEqual(ConsoleColor.Yellow, line.parts[1].color);
			line.add("bbbbb", ConsoleColor.Gray);
			Assert.AreEqual(3, line.parts.Count);
			Assert.AreEqual("bbbbb", line.parts[2].text);
			Assert.AreEqual(ConsoleColor.Gray, line.parts[2].color);
		}
		
		#endregion
		
		#region Lines
		
		[Test]
		public void SingleLineStringTest() {
			Lines lines = new Lines();
			lines.add("aaaaa", ConsoleColor.Red);
			Assert.AreEqual(1, lines.lines.Count);
			Assert.AreEqual(1, lines.lines[0].parts.Count);
			Assert.AreEqual("aaaaa", lines.lines[0].parts[0].text);
			Assert.AreEqual(ConsoleColor.Red, lines.lines[0].parts[0].color);
			
		}
		
		[Test]
		public void SingleLineTest() {
			Lines lines = new Lines();
			lines.add(new Line("aaaaa"));
			Assert.AreEqual(1, lines.lines.Count);
			Assert.AreEqual(1, lines.lines[0].parts.Count);
			Assert.AreEqual("aaaaa", lines.lines[0].parts[0].text);
			Assert.AreEqual(ConsoleColor.Gray, lines.lines[0].parts[0].color);
		}
		
		[Test]
		public void SingleLineMultiPartTest() {
			Lines lines = new Lines();
			Line line = new Line("aaaa");
			line.add("bbbb", ConsoleColor.Red);
			lines.add(line);
			Assert.AreEqual(1, lines.lines.Count);
			Assert.AreEqual(2, lines.lines[0].parts.Count);
			Assert.AreEqual("aaaa", lines.lines[0].parts[0].text);
			Assert.AreEqual(ConsoleColor.Gray, lines.lines[0].parts[0].color);
			Assert.AreEqual("bbbb", lines.lines[0].parts[1].text);
			Assert.AreEqual(ConsoleColor.Red, lines.lines[0].parts[1].color);
		}
		
		[Test]
		public void MultiLineTest() {
			Lines lines = new Lines();
			Line line = new Line("aaaa");
			line.add("bbbb", ConsoleColor.Red);
			lines.add(line);
			lines.add("ccccc", ConsoleColor.Green);
			lines.add("dddddd");
			Assert.AreEqual(3, lines.lines.Count);
			Assert.AreEqual(2, lines.lines[0].parts.Count);
			Assert.AreEqual("aaaa", lines.lines[0].parts[0].text);
			Assert.AreEqual(ConsoleColor.Gray, lines.lines[0].parts[0].color);
			Assert.AreEqual("bbbb", lines.lines[0].parts[1].text);
			Assert.AreEqual(ConsoleColor.Red, lines.lines[0].parts[1].color);
			
			Assert.AreEqual(1, lines.lines[1].parts.Count);
			Assert.AreEqual("ccccc", lines.lines[1].parts[0].text);
			Assert.AreEqual(ConsoleColor.Green, lines.lines[1].parts[0].color);
			Assert.AreEqual("dddddd", lines.lines[2].parts[0].text);
			Assert.AreEqual(ConsoleColor.Gray, lines.lines[2].parts[0].color);
		}
		
		#endregion
	}
}
