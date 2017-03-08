package myNotes;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class MongoPropertiesReader implements PropertiesReader {
	
	private String baseURL = "";
	private String componentURL = "";
	private String password = "";
	
	
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
			componentURL = prop.getProperty("componentURL");
			password = prop.getProperty("password");

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
	
	public String getComponentURL() {
		return componentURL;
	}
	
	public String getPassword() {
		return password;
	}
}
