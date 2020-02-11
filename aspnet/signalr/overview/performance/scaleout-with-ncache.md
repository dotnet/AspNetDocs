---
uid: signalr/overview/performance/scaleout-with-ncache
title: "SignalR Scaleout with NCache | Microsoft Docs"
author: Obaid-Rehman
description: "Software versions used in this topic Visual Studio 2019 .NET 4.5.2 SignalR version 2.4.0"
ms.author: Brad Rehman
ms.date: 02/11/2020
---
# SignalR scaleout with NCache

by [Brad Rehman](https://github.com/Obaid-Rehman)

[!INCLUDE [Consider ASP.NET Core SignalR](~/includes/signalr/signalr-version-disambiguation.md)]

>## Software versions used in this topic
>
>- [Visual Studio 2019](https://visualstudio.microsoft.com/)
>- .NET 4.5.2
>- SignalR version 2.4.0
>
>## Questions and comments
>
>Please leave feedback on how you liked this tutorial and what we could improve in the comments at the bottom of the page. If you have questions that are not directly related to the tutorial, you can post them to the [ASP.NET SignalR forum](https://forums.asp.net/1254.aspx/1?ASP+NET+SignalR) or [StackOverflow.com](http://stackoverflow.com/).

In this tutorial, you will use [NCache](https://www.alachisoft.com/ncache) as a SignalR backplane to distribute messages among instances of a SignalR application that are hosted on two different IIS servers.

NCache is a scalable distributed caching solution that pools together the CPU and memory resources of the individual NCache servers, also called nodes, which make up the clustered cache. As a SignalR backplane, it extends the [IDependencyResolver](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.idependencyresolver?view=aspnet-mvc-5.2) interface with the `UseNCache` method. 

The `UseNCache` method has two overloads: 

- The first overload takes as parameters the string `cacheName` which is the name of the cache used as the SignalR backplane and the string `eventKey`which is a unique user specified attribute added to NCache on client registration. Each instance of the app will use the same value for `eventKey` when calling the `UseNCache` extension method.

- The second overload takes as a single parameter an instance of `NCacheScaleoutConfiguration` which extends the `ScaleoutConfiguration`  class. The `NCacheScaleoutConfiguration` object takes as parameters `cacheName` and `eventKey` just as in the first `UseNCache` overloaded method.



