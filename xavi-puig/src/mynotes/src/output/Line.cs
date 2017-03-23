using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace mynotes.lines {
	public class Line {
		public List<Part> parts = new List<Part>();
		
		public Line() {}
		
		public Line(string text, ConsoleColor color = ConsoleColor.Gray) {
			add(new Part(text, color));
		}
		
		public void add(string text, ConsoleColor color = ConsoleColor.Gray) {
			add(new Part(text, color));
		}
		
		public void add(Part part) {
			this.parts.Add(part);
		}
		
		public override bool Equals(Object obj) {      
	      	if (obj == null || GetType() != obj.GetType()) 
	        	return false;
	
	      	Line l = (Line)obj;
			return this.parts.SequenceEqual(l.parts);
	    }
		
		public override int GetHashCode() {
			return ((IStructuralEquatable)this.parts).GetHashCode(EqualityComparer<int>.Default);
		}
	
	}
}
