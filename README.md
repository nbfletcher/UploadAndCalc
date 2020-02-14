To run the demo, you first need to create the database.  There is already a migration for this so you can create the database via Package manager.

Steps:

1. Ensure AppSettings.json has the correct value for DefaultConnection
2. In Package manager console, run the following command:
   Update-Database
3. This should create a database called UploadAndCalc