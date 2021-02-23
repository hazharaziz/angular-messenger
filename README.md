# Angular Messenger

This is a simple web chat application. The following technologies is used in this project:
 
 - Microsoft SQL Server
 - ASP.NET Core Web API 
 - Unit Testing using xUnit and Moq 
 - Angular
 - EFCore 

## Database Setup
In order to create the database and the tables you should have Microsoft Sql Server installed on your system. Then, from the project root directory go to the **/database** directory and execute create queries in the ascending order. Then go to the **/triggers** directory and execute the trigger queries to create the triggers for the respective tables.   

## Set Database Connection String
After creating your database, open your Visual Studio (At the time of making this project, I am using Visual Studio Community 2019, version 16.8.5). Make sure that you have .Net core 5.0 or higher installed on your system. Right click on **WebServer** project, select Manage User secrets and paste the following text in it:
```
{
  "ConnectionString": [your-connection-string]
}
``` 
