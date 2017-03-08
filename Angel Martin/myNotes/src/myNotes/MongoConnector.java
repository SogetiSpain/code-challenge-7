package myNotes;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.List;

import com.google.gson.Gson;

public class MongoConnector extends Connector {
	
	private String projectURL = "";
	private Gson gson = new Gson();
	private List<Note> notes;
	
	
	public MongoConnector() {
		super();
		MongoPropertiesReader pr = new MongoPropertiesReader();
		pr.read("mongo.properties");
		
		projectURL = pr.getBaseURL() + pr.getPassword() + pr.getComponentURL();
		
	}
	

	@Override
	public boolean addNote(Note note) {
		boolean correctlyAdded = true;
		
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
			}
			
			BufferedReader br = new BufferedReader(new InputStreamReader((conn.getInputStream())));
			
			String output;
			while ((output = br.readLine()) != null) {
				if (output == null || "".equals(output)) {
					correctlyAdded = false;
				}
			}
			conn.disconnect();
			
			if (correctlyAdded)
				System.out.println(">>>: ...saved...");
			else{
				System.out.println(">>>: there was some problem saving your note, please check your network connectivity.   =D--");
			}
	
		} catch (MalformedURLException e) {
		
			e.printStackTrace();
		
		} catch (IOException e) {
		
			e.printStackTrace();
		
		}
		
		return true;
	}

	@Override
	public List<Note> getNotes(String toSeek) {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public List<Note> getNotes() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	public void deleteNotes(String toSeek, boolean deleteAll) {
		// TODO Auto-generated method stub
		
	}

}
