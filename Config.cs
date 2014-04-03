private const string ASSOCIATE_EXTERNAL_ID = "AssociateExternalID";
public static Guid AssociateExternalID
{
    get
    {
        if (ConfigurationManager.AppSettings[ASSOCIATE_EXTERNAL_ID] == null)
            throw new ConfigurationErrorsException("Configuration key '" + ASSOCIATE_EXTERNAL_ID + "' is missing!");
        return Guid.Parse(ConfigurationManager.AppSettings[ASSOCIATE_EXTERNAL_ID]);
    }
}
