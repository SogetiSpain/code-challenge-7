using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using mynotes.lines;

namespace mynotes {
	
	public class Lines {
		public List<Line> lines = new List<Line>();
				
		public void add(string text, ConsoleColor color = ConsoleColor.Gray) {
			add(new Line(text, color));
		}
		
		public void add(Line line) {
			lines.Add(line);
		}
		
		public override bool Equals(Object obj) {      
	      	if (obj == null || GetType() != obj.GetType()) 
	        	return false;
	
	      	Lines l = (Lines)obj;
			return this.lines.SequenceEqual(l.lines);
	    }
		
		public override int GetHashCode() {
			return ((IStructuralEquatable)this.lines).GetHashCode(EqualityComparer<int>.Default);
		}
	}
}
