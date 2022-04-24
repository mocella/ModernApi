

*These scripts will only run in LOCAL, DEV and QA. 
*You must pass in an Env enum when creating a DbUpdater if you want the scripts to run.
*By default Evn is set to Undefined.

*These scripts are executed every time.

*Scripts in the folder journal to null, so 
 the SchemaVersions dbup migration table 
 will NOT show these as entries.

 NOTE: This are excluded from the migration process as they arn not marked as embedded resources, just included here for reference.  
 you'd need to follow these instructions first before these scripts will deploy cleanly: https://msdn.microsoft.com/en-us/library/ms131048.aspx