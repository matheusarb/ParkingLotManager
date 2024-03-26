<h1>Parking Lot Manager v1</h1>

<img src="https://i.imgur.com/ektThjj.png" width="640px" height="480px"></img>

<hr>


![GitHub repo size](https://img.shields.io/github/repo-size/matheusarb/ParkingLotManager?style=for-the-badge)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)

> Parking Lot Manager v1 is the backend of a REST API which mocks a real-life parking lot application manager.


## 💻 Requirements
In order to install <b>Parking Lot Manager v1</b>, you need to have at least .NET Core 8.0.X. SDK on your machine. You can install it from <a href="https://dotnet.microsoft.com/en-us/download/dotnet" target="_blank">here</a>.<br>
Then, after cloning this repo, run the following commands (in order) within the Root folder of the .csproj:
```
dotnet restore
```
```
dotnet build
```
```
dotnet run
```
You can also run these commands via your preferred IDE (we recommend using Visual Studio 2022)

## :star: Features
+ CRUD Operations
+ Authentication and Authorization Services using RBAC(Role-based Access Control) pattern and API Key
+ Unit testing with XUnit
+ Database with SqlServer and EntityFramework Core
+ Fully documented API 
+ Extension methods for exception filtering
+ API integration with Report.Api to generate reports about vehicles flow
+ SQL script to ease the creation of records on the database
+ Postman Collection(JSON in the Root folder) already organized to test our endpoints

## :bulb: Tips on how to use it
* This project is intended to simulate the backend of a parking lot workflow via API requests
* The ParkingLotManager.WebApi is fully documented, where you can find all the information about each endpoint and what it does
* You can run the script.sql to add entries to your database
* RBAC is binded to be used on GetFordBrand endpoint(Vehicles/GetFordBrand). First, you need to create an admin user(Account/CreateAdmin) and log in (Account/Login). You will receive a Bearer Token which is needed in the request.
  * Pass the Bearer Token in the "Authorization" tab on Postman. Select Type "Bearer Token"
  * Before sending a request make sure you have at least one vehicle which "brand" is "Ford"
  * ![image](https://github.com/matheusarb/ParkingLotManager/assets/89713533/b39d016e-5ddb-42d4-ae5c-db2060c6ef65)
* Set your ConnectionString inside appsettings.Development.json file
