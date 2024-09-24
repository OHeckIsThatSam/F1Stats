Prerequisites;
- This application requires a Microsoft Sql Express server as a database

The sql for table creation and a backup copy of the database is provided in the folder 'Sql'

Using visual studio's internal MSSQLLocalBD
1. View->SQL Server Object Explorer
2. If this option doesn't exist make sure you have the web development workload for visual studio installed
3. Open SQL Server then (localdb)\MSSQLLocalDB
4. Right click Databases->Add New Database
5. Call it 'SystemsProgramming', leave the file location and create
6. In the Solution Explorer open the 'Sql' folder
7. For each file in 'Sql' folder 
	7.1. Open file
	7.2. Click execute (green arrow top left)
	7.3. In the popup, open local then MSSQLLocalDB
	7.4. Select the Database to be 'SystemsProgramming'
	7.5. Select Run
8. Now that the database is created check that the connection string in Program matches the server and database name
9. Your good to go! Enter data into the database using the program. Race results files can be found in the folder 'Results'.