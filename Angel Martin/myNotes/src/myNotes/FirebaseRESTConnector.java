package myNotes;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

import com.google.gson.Gson;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

public class FirebaseRESTConnector extends FirebaseConnector {
	
	public FirebaseRESTConnector() {
		super();
		projectURL = baseURL + urlCollection + dataExtension + authPrefix + authToken;
	}
	
	
	@Override
	public boolean addNote(Note note) {
		boolean saved = false;
		
		try {
			URL url = new URL(projectURL);
			HttpURLConnection conn = (HttpURLConnection) url.openConnection();
			conn.setDoOutput(true);
			conn.setRequestMethod("POST");
			conn.setRequestProperty("Content-Type", "application/json");
	
			String jsonNote = gson.toJson(note);
	
			OutputStream os = conn.getOutputStream();
			os.write(jsonNote.getBytes());
			os.flush();
	
			if (!(conn.getResponseCode()+"").matches("20*")) {
				throw new RuntimeException("Failed : HTTP error code : " + conn.getResponseCode());
			} else {
				saved = true;
			}
			
			BufferedReader br = new BufferedReader(new InputStreamReader((conn.getInputStream())));
			
			String output;
			while ((output = br.readLine()) != null) {
				if (output == null || "".equals(output)) {
					saved = false;
				}
			}
			conn.disconnect();
	
		} catch (MalformedURLException e) {
		
			e.printStackTrace();
		
		} catch (IOException e) {
		
			e.printStackTrace();
		
		}
		
		return saved;
	}
	
	@Override
	public List<Note> getNotes(String toSeek) {
		String response = "";
		JSONParser parser = new JSONParser();
		JSONObject jsonNotes = null;
		notes = new ArrayList<>();
		toSeek = toSeek.trim();
		
		try {

			URL url = new URL(projectURL);
			HttpURLConnection conn = (HttpURLConnection) url.openConnection();
			conn.setRequestMethod("GET");
			conn.setRequestProperty("Accept", "application/json");

			if (!(conn.getResponseCode()+"").matches("20*")) {
				throw new RuntimeException("Failed : HTTP error code : " + conn.getResponseCode());
			}

			BufferedReader br = new BufferedReader(new InputStreamReader((conn.getInputStream())));

			String output;
			while ((output = br.readLine()) != null) {
				response = response + output;
			}
			
			try {
				jsonNotes = (JSONObject) parser.parse(response);
			} catch (ParseException e) {
				e.printStackTrace();
			}
			
			if (jsonNotes != null) {
				for (Object i : jsonNotes.keySet()) {
					note = gson.fromJson((String) jsonNotes.get(i).toString(), Note.class);
					note.setId((String) i);
					String tags = note.formatTagsAsString();
					
					if (note.getContent().toLowerCase().contains(toSeek) || tags.toLowerCase().contains(toSeek)) {
						notes.add(note);
					}
				}
			}
			
			
			conn.disconnect();

		  } catch (MalformedURLException e) {

			e.printStackTrace();

		  } catch (IOException e) {

			e.printStackTrace();

		  }
		
		return notes;
	}

	@Override
	public List<Note> getNotes() {
		return getNotes("");
	}
	
	@Override
	public void deleteNotes(String toSeek, boolean deleteAll) {
		boolean somethingDeleted = false;
		
		if (!"".equals(toSeek) || deleteAll) {
			notes = getNotes();
			
			for (Note i : notes) {
				if ((i.getContent().toLowerCase().contains(toSeek) || i.formatTagsAsString().toLowerCase().contains(toSeek)) && i.getId() != "-1") {
			
					try {
						URL url = new URL(baseURL + urlCollection + "/" + i.getId() + dataExtension + authPrefix + authToken);
						
						HttpURLConnection conn = (HttpURLConnection) url.openConnection();
						conn.setRequestMethod("DELETE");
						conn.setRequestProperty("Accept", "application/json");
			
						if ((conn.getResponseCode()+"").matches("20*")) {
							somethingDeleted = true;
							if (!deleteAll) {
								System.out.println(">>>: deleted: " + i.getContent());
							}
						} else {
							System.out.println(">>>: there was some problem deleting your note, please check your network connectivity.     |:|  =D~~~~~");
							throw new RuntimeException("Failed : HTTP error code : " + conn.getResponseCode());
						}
						
						conn.disconnect();
					} catch (MalformedURLException e) {
					
						e.printStackTrace();
					
					} catch (IOException e) {
					
						e.printStackTrace();
	
					}
				}
			}
			
			
			displayDeleteMessage(deleteAll, somethingDeleted); //TODO
			
			}
	}
	
	
}







