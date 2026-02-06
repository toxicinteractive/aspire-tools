using Toxic.Aspire;
using Toxic.Aspire.NamingConventions;

var builder = DistributedApplication
    .CreateBuilder(args)
    .WithAzureNamingConvention("test");

builder
    .AddProject<Projects.Toxic_Aspire_SampleApp>("api")
    .WithExternalHttpEndpoints();

var app = builder
    .WithToxicDefaults()
    .Build();

app.Run();
