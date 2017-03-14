package myNotes;

public abstract class MongoConnector extends Connector {
	
	protected String address = "";
	protected int port = -1;
	protected String databaseName = "";
	
	
	public MongoConnector() {
		super();
		MongoPropertiesReader pr = new MongoPropertiesReader();
		pr.read("mongo.properties");
		
		address = pr.getAddress();
		port = pr.getPort();
		databaseName = pr.getDatabaseName();
	}
	
	
	
}
