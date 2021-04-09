# Distributed Systems with .NET
This repo contains code and samples for Dylan Beattie's "Distributed Systems with .NET" workshop with fwdays, April 2021.

The starting point for the workshop is a simple ASP.NET MVC website called **Autobarn**. If you're using Visual Studio or Rider, you can build and run the application by opening `Autobarn\Autobarn.sln`

If you're using the `dotnet` command line tools, you can run the solution using:

```bash
cd Autobarn
dotnet build
dotnet run --project Autobarn.Website
```

That should get you the Autobarn website homepage, which is very similar to the ASP.NET MVC standard `dotnet new mvc` website.

The project uses an in-memory database that's initialised using data stored in JSON files in `Autobarn.Website\JsonData`. Any records you add to the database will be lost when you rebuild the solution; this is fine. This is a workshop, not a production system. 😉