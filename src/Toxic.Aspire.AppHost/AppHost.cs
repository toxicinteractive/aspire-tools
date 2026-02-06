using Toxic.Aspire;
using Toxic.Aspire.NamingConventions;

var builder = DistributedApplication
    .CreateBuilder(args)
    .WithAzureNamingConvention("test");

var app = builder
    .WithToxicDefaults()
    .Build();

app.Run();
