var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Morganas>("morganas-api");

builder.AddProject<Projects.MorganasUmbraco>("morganasumbraco-api");

builder.Build().Run();
