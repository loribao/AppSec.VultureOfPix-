var builder = DistributedApplication.CreateBuilder(args);


//var mongo = builder.AddMongoDB("mongo", 27017).AddDatabase("mongodb","appsec");
var apiService = builder.AddProject<Projects.AppSec>("appsec");                        

builder.Build().Run();
