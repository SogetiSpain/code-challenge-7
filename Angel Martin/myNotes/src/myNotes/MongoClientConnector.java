package myNotes;

import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import org.bson.Document;
import org.bson.conversions.Bson;
import org.bson.types.ObjectId;

import com.mongodb.Block;
import com.mongodb.MongoClient;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import com.mongodb.client.model.Filters;

public class MongoClientConnector extends MongoConnector {
	
	private MongoClient mongoClient;
	private MongoDatabase database;
	private MongoCollection<Document> collection;
	
	
	public MongoClientConnector() {
		super();
		mongoClient = new MongoClient(address, port);
		database = mongoClient.getDatabase(databaseName);
		collection = database.getCollection("data");
	}
	
	
	@Override
	public boolean addNote(Note note) {
		boolean saved = false;
		
		Document document = new Document("tags", note.getTags())
	               .append("content", note.getContent());
		
		try {
			collection.insertOne(document);
			saved = true;
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return saved;
	}

	@Override
	public synchronized List<Note> getNotes(String toSeek) {
		List<Note> notes = new ArrayList<>();
		Block<Document> composeNotes = new Block<Document>() {
		       @Override
		       public synchronized void apply(final Document document) {
		    	   Note auxNote = new Note();
		    	   auxNote.setId(document.get("_id", ObjectId.class).toHexString());
		           auxNote.setContent(document.getString("content"));
		           auxNote.setTags(document.get("tags", ArrayList.class));
		           if ((auxNote.getContent().toLowerCase().contains(toSeek) || auxNote.formatTagsAsString().toLowerCase().contains(toSeek))) {
		        	   notes.add(auxNote);
		           }
		       }
		};
		
		collection.find().forEach(composeNotes);
		
		return notes;
	}

	@Override
	public List<Note> getNotes() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public void deleteNotes(String toSeek, boolean deleteAll) {
		boolean somethingDeleted = false;
		
		if (!"".equals(toSeek) || deleteAll) {
			List<Note> notes = getNotes("");
			
			for (Note i : notes) {
				if ((i.getContent().toLowerCase().contains(toSeek) || i.formatTagsAsString().toLowerCase().contains(toSeek)) && i.getId() != "-1") {
					try {
						collection.deleteOne(Filters.eq("_id", new ObjectId(i.getId())));
						somethingDeleted = true;
						if (!deleteAll) {
							System.out.println(">>>: deleted: " + i.getContent());
						}
					} catch (Exception e) {
						System.out.println(">>>: there was some problem deleting your note, please check your network connectivity.     |:|  =D~~~~~");
						e.printStackTrace();
					}
				}
			}
			
			
			displayDeleteMessage(deleteAll, somethingDeleted); //TODO
			
		}
	}

}
