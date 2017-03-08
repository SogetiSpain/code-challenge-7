package myNotes;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

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
		String jsonNote = gson.toJson(note);
		firebaseRef.child("clientData").setValue(jsonNote);
		
		return true;
	}

	@Override
	public List<Note> getNotes(String toSeek) {
		firebaseRef.child("message").addValueEventListener(new ValueEventListener() {
			@Override
			public void onDataChange(DataSnapshot snapshot) {
				System.out.println(snapshot.getValue());  //prints "Do you have data? You'll love Firebase."
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
