# how to run migrations:

```
dotnet ef migrations add Inital --project ZooManagment.DataAccess --startup-project ZooManagment.API
```

# how to update database:

```
dotnet ef database update --project ZooManagment.DataAccess --startup-project ZooManagment.API
```

Check for connection string in the appsettings.development.json


The code lacks unit tests
