# RabbitMQ Infrastructure Auto-Setup Documentation

## Overview
Your application now automatically creates and verifies RabbitMQ exchanges, queues, and bindings on startup.

## How It Works

### 1. Infrastructure Manager (`RabbitMqInfrastructureManager`)
- Connects to RabbitMQ on startup
- Provides methods to declare exchanges, queues, and bindings
- Uses RabbitMQ's idempotent operations (declaring an existing entity doesn't cause errors)

### 2. Configuration (`RabbitMqInfrastructureConfiguration`)
Define your infrastructure in `appsettings.json`:
```json
"RabbitMqInfrastructure": {
  "Exchanges": [
    {
      "Name": "user.events",
      "Type": "fanout",
      "Durable": true,
      "AutoDelete": false
    }
  ],
  "Queues": [
    {
      "Name": "email-service-queue",
      "Durable": true,
      "Exclusive": false,
      "AutoDelete": false
    }
  ],
  "Bindings": [
    {
      "QueueName": "email-service-queue",
      "ExchangeName": "user.events",
      "RoutingKey": ""
    }
  ]
}
```

### 3. Hosted Service (`RabbitMqInfrastructureHostedService`)
- Runs automatically when the application starts
- Reads configuration from `appsettings.json`
- Declares all exchanges, queues, and bindings before the app starts accepting requests
- Order of operations:
  1. Declare all exchanges
  2. Declare all queues
  3. Create all bindings

## Key Features

### ✅ Idempotent Operations
RabbitMQ's declare operations are safe to call multiple times:
- If an exchange/queue exists with the same settings → No error
- If an exchange/queue exists with different settings → Error (protects you from mistakes)
- If it doesn't exist → Created

### ✅ Configuration-Driven
Add new exchanges/queues without changing code - just update `appsettings.json`

### ✅ Environment-Specific
You can have different configurations per environment:
- `appsettings.json` - Base configuration
- `appsettings.Docker.json` - Docker-specific overrides
- `appsettings.Development.json` - Local development

### ✅ Logging
Full logging of infrastructure setup:
- Connection establishment
- Exchange declarations
- Queue declarations (with current message/consumer counts)
- Binding creations

## Current Setup

Your application is configured with:

**Exchange:** `user.events` (Fanout)
- Broadcasts all messages to all bound queues
- Perfect for multi-service event distribution

**Queues:**
1. `email-service-queue` - For email notifications
2. `notification-service-queue` - For in-app notifications

**Bindings:**
- Both queues bound to `user.events` exchange
- Every message published to `user.events` goes to both queues

## Adding New Infrastructure

To add a new service that listens to user events:

1. Add a new queue in `appsettings.json`:
```json
{
  "Name": "analytics-service-queue",
  "Durable": true,
  "Exclusive": false,
  "AutoDelete": false
}
```

2. Add a binding:
```json
{
  "QueueName": "analytics-service-queue",
  "ExchangeName": "user.events",
  "RoutingKey": ""
}
```

3. Restart the application - infrastructure is automatically created!

## Testing

When you start your MinimalAPI.Host, you'll see logs like:
```
[Information] RabbitMQ Infrastructure Hosted Service is starting - setting up exchanges, queues, and bindings
[Information] Declaring exchange: user.events (Type: fanout, Durable: True)
[Information] Exchange user.events declared successfully
[Information] Declaring queue: email-service-queue (Durable: True, Exclusive: False)
[Information] Queue email-service-queue declared successfully (Messages: 0, Consumers: 0)
[Information] Binding queue email-service-queue to exchange user.events with routing key ''
[Information] Queue email-service-queue bound to exchange user.events successfully
[Information] RabbitMQ infrastructure setup completed successfully
```


