# WebApplicationSearch
## The app is written in asp dotnet core 3.1 and angular 8 and prepared to run in containers.
### The approach used was code first, mssql database is created automatically, on the first connection  
   ***If you don't use docker-compose to run the application, do not forget to correctly set the connection string for DBContext to the database in the file appsettings.json.  
By default setting for docker.***  
If use docker(recommended), download images before start - that will speed up application launch  
```bash
docker pull  mcr.microsoft.com/mssql/server
docker pull  mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
docker pull  mcr.microsoft.com/dotnet/core/sdk:3.1-buster
```
also at the first start, there may be a delay associated with different launch speeds of mssql and application  
***WebApplicationSearch uses standard ports(80 and 443), so make sure they are free or reassign in docker conf files***
