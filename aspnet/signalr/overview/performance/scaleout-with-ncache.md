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

In this tutorial, you will use [NCache](https://www.alachisoft.com/ncache) as a SignalR backplane to distribute messages among instances of a SignalR application that are hosted on two different IIS servers. NCache is a scalable distributed caching solution that pools together the CPU and memory resources of the individual NCache servers, also called nodes, which make up the clustered cache. 

## Tutorial overview

The NCache SignalR backplane client interface is exposed using the `UseNCache` extension method of the [IDependencyResolver](https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.idependencyresolver?view=aspnet-mvc-5.2) interface. The `UseNCache` method has two overloads: 

- The first overload takes as parameters the string `cacheName` which is the name of the cache used as the SignalR backplane and the string `eventKey`which is a unique user specified attribute added to NCache on client registration. Each instance of the app will use the same argument for the `eventKey` parameter when calling the `UseNCache` overloaded method.

- The second overload takes as a single parameter an instance of `NCacheScaleoutConfiguration` which extends the `ScaleoutConfiguration`  class. The `NCacheScaleoutConfiguration` object takes as parameters `cacheName` and `eventKey` just as in the first `UseNCache` overloaded method.

For this tutorial, you will use four servers:

- Two servers running Windows on which you will host the SignalR app instances.
- Two servers, both running either Windows or Linux, which will be running NCache that make up the NCache clustered cache to be used as the SignalR backplane.

For both the SignalR servers and the NCache servers, I have used Windows 10 OS.

## Tutorial steps

Here is the sequence of steps to complete this tutorial:

1. Download NCache on both servers from [here](https://www.alachisoft.com/download-ncache.html). You can download the NCache .NET Framework or the .NET Core installation on Windows whereas for Linux, you must use the .NET Core installation.
2. Install NCache using the steps outlined [here](https://www.alachisoft.com/resources/docs/ncache/install-guide/install-ncache-net.html) on both NCache servers.
3. Create a 2-server NCache cluster using the NCache servers running [Windows](https://www.alachisoft.com/resources/docs/ncache/admin-guide/create-new-cache-cluster.html?tabs=windows) or [Linux](https://www.alachisoft.com/resources/docs/ncache/admin-guide/create-new-cache-cluster.html?tabs=linux) and [start it](https://www.alachisoft.com/resources/docs/ncache/admin-guide/start-cache.html).
4. Add these NuGet packages in your app:

   - [Microsoft.AspNet.SignalR](http://nuget.org/packages/Microsoft.AspNet.SignalR) version >= 2.4.0
   - [AspNet.SignalR.NCache](https://www.nuget.org/packages/AspNet.SignalR.NCache/)
   
5. Create a SignalR application or use an example such as the one [here](https://docs.microsoft.com/en-us/aspnet/signalr/overview/getting-started/tutorial-getting-started-with-signalr).
6. Register an instance of the `UseNCache` method in the `Startup.cs` of your application using either of the two method overloads mentioned [here](#tutorial-overview):

   - Overload 1
     ```csharp
     public class Startup
     {
     	public void Configuration(IAppBuilder app)
	 {
		string cache, eventKey;   
		cache    = "myPartitionedCache";
		eventKey = "Chat";  
		//using NCache SignalR              
		GlobalHost.DependencyResolver.UseNCache(cache, eventKey);
		app.MapSignalR();
	 }
      }
      ```
    - Overload 2
      ```csharp
      public class Startup
      {
          public void Configuration(IAppBuilder app)
          {
         	  string cache, eventKey;  
                  cache = "myPartitionedCache";
                  eventKey = "Chat";     

                  var configuration = new NCacheScaleoutConfiguration(cache, eventKey);

                  lobalHost.DependencyResolver.UseNCache(configuration); //using NCache SignalR

                  app.MapSignalR();
           }
       }
       ```





