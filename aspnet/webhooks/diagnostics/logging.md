---
uid: webhooks/diagnostics/logging
title: "ASP.NET WebHooks logging | Microsoft Docs"
author: rick-anderson
description: "How to do logging in ASP.NET WebHooks."
ms.author: wpickett
ms.date: 01/17/2012
ms.assetid: f71bc442-5f80-481b-a32c-a0ec18dee9d6
---
# ASP.NET WebHooks logging

[!INCLUDE[](~/includes/webhooks-deprecation.md)]

Microsoft ASP.NET WebHooks uses logging as a way of reporting issues and problems. By default logs are written using [System.Diagnostics.Trace](https://msdn.microsoft.com/library/system.diagnostics.trace) where they can be manged using [Trace Listeners](https://msdn.microsoft.com/library/system.diagnostics.tracelistener.aspx) like any other log stream.

When deploying your Web Application as an Azure Web App, the logs are automatically picked up and can be managed together with any other [System.Diagnostics.Trace](https://msdn.microsoft.com/library/system.diagnostics.trace) logging. For details, please see [Enable diagnostics logging for web apps in Azure App Service](/azure/app-service/troubleshoot-diagnostic-logs)

In addition, see [Enable diagnostic logging for apps in Azure App Service](/azure/app-service/troubleshoot-diagnostic-logs#enable-web-server-logging) for instructions on accessing web server logs.

## Redirecting Logs

Instead of writing logs to [System.Diagnostics.Trace](https://msdn.microsoft.com/library/system.diagnostics.trace), it is possible to provide an alternate logging implementation that can log directly to a log manager such as [Log4Net](http://logging.apache.org/log4net/) and [NLog](http://nlog-project.org/). Simply provide an implementation of [ILogger](https://github.com/aspnet/AspNetWebHooks/blob/master/src/Microsoft.AspNet.WebHooks.Common/Diagnostics/ILogger.cs) and register it with a dependency injection engine of your choice and it will get picked up by Microsoft ASP.NET WebHooks. Please see [Dependency Injection in ASP.NET Web API 2](https://www.asp.net/web-api/overview/advanced/dependency-injection) for details.

