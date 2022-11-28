# unit-of-work-entity-framework-domain-driven-design
<h1>Overview</h1>

A sample proof of concept app using Domain Driven Design, Unit of Work pattern and Entity Framework, written in .NET 6.0. The App is written using the async/await pattern [Tasks] to achieve asynchronous programming.

<h1>Controllers</h1>

This solution contains 3 main controllers with the respective CRUD operations for the specific controllers:
The app comes with swagger out of the box.

<br>
<h2>KeywordController</h2>

    - AddAsync
    - BulkAddAsync
    - DeleteAsync
    - UpdateAsync
    - GetAsync
    - GetAllAsync

<br>
<h2>CampaignController</h2>

    - AddAsync
    - BulkAddAsync
    - DeleteAsync
    - UpdateAsync
    - GetAsync
    - GetAllAsync

<br>
<h2>ClientController</h2>

    - GetAsync

<br>
<h1>Project Structure</h1>

The project is split into the following layers:
- Host Layer [This is where the startup host project will be found.]
- Application Layer [This is where you will be able to find the Services interacting with the Domain and Infra layer.]
- Infrastructure Layer [Here you will find the SQL repositories. This layer will deal with external infra dependencies. This layer will only be invoked by the Application Layer]
- Domain Layer [Here you will find the Domain models defined for our domain. Any mutator methods on the models should be placed here.

<br>
<h1>How to Setup</h1>

The project depends on the MSSQL database called MarketingSuite.

If you already have a live MSSQL database created, you will need to only change the connection string in the appsettings.Development.json under the ConnectionStrings => SqlConnection section.

If you don't have the MSSQL server installed, there is a docker-compose.yml file which can be run with the following command:

```
docker-compose up -d
```

The above command will only setup the MSSQL server, it is up to you to then create the database and run the GenerateSQL.sql manually on the docker MSSQL server.

Don't forget to then change the connection string to point to the local docker MSSQL Server.

<br>
<h1>Testing</h1>
Under the root directory, you will find a test folder that will encapsulate all unit tests. The testing framework installed is NUnit.
