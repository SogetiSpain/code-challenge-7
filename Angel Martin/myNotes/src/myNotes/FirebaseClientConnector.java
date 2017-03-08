package myNotes;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import com.firebase.client.AuthData;
import com.firebase.client.DataSnapshot;
import com.firebase.client.Firebase;
import com.firebase.client.FirebaseError;
import com.firebase.client.ValueEventListener;
import com.firebase.security.token.TokenGenerator;

public class FirebaseClientConnector extends FirebaseConnector {
	
	private Firebase firebaseRef;
	private String clientToken;
	
	
	public FirebaseClientConnector() {
		super();
		projectURL = baseURL + authToken;
		firebaseRef = new Firebase(baseURL);
		generateToken();
		authenticateUser();
	}
	
	
	
	@Override
	public boolean addNote(Note note) {
		firebaseRef.child("data").push().setValue(note);
		
		return true;
	}

	@Override
	public List<Note> getNotes(String toSeek) {
		firebaseRef.child("data").addValueEventListener(new ValueEventListener() {
			JSONParser parser = new JSONParser();
			JSONObject jsonNotes = null;
			
			@Override
			public void onDataChange(DataSnapshot snapshot) {
				try {
					jsonNotes = (JSONObject) parser.parse(snapshot.getValue().toString());
				} catch (ParseException e) {
					e.printStackTrace();
				}
				
				if (jsonNotes != null) {
					for (Object i : jsonNotes.keySet()) {
						note = gson.fromJson((String) jsonNotes.get(i).toString(), Note.class);
						note.setId((String) i);
						String tags = note.getTagsAsString();
						
						if (note.getContent().toLowerCase().contains(toSeek) || tags.toLowerCase().contains(toSeek)) {
							notes.add(note);
						}
					}
				}
				
				
			}
			
			@Override 
			public void onCancelled(FirebaseError error) {
				
			}
		});
		
		return null;
	}

	@Override
	public List<Note> getNotes() {
		return getNotes("");
	}

	@Override
	public void deleteNotes(String toSeek, boolean deleteAll) {
		// TODO Auto-generated method stub
		
	}
	
	private void generateToken() {
		Map<String, Object> payload = new HashMap<String, Object>();
		payload.put("uid", "uniqueId1");
		payload.put("some", "arbitrary");
		payload.put("data", "here");
		TokenGenerator tokenGenerator = new TokenGenerator("e6d0skdx8BIET471DeFwbKtkHTFX1RH4omEaQcDl");
		clientToken = tokenGenerator.createToken(payload);
	}
	
	private void authenticateUser() {
		firebaseRef.authWithCustomToken(clientToken, new Firebase.AuthResultHandler() {
		    @Override
		    public void onAuthenticationError(FirebaseError error) {
		        System.err.println("Login Failed! " + error.getMessage());
		    }
		    @Override
		    public void onAuthenticated(AuthData authData) {
		        System.out.println("Login Succeeded!");
		    }
		});
	}

}
