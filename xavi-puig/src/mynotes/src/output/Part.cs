using System;

namespace mynotes.lines {

	public class Part {
		public string text;
		public ConsoleColor color;
		
		public Part(string text, ConsoleColor color = ConsoleColor.Gray) {
			this.text = text;
			this.color = color;
		}
		
		public override bool Equals(Object obj) {      
			if (obj == null || GetType() != obj.GetType()) 
	 			return false;
			
			Part p = (Part)obj;
			return (this.text == p.text) && (this.color == p.color);
	    }
		
		public override int GetHashCode() {
			return text.Length ^ color.ToString().Length;
		}
	}
}
