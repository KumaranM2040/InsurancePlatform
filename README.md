# InsurancePlatform

## ASP .Net Core

MVC - Model View Controller

## Micro ORM
Dapper was chosen as the Micro ORM for our Insurance Application. It has all the features and support required to back Enterprise application. 

## Microsoft SQL Server Setup
MSQL can now be run on ubuntu machines, so this unlocks hosting on various cloud platforms (AWS, Google Cloud, Azure)
Image was obtained from docker hub using the below command:

`docker pull mcr.microsoft.com/mssql/server`

To run the image use the below command:

`docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`


