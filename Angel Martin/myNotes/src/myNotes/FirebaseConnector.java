package myNotes;

import java.util.List;

import com.google.gson.Gson;

public abstract class FirebaseConnector extends Connector {
	
	protected String baseURL = "";
	protected String urlCollection = "";
	protected String dataExtension = ".json";
	protected String authPrefix = "?auth=";
	protected String authToken = "";
	protected String projectURL;
	protected Gson gson = new Gson();
	protected List<Note> notes;
	
	
	public FirebaseConnector() {
		super();
		FirebasePropertiesReader pr = new FirebasePropertiesReader();
		pr.read("firebase.properties");
		
		baseURL = pr.getBaseURL();
		urlCollection = pr.getUrlCollection();
		authToken = pr.getAuthToken();
	}
	
	
	
}
