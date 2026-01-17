var builder = DistributedApplication.CreateBuilder(args);

var dbserver = builder
    .AddSqlServer("ProductsDBServer")
    .WithLifetime(ContainerLifetime.Persistent);

var db = dbserver.AddDatabase("ProductsDB");

builder.AddProject<Projects.DocumentStorage>("documentstorage")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
