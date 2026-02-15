using Azure.Provisioning.AppContainers;
using Microsoft.Extensions.DependencyInjection;
using AspireTools;
using AspireTools.Codespaces;
using AspireTools.NamingConventions;
using AspireTools.Telemetry.NewRelic;
using AspireTools.Umbraco;

var builder = DistributedApplication
    .CreateBuilder(args)
    // enable resource naming in publish mode
    // note that the default region used is defined in appsettings.json/env variable
    .WithAzureNamingConvention("project");

builder
    .AddProject<Projects.AspireTools_SampleApp>("api")
    
    // without this, the Azure resource name will be: "ca-project-prod-swc" (without resource name)
    // with this makes it: "ca-project-appname-prod-swc" (with custom resource name)
    // .WithAzureWorkloadName("appname")

    // use this to mark this as an Umbraco project
    // this will add Codespaces support
    // .IsUmbraco()

    // use this to export telemetry to a New Relic OTLP collector
    // .WithNewRelicTelemetry("api-key from a secret or parameter")
    
    // use these to exclude this resource from getting 
    // deployed to Azure while publishing in a specific environment
    // .ExcludeFromDevelopmentManifest()
    // .ExcludeFromStageManifest()
    // .ExcludeFromProductionManifest()
    // .ExcludeFromManifest(["environment name"])

    .WithExternalHttpEndpoints()
    .PublishAsAzureContainerApp((app, infra) => 
    {
        // shortcut for app.GetProvisionableResources().OfType<ContainerApp>().Single()
        var ca = app.GetProvisionableResource<ContainerApp>();
    });

if (builder.ExecutionContext.IsCodespace)
{
    // custom builder actions for when the app host 
    // is running (not published) in a GitHub Codespace
}

var app = builder
    .WithAspireToolkitDefaults()
    .Build();

// this is available when the app host is running in a GitHub Codespace
// var codespaceOpts = app.Services.GetService<CodespacesOptions>();

app.Run();
