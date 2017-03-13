package myNotes;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Scanner;
import java.util.Set;

import com.firebase.client.AuthData;
import com.firebase.client.DataSnapshot;
import com.firebase.client.Firebase;
import com.firebase.client.FirebaseError;
import com.firebase.client.ValueEventListener;
import com.firebase.security.token.TokenGenerator;

public class FirebaseClientConnector extends FirebaseConnector {
	
	private static final int DB_TIMEOUT = 2;
	private Firebase firebaseRef;
	private String clientToken;
	private boolean somethingDeleted;
	
	
	public FirebaseClientConnector() {
		super();
		projectURL = baseURL + authToken;
		firebaseRef = new Firebase(baseURL);
		generateToken();
		authenticateUser();
	}
	
	
	
	@Override
	public boolean addNote(Note note) {
		boolean saved = false;
		
		try {
			firebaseRef.child("data").push().setValue(note);
			saved = true;
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return saved;
	}

	@Override
	public List<Note> getNotes(String toSeek) {
		notes = new ArrayList<>();
		double timeout = 0;
		
		firebaseRef.child("data").addValueEventListener(new ValueEventListener() {
			@Override
			public synchronized void onDataChange(DataSnapshot snapshot) {
	            Note note = null;
				for (DataSnapshot postSnapshot: snapshot.getChildren()) {
					note = postSnapshot.getValue(Note.class);
					if (note.getContent().toLowerCase().contains(toSeek) || note.formatTagsAsString().toLowerCase().contains(toSeek)) {
						notes.add(note);
					}
		        }
			}
			
			@Override 
			public void onCancelled(FirebaseError error) {
				
			}
		});
		
		// custom timeout to prevent asynchronous behavior of listeners
		while (notes.isEmpty() && timeout < DB_TIMEOUT) {
			try {
				Thread.sleep(100);
				timeout = timeout + 0.100;
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
		
		return notes;
	}

	@Override
	public List<Note> getNotes() {
		return getNotes("");
	}

	@Override
	public void deleteNotes(String toSeek, boolean deleteAll) {
		somethingDeleted = false;
		double timeout = 0;
		
		if (!"".equals(toSeek) || deleteAll) {
			notes = getNotes();
			
			
			
			for (Note i : notes) {
				if ((i.getContent().toLowerCase().contains(toSeek) || i.formatTagsAsString().toLowerCase().contains(toSeek)) && i.getId() != "-1") {

					try {
					
						firebaseRef.child("data").addValueEventListener(new ValueEventListener() {
							@Override
							public synchronized void onDataChange(DataSnapshot snapshot) {
					            Note note = null;
					            
								for (DataSnapshot postSnapshot: snapshot.getChildren()) {
									note = postSnapshot.getValue(Note.class);
									if (note.getContent().toLowerCase().contains(toSeek) || note.formatTagsAsString().toLowerCase().contains(toSeek)) {
										Firebase ref = postSnapshot.getRef();
										ref.removeValue();
										
										somethingDeleted = true;
										
										if (!deleteAll) {
											System.out.println(">>>: deleted: " + i.getContent());
										}
									}
						        }
							}
							
							@Override 
							public void onCancelled(FirebaseError error) {
								
							}
						});
						
					} catch (Exception e) {
						System.out.println(">>>: there was some problem deleting your note, please check your network connectivity.     |:|  =D~~~~~");
						e.printStackTrace();
					}
					
				}
			}
			
			// custom timeout to prevent asynchronous behavior of listeners
			while (!somethingDeleted && timeout < DB_TIMEOUT) {
				try {
					Thread.sleep(100);
					timeout = timeout + 0.100;
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}
			
			displayDeleteMessage(deleteAll, somethingDeleted);
			
		}
		
	}
	
	private void generateToken() {
		Map<String, Object> payload = new HashMap<String, Object>();
		payload.put("uid", "uniqueId5461324564");
		payload.put("some", "arbitrary");
		payload.put("data", "here");
		TokenGenerator tokenGenerator = new TokenGenerator();
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
