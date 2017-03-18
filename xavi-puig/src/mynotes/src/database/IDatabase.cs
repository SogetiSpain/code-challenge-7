using System;

namespace mynotes {

	public interface IDatabase 	{
		
		string save(Note note);
		
		Note[] all();
		
		Note[] findByDate(string date);
		
		Note[] findByText(string text);
		
		Note[] findByTag(string text);
		
		string delete(int index);
		
		string deleteAll();
		
		string edit(int index, string newText);
		
		string tag(int index, string tag);
		
		string untag(int index, string tag);
		
		string xtag(int index, string oldTag, string newTag);
	}
}
