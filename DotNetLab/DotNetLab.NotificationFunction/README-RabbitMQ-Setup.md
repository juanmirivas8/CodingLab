# RabbitMQ Notification Function Setup

This document describes the RabbitMQ infrastructure setup required for the NotificationFunction to consume user events.

## Queue Setup

The NotificationFunction listens to three queues that need to be created and bound to the `user.events` exchange:

### Queues:
1. `user.notifications.created` - Receives user creation events
2. `user.notifications.updated` - Receives user update events  
3. `user.notifications.deleted` - Receives user deletion events

### Exchange:
- **Name**: `user.events`
- **Type**: `topic`
- **Durable**: `true`

### Bindings:
- Queue: `user.notifications.created` → Exchange: `user.events` → Routing Key: `user.created`
- Queue: `user.notifications.updated` → Exchange: `user.events` → Routing Key: `user.updated`
- Queue: `user.notifications.deleted` → Exchange: `user.events` → Routing Key: `user.deleted`

## Setup Commands (RabbitMQ Management UI or CLI)

### Using RabbitMQ Management UI:
1. Navigate to http://localhost:15672 (default credentials: guest/guest)
2. Go to **Queues** tab and create the three queues listed above
3. Go to **Exchanges** tab and verify `user.events` exchange exists (should be created by the API)
4. For each queue, go to **Bindings** section and add bindings with the routing keys specified above

### Using RabbitMQ CLI (rabbitmqadmin):

```bash
# Declare the exchange (if not already created by the API)
rabbitmqadmin declare exchange name=user.events type=topic durable=true

# Declare queues
rabbitmqadmin declare queue name=user.notifications.created durable=true
rabbitmqadmin declare queue name=user.notifications.updated durable=true
rabbitmqadmin declare queue name=user.notifications.deleted durable=true

# Create bindings
rabbitmqadmin declare binding source=user.events destination=user.notifications.created routing_key=user.created
rabbitmqadmin declare binding source=user.events destination=user.notifications.updated routing_key=user.updated
rabbitmqadmin declare binding source=user.events destination=user.notifications.deleted routing_key=user.deleted
```

## Configuration

The NotificationFunction requires the following configuration in `local.settings.json`:

```json
{
  "Values": {
    "RabbitMQConnection": "amqp://guest:guest@localhost:5672"
  }
}
```

For production/Docker deployments, set this as an environment variable or App Setting.

## Event Flow

1. User operation occurs in the MinimalAPI (Create/Update/Delete)
2. UserService publishes event to `user.events` exchange with appropriate routing key
3. RabbitMQ routes the message to the corresponding notification queue
4. NotificationFunction triggers and processes the event
5. Notification logic executes (placeholder for now - send emails, push notifications, etc.)

