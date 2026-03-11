var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.otoQr>("otoqr");

builder.Build().Run();
