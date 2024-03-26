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
+ Authentication and Authorization Services using RBAC pattern and API Key
+ Unit testing with XUnit
+ Database with SqlServer and EntityFramework Core
+ API Documentation
+ Extension methods for exception filtering
+ API integration with Report.Api to generate reports about vehicles flow
+ SQL script to insert entries on database
+ Postman Collection already organized to test our endpoints in the root folder

## :bulb: Tips on how to use it
* This project is intended to simulate the backend of a parking lot workflow via API requests
* The ParkingLotManager.WebApi is fully documented, where you can find all the information about each endpoint and what it does
* You can run the script.sql to add entries to your database
* RBAC is binded to be used on GetFordBrand endpoint(Vehicles/GetFordBrand). First, you need to create an admin user(Account/CreateAdmin) and log in (Account/Login). Before sending a request make sure you have at least one vehicle which "brand" is "Ford" 
