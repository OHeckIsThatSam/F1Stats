using Database;
using Menus;

Environment.SetEnvironmentVariable("f1ConnectionString",
                                  @"Server=(localdb)\\mssqllocaldb;
                                    Database=SystemsProgramming;
                                    Trusted_Connection=True;");

ArgumentHandler argumentHandler = new(args);

// Runs a dummy query agianst the database while the user is navigating menus
// this gets all the data from the database and loads the program,
// essentially caching it in the background to improve speed apoun the first request
//https://www.c-sharpcorner.com/article/Threads-in-CSharp/
Thread thread = new(new ThreadStart(QueryManager.Init));
thread.Start();

TitleMenu titleScreen = new();