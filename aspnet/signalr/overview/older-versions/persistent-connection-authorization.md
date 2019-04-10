---
uid: signalr/overview/older-versions/persistent-connection-authorization
title: "Authentication and Authorization for SignalR Persistent Connections (SignalR 1.x) | Microsoft Docs"
author: bradygaster
description: "This topic describes how to enforce authorization on a persistent connection. For general information about integrating security into a SignalR application,..."
ms.author: bradyg
ms.date: 10/21/2013
ms.assetid: c34bc627-41af-4c21-a817-e97a19a7f252
msc.legacyurl: /signalr/overview/older-versions/persistent-connection-authorization
msc.type: authoredcontent
---
# Authentication and Authorization for SignalR Persistent Connections (SignalR 1.x)

by [Patrick Fletcher](https://github.com/pfletcher), [Tom FitzMacken](https://github.com/tfitzmac)

[!INCLUDE [Consider ASP.NET Core SignalR](~/includes/signalr/signalr-version-disambiguation.md)]

> This topic describes how to enforce authorization on a persistent connection. For general information about integrating security into a SignalR application, see [Introduction to Security](index.md).

## Enforce authorization

To enforce authorization rules when using a [PersistentConnection](https://msdn.microsoft.com/library/microsoft.aspnet.signalr.persistentconnection(v=vs.111).aspx) you must override the `AuthorizeRequest` method. You cannot use the `Authorize` attribute with persistent connections. The `AuthorizeRequest` method is called by the SignalR Framework before every request to verify that the user is authorized to perform the requested action. The `AuthorizeRequest` method is not called from the client; instead, you authenticate the user through your application's standard authentication mechanism.

The example below shows how to limit requests to authenticated users.

[!code-csharp[Main](persistent-connection-authorization/samples/sample1.cs)]

You can add any customized authorization logic in the AuthorizeRequest method; such as, checking whether a user belongs to a particular role.
