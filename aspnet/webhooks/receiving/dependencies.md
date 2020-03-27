---
uid: webhooks/receiving/dependencies
title: "ASP.NET WebHooks receiver dependencies | Microsoft Docs"
author: rick-anderson
description: "Receiver dependencies and dependency injection in ASP.NET WebHooks."
ms.author: riande
ms.date: 01/17/2012
ms.assetid: 5125e483-c2bb-435b-8cd1-21d3499bfaaf
---
# ASP.NET WebHooks receiver dependencies

Microsoft ASP.NET WebHooks is designed with dependency injection in mind. Most dependencies in the system can be replaced with alternative implementations using a dependency injection engine.

See [DependencyScopeExtensions](https://github.com/aspnet/aspnetWebHooks/blob/master/src/Microsoft.AspNet.WebHooks.Receivers/Extensions/DependencyScopeExtensions.cs) for a list of receiver dependencies. If no dependency has been registered, a default implementation is used. See [ReceiverServices](https://github.com/aspnet/aspnetWebHooks/blob/master/src/Microsoft.AspNet.WebHooks.Receivers/Services/ReceiverServices.cs) for a list of default implementations.
