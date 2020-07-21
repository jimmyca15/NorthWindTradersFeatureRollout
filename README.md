# Northwind Traders Bike Shop Beta Rollout

This repository provides an example of how the Northwind Traders rolled out a new beta version of their website using Azure App Configuration.

## Running Locally

To run this application locally, a connection to Azure App Configuration needs to be set up. There are two options for authentication with Azure App Configuration.

* AAD
* Connection String

### AAD Connection

If you have access to AAD, a service principal can be used to run the application locally. The following environmental variables related to the service principal need to be set when running the application.

* AZURE_CLIENT_ID
* AZURE_TENANT_ID
* AZURE_CLIENT_SECRET

The service principal needs to be assigned either the "Azure App Configuration Data Reader" or "Azure App Configuration Data Owner" role in the target App Configuration instance.

### Connection String

To use a connection string, the `program.cs` file needs to be updated. Update the connection to Azure App Configuration to look like

`o.Connect(intermediate["AppConfigurationConnectionString"]);`

Then, create an environment variable named `AppConfigurationConnectionString` that has a connection string for the target App Configuration instance.