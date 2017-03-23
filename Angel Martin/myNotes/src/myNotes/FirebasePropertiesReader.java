package myNotes;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class FirebasePropertiesReader implements PropertiesReader {
	
	private String baseURL = "";
	private	String urlCollection = "";
	private String dataExtension = "";
	private String authPrefix = "";
	private String authToken = "";
	
	@Override
	public void read(String configFile) {
		Properties prop = new Properties();
		InputStream input = null;

		try {

			input = new FileInputStream(configFile);

			// load a properties file
			prop.load(input);

			// get the property value and print it out
			baseURL = prop.getProperty("baseURL");
			urlCollection = prop.getProperty("urlCollection");
			dataExtension = prop.getProperty("dataExtension");
			authPrefix = prop.getProperty("authPrefix");
			authToken = prop.getProperty("authToken");

		} catch (IOException ex) {
			ex.printStackTrace();
		} finally {
			if (input != null) {
				try {
					input.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}

	}
	
	public String getBaseURL() {
		return baseURL;
	}
	
	public String getUrlCollection() {
		return urlCollection;
	}
	
	public String getAuthToken() {
		return authToken;
	}

	public String getAuthPrefix() {
		return authPrefix;
	}

	public String getDataExtension() {
		return dataExtension;
	}
	
}
