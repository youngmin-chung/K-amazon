# K-amazon - YC C# and ASP.NET /w MVC  Project

"Do you need a web application for your business?"


## Getting Started

In order to get started with this project, you will need to run it on Visusal Studio 2019 or similar IDE like Visual Studio Community. Then navigate to `http://localhost:44306` or to different port number to view the shopping web app. 

### Prerequisites

What you need to install:

__Front-end:__
- To be updated...,

__Front-end development dependencies:__
- To be updated...,

__Back-end:__
- To be updated...,

__Back-end development dependencies:__
- To be updated...,

### Installing

Clone project and make sure to follow next stop in Visual Studio.
- Create local DB to `(localdb)\ProjectV13 (SQL Server 13.xxx)` on SQL Server Object Exploer
- Change file `appsetting.json` as follows,

`{
"Logging": {
  "LogLevel": {
    "Default": "Warning"
    }
  },
"AllowedHosts": "*",
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\ProjectsV13;Initial Catalog={YOUR DB NAME HERE without curly brace};Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  }
}`
- Open Package Manager Console: 
   insatll NuGet Packages
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
  - `Microsoft.AspNetCore.ResponseCompression`
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.EntityFrameworkCore.Tools`
  - `Newtonsoft.Json`
  
   Enter `add-migration first` and then `update-database`

## Running the tests

No tests added to this project

## Deployment

To Be Deployed on Azure

## Versioning

K-amazon version 0.0.1

## Authors

* Youngmin Chung: C# | ASP.NET | JQuery | Razor  | Google Maps API | Bootstrap/CSS | Azure | SQL



## License

This project is licensed under the YC License

## Acknowledgments

* Learning C# and the latest ASP.NET Core with creating web application
* Professors in Fanshawe College, family, and friends for their sincere support 
* All people who posted neccessary inforamtion about this application on StackOverFlow and their support and suggestions



## App Flow

__Welcome Page! - The first page of K-amazon web app__
- TO BE POSTED

__Register / Login / Logoff __
- TO BE POSTED

__Add to cart / Cart status__
- TO BE POSTED

__Order message / Purchase Order Page__
- TO BE POSTED

__The nearest three stores on google map__
- TO BE POSTED
