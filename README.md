# kafka-forwarder-kestrel

A Kafka Forwarder written in ASP.NET Core (Kestrel) for Segment outbound events.

# Local Setup

We use https://github.com/conduktor/kafka-stack-docker-compose to run a local Kafka cluster.

# Running

If using VSCode, there's a Run and Debug configuration for the project. Otherwise, run `dotnet run` from the project directory.

# OpenAPI Docs

The OpenAPI docs are available at `/swagger/index.html` (locally under `https://localhost:7163/swagger/index.html`).