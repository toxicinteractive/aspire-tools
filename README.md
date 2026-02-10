# Toxic.Aspire
Contains sensible conventions and utilities for Aspire projects deployed to Azure, used internally at Toxic Interactive Solutions.

## How to use
1. Install the `Toxic.Aspire` nuget package to your app host project and your individual Aspire resource projects. The package version aligns with the corresponding Aspire version.
2. Call the `WithToxicDefaults()` extension method on your builder objects in both the app host file and each of the individual Aspire resource projects. See the [wiki on the GitHub page](https://github.com/toxicinteractive/toxic-aspire/wiki) for details.
3. Use the various extension methods and utilities included.
4. Visit the [GitHub page](https://github.com/toxicinteractive/toxic-aspire) for a sample project and explanation of below features.

## Features
### Automatic Azure resource naming convention for publish mode
### Includes the features of the Aspire ServiceDefaults project
### Simplify custom certificate and CA trust for projects, containers and JavaScript apps
### Support for Umbraco running in GitHub Codespaces
### Use secure health probes for container apps
### Adds a New Relic OTLP extension
### Misc extensions and utilities
