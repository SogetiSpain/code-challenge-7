package myNotes;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class MongoPropertiesReader implements PropertiesReader {
	
	private String address = "";
	private int port = -1;
	private String databaseName = "";
	
	
	@Override
	public void read(String configFile) {
		Properties prop = new Properties();
		InputStream input = null;

		try {

			input = new FileInputStream(configFile);

			// load a properties file
			prop.load(input);

			// get the property value and print it out
			address = prop.getProperty("address");
			port = Integer.valueOf(prop.getProperty("port"));
			databaseName = prop.getProperty("databaseName");

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

	public String getAddress() {
		return address;
	}
	
	public int getPort() {
		return port;
	}

	public String getDatabaseName() {
		return databaseName;
	}
	
}
