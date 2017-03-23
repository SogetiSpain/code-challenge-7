using System;
using mynotes.config;

namespace mynotes.database {
	public abstract class AbstractDataBase : IDatabase {
		
		protected AbstractDataBase() {}

		abstract public string save(Note note);

		abstract public Note[] all();
		
		abstract public Note[] findByDate(string date);
		
		abstract public Note[] findByText(string text);
		
		abstract public Note[] findByTag(string text);
		
		abstract public string delete(int index);
		
		abstract public string deleteAll();
		
		abstract public string edit(int index, string newText);
		
		abstract public string tag(int index, string tag);
		
		abstract public string untag(int index, string tag);
		
		abstract public string xtag(int index, string oldTag, string newTag);
	}
}
