package myNotes;

import java.util.ArrayList;
import java.util.List;

import org.junit.Ignore;

public class Note {
	
	private String id;
	private List<String> tags;
	private String content = "";
	private String date = "";
	
	
	public Note() {
		super();
		// TODO Auto-generated constructor stub
	}

	public Note(String content) {
		super();
		this.content = content;
	}
	
	public Note(String content, String[] tags) {
		super();
		this.content = content;
	}
	
	
	public List<String> getTags() {
		return tags;
	}
	
	public String formatTagsAsString() {
		String stringTags = "";
		
		if (tags != null)
		for (String i : tags) {
			stringTags = stringTags + i;
		}
		
		return stringTags;
	}
	
	public String formatTags() {
		String aux = "";
		
		if (tags != null && !tags.isEmpty()) {
			for (String i : tags) {
				aux =aux + "[" + i + "] ";
			}
			
			return aux.trim();
		}
		return "";
	}
	
	public void setTags(List<String> tag) {
		this.tags = tag;
	}
	
	public void addTag(String tag) {
		if (tags == null)
			tags = new ArrayList<>();
		
		tags.add(tag);
	}
	
	public String getContent() {
		return content;
	}
	
	public void setContent(String content) {
		this.content = content;
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getDate() {
		return date;
	}

	public void setDate(String date) {
		this.date = date;
	}
	
}
