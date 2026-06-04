# CQRS-ProductService

CQRS-ProductService is a .NET Worker Service that implements the Command Query Responsibility Segregation (CQRS) pattern and communicates asynchronously through Azure Service Bus.

The service is designed to process product-related commands and events in a scalable, reliable, and loosely coupled architecture.

## Features

* CQRS-based architecture
* Background Worker Service implementation
* Azure Service Bus integration for asynchronous messaging
* Command and event processing
* Dependency Injection support
* Structured logging
* Scalable and cloud-friendly design

## Architecture

The service listens to Azure Service Bus queues or topics and processes incoming messages based on the configured command and event handlers.

```text
+------------------+
| Client Services  |
+--------+---------+
         |
         v
+------------------+
| Azure Service Bus|
+--------+---------+
         |
         v
+-----------------------+
| CQRS-ProductService   |
|  - Command Handlers   |
|  - Event Handlers     |
|  - Business Logic     |
+-----------------------+
         |
         v
+------------------+
| Data Store       |
+------------------+
```

## Prerequisites

* .NET 8.0 (or your target framework)
* Azure Service Bus Namespace
* Azure Subscription
* Visual Studio 2022 / VS Code

## Configuration

Configure the Azure Service Bus connection string and queue/topic settings in `appsettings.json`.

```json
{
  "AzureServiceBus": {
    "ConnectionString": "<your-connection-string>",
    "QueueName": "product-queue"
  }
}
```

Alternatively, use environment variables for production deployments.

```bash
AzureServiceBus__ConnectionString=<your-connection-string>
AzureServiceBus__QueueName=product-queue
```

## Running the Service

### Local Development

```bash
dotnet restore
dotnet build
dotnet run
```

### Publish

```bash
dotnet publish -c Release
```

## Message Processing

The worker continuously listens for messages from Azure Service Bus and:

1. Receives commands/events.
2. Validates incoming messages.
3. Executes the appropriate CQRS handler.
4. Persists business data.
5. Publishes follow-up events when required.

## Project Structure

```text
CQRS-ProductService/
│
├── Handlers/
│   ├── Commands/
│   └── Events/
│
├── Services/
│
├── Infrastructure/
│   ├── Messaging/
│   └── Persistence/
│
├── Models/
│
├── Worker.cs
├── Program.cs
└── appsettings.json
```

## Logging

Application logs are generated through the configured logging provider and can be integrated with:

* Azure Application Insights
* Azure Monitor
* Serilog
* ELK Stack

## Deployment

The service can be deployed to:

* Azure App Service
* Azure Container Apps
* Azure Kubernetes Service (AKS)
* Windows/Linux Service
* Docker Containers

## Contributing

Contributions are welcome. Please create an issue or submit a pull request for improvements and bug fixes.

## License

This project is licensed under the MIT License.
