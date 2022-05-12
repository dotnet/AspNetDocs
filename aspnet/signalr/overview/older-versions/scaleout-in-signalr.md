---
uid: signalr/overview/older-versions/scaleout-in-signalr
title: Introduction to Scaleout in SignalR 1.x | Microsoft Docs
author: bradygaster
description: Describes Scaleout and provides an introduction on the web application through illustration from diagrams.
ms.author: bradyg
ms.date: 04/29/2022
ms.custom: devdivchpfy22
ms.assetid: 3fd9f11c-799b-4001-bd60-1e70cfc61c19
msc.legacyurl: /signalr/overview/older-versions/scaleout-in-signalr
msc.type: authoredcontent
---
# Introduction to Scaleout in SignalR 1.x

by [Patrick Fletcher](https://github.com/pfletcher)

[!INCLUDE [Consider ASP.NET Core SignalR](~/includes/signalr/signalr-version-disambiguation.md)]

In general, there are two ways to scale a web application: *scale up* and *scale out*.

- Scale up means using a larger server (or a larger VM) with more RAM, CPUs, etc.
- Scale out means adding more servers to handle the load.

The problem with scaling up is that you quickly hit a limit on the size of the machine. Beyond that, you need to scale out. However, when you scale out, clients can get routed to different servers. A client that is connected to one server will not receive messages sent from another server.

:::image type="content" source="scaleout-in-signalr/_static/image1.png" alt-text="Screenshot of the issue a client faces when a server is scaled out is that since it is connected to one server it will not receive messages sent from another server.":::

One solution is to forward messages between servers, using a component called a *backplane*. With a backplane enabled, each application instance sends messages to the backplane, and the backplane forwards them to the other application instances. (In electronics, a backplane is a group of parallel connectors. By analogy, a SignalR backplane connects multiple servers.)

:::image type="content" source="scaleout-in-signalr/_static/image2.png" alt-text="Screenshot of the solution to forward messages between servers using a component called a backplane.":::


SignalR currently provides three backplanes:

- **Azure Service Bus**. Service Bus is a messaging infrastructure that allows components to send messages in a loosely coupled way.
- **Redis**. Redis is an in-memory key-value store. Redis supports a publish/subscribe ("pub/sub") pattern for sending messages.
- **SQL Server**. The SQL Server backplane writes messages to SQL tables. The backplane uses Service Broker for efficient messaging. However, it also works if Service Broker is not enabled.

If you deploy your application on Azure, consider using the Azure Service Bus backplane. If you are deploying to your own server farm, consider the SQL Server or Redis backplanes.

The following topics contain step-by-step tutorials for each backplane:

- [SignalR Scaleout with Azure Service Bus](scaleout-with-windows-azure-service-bus.md)
- [SignalR Scaleout with Redis](scaleout-with-redis.md)
- [SignalR Scaleout with SQL Server](scaleout-with-sql-server.md)

## Implementation

In SignalR, every message is sent through a message bus. A message bus implements the [IMessageBus](https://msdn.microsoft.com/library/microsoft.aspnet.signalr.messaging.imessagebus(v=vs.100).aspx) interface, which provides a publish/subscribe abstraction. The backplanes work by replacing the default **IMessageBus** with a bus designed for that backplane. For example, the message bus for Redis is [RedisMessageBus](https://msdn.microsoft.com/library/microsoft.aspnet.signalr.redis.redismessagebus(v=vs.100).aspx), and it uses the Redis [pub/sub](http://redis.io/topics/pubsub) mechanism to send and receive messages.

Each server instance connects to the backplane through the bus. When a message is sent, it goes to the backplane, and the backplane sends it to every server. When a server gets a message from the backplane, it puts the message in its local cache. The server then delivers messages to clients from its local cache.

For each client connection, the client's progress in reading the message stream is tracked using a cursor. (A cursor represents a position in the message stream.) If a client disconnects and then reconnects, it asks the bus for any messages that arrived after the client's cursor value. The same thing happens when a connection uses [long polling](../getting-started/introduction-to-signalr.md#transports). After a long poll request completes, the client opens a new connection and asks for messages that arrived after the cursor.

The cursor mechanism works even if a client is routed to a different server on reconnect. The backplane is aware of all the servers, and it doesn't matter which server a client connects to.

## Limitations

Using a backplane, the maximum message throughput is lower than it is when clients talk directly to a single server node. That's because the backplane forwards every message to every node, so the backplane can become a bottleneck. Whether this limitation is a problem depends on the application. For example, here are some typical SignalR scenarios:

- [Server broadcast](tutorial-server-broadcast-with-aspnet-signalr.md) (e.g., stock ticker): Backplanes work well for this scenario, because the server controls the rate at which messages are sent.
- [Client-to-client](tutorial-getting-started-with-signalr.md) (e.g., chat): In this scenario, the backplane might be a bottleneck if the number of messages scales with the number of clients; that is, if the rate of messages grows proportionally as more clients join.
- [High-frequency realtime](tutorial-high-frequency-realtime-with-signalr.md) (e.g., real-time games): A backplane is not recommended for this scenario.

## Enabling Tracing For SignalR Scaleout

To enable tracing for the backplanes, add the following sections to the web.config file, under the root **configuration** element:

[!code-html[Main](scaleout-in-signalr/samples/sample1.html)]
