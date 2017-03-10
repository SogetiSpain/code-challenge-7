package myNotes;

import java.util.List;

import com.google.gson.Gson;

public abstract class FirebaseConnector extends Connector {
	
	protected String baseURL = "";
	protected String urlCollection = "";
	protected String dataExtension = "";
	protected String authPrefix = "";
	protected String authToken = "";
	protected String webAPIKey = "";
	protected String projectURL;
	protected Gson gson = new Gson();
	protected List<Note> notes;
	
	
	public FirebaseConnector() {
		super();
		FirebasePropertiesReader pr = new FirebasePropertiesReader();
		pr.read("firebase.properties");
		
		baseURL = pr.getBaseURL();
		urlCollection = pr.getUrlCollection();
		dataExtension = pr.getDataExtension();
		authPrefix = pr.getAuthPrefix();
		authToken = pr.getAuthToken();
		webAPIKey = pr.getWebAPIKey();
	}
	
	
	
}
