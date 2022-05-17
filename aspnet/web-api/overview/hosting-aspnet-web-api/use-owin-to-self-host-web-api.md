---
uid: web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api
title: "Use OWIN to Self-Host ASP.NET Web API - ASP.NET 4.x"
author: rick-anderson
description: "Tutorial with code showing how to host ASP.NET Web API in a console application."
ms.author: riande
ms.date: 07/09/2013
ms.custom: seoapril2019
ms.assetid: a90a04ce-9d07-43ad-8250-8a92fb2bd3d5
msc.legacyurl: /web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api
msc.type: authoredcontent
---
# Use OWIN to Self-Host ASP.NET Web API 

> This tutorial shows how to host ASP.NET Web API in a console application, using OWIN to self-host the Web API framework.
>
> [Open Web Interface for .NET](http://owin.org) (OWIN) defines an abstraction between .NET web servers and web applications. OWIN decouples the web application from the server, which makes OWIN ideal for self-hosting a web application in your own process, outside of IIS.
>
> ## Software versions used in the tutorial
>
>
> - [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) 
> - Web API 5.2.7

> [!NOTE]
> You can find the complete source code for this tutorial at [github.com/aspnet/samples](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/OwinSelfhostSample).

## Create a console application

On the **File** menu,  **New**, then select **Project**. From **Installed**, under **Visual C#**, select **Windows Desktop** and then select **Console App (.Net Framework)**. Name the project "OwinSelfhostSample" and select **OK**.

[![Screenshot of the 'create new project' dialog box, showing the menu options to select Windows Desktop then Console App from the dropdown list.](use-owin-to-self-host-web-api/_static/image7.png)](use-owin-to-self-host-web-api/_static/image7.png)

## Add the Web API and OWIN packages

From the **Tools** menu, select **NuGet Package Manager**, then select **Package Manager Console**. In the Package Manager Console window, enter the following command:

`Install-Package Microsoft.AspNet.WebApi.OwinSelfHost`

This will install the WebAPI OWIN selfhost package and all the required OWIN packages.

[![Screenshot of the package manager console, showing licensing information, followed by P M > at the end, signaling where to type the command.](use-owin-to-self-host-web-api/_static/image4.png)](use-owin-to-self-host-web-api/_static/image3.png)

## Configure Web API for self-host

In Solution Explorer, right-click the project and select **Add** / **Class** to add a new class. Name the class `Startup`.

![Screenshot of the solution explorer dialog box menu, showing the steps to follow for adding a class to the project.](use-owin-to-self-host-web-api/_static/image5.png)

Replace all of the boilerplate code in this file with the following:

[!code-csharp[Main](use-owin-to-self-host-web-api/samples/sample1.cs)]

## Add a Web API controller

Next, add a Web API controller class. In Solution Explorer, right-click the project and select **Add** / **Class** to add a new class. Name the class `ValuesController`.

Replace all of the boilerplate code in this file with the following:

[!code-csharp[Main](use-owin-to-self-host-web-api/samples/sample2.cs)]

## Start the OWIN Host and make a request with HttpClient

Replace all of the boilerplate code in the Program.cs file with the following:

[!code-csharp[Main](use-owin-to-self-host-web-api/samples/sample3.cs)]

## Run the application

To run the application, press F5 in Visual Studio. The output should look like the following:

[!code-console[Main](use-owin-to-self-host-web-api/samples/sample4.cmd)]

![Screenshot of the console, showing the status code and information for the application as it's running.](use-owin-to-self-host-web-api/_static/image6.png)

## Additional resources

[An Overview of Project Katana](../../../aspnet/overview/owin-and-katana/an-overview-of-project-katana.md)

[Host ASP.NET Web API in an Azure Worker Role](host-aspnet-web-api-in-an-azure-worker-role.md)
