# Angular Messenger

This is a simple web chat application. The following technologies are used in this project:

- Microsoft SQL Server
- ASP.NET Core Web API
- Unit Testing using xUnit and Moq
- EFCore
- Angular
- NgRx

## Database Setup

In order to create the database and the tables you should have Microsoft Sql Server installed on your system. Then, from the project root directory go to the **/database** directory and execute create queries in the ascending order. Then go to the **/triggers** directory and execute the trigger queries to create the triggers for the respective tables.

## Set Database Connection String

After creating your database, open your Visual Studio (At the time of making this project, I am using Visual Studio Community 2019, version 16.8.5). Make sure that you have .Net core 5.0 or higher installed on your system. Right click on **WebServer** project, select Manage User secrets and paste the following text in it:

```
{
  "ConnectionString": [your-connection-string]
}
```

## Start the server

Open a new terminal and go to the server directory by running the following command:

```
cd back-end/WebServer
```

Now you are in the server directory and you can start your server by running the following command in the terminal:

```
dotnet run
```

Now your server will start listening on the localhost:5000

## Run the Angular Messenger

In order to run the angular messenger, you should Angular 11.x.x installed. At the time of creating this project, I am using Angular 11.1.2. From the root of the project, run the following command in a new terminal:

```
cd front-end/angular-messenger
```

Run the project by typing the following command in the terminal:

```
ng serve -o
```

The above command will open a tab in your browser named **Angular Messenger**.
Now, the messenger is ready for chat!
