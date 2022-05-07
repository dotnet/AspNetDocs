---
uid: signalr/overview/older-versions/scaleout-with-sql-server
title: SignalR Scaleout with SQL Server (SignalR 1.x) | Microsoft Docs
author: bradygaster
description: Describes the SignalR Scaleout with SQL Server application and provides prerequisites and how to create and run the application.
ms.author: bradyg
ms.date: 05/01/2013
ms.assetid: 1dca7967-8296-444a-9533-837eb284e78c
msc.legacyurl: /signalr/overview/older-versions/scaleout-with-sql-server
msc.type: authoredcontent
---
# SignalR Scaleout with SQL Server (SignalR 1.x)

by [Patrick Fletcher](https://github.com/pfletcher)

[!INCLUDE [Consider ASP.NET Core SignalR](~/includes/signalr/signalr-version-disambiguation.md)]

In this tutorial, you will use SQL Server to distribute messages across a SignalR application that is deployed in two separate IIS instances. You can also run this tutorial on a single test machine, but to get the full effect, you need to deploy the SignalR application to two or more servers. You must also install SQL Server on one of the servers, or on a separate dedicated server. Another option is to run the tutorial using VMs on Azure.

![Diagram of the S Q L Server and its relationship between V Ms, computers, sending queries, and updates to the S Q L Server.](scaleout-with-sql-server/_static/image1.png)

## Prerequisites

Microsoft SQL Server 2005 or later. The backplane supports both desktop and server editions of SQL Server. It does not support SQL Server Compact Edition or Azure SQL Database. (If your application is hosted on Azure, consider the Service Bus backplane instead.)

## Overview

Before we get to the detailed tutorial, here is a quick overview of what you will do.

1. Create a new empty database. The backplane will create the necessary tables in this database.
2. Add these NuGet packages to your application: 

    - [Microsoft.AspNet.SignalR](http://nuget.org/packages/Microsoft.AspNet.SignalR)
    - [Microsoft.AspNet.SignalR.SqlServer](http://nuget.org/packages/Microsoft.AspNet.SignalR.SqlServer)
3. Create a SignalR application.
4. Add the following code to Global.asax to configure the backplane: 

    [!code-csharp[Main](scaleout-with-sql-server/samples/sample1.cs)]

## Configure the Database

Decide whether the application will use Windows authentication or SQL Server authentication to access the database. Regardless, make sure the database user has permissions to log in, create schemas, and create tables.

Create a new database for the backplane to use. You can give the database any name. You don't need to create any tables in the database; the backplane will create the necessary tables.

![Screenshot of the Object Explorer window with the Databases folder being highlighted, revealing its contained sub-folders.](scaleout-with-sql-server/_static/image2.png)

## Enable Service Broker

It is recommended to enable Service Broker for the backplane database. Service Broker provides native support for messaging and queuing in SQL Server, which lets the backplane receive updates more efficiently. (However, the backplane also works without Service Broker.)

To check whether Service Broker is enabled, query the **is\_broker\_enabled** column in the **sys.databases** catalog view.

[!code-sql[Main](scaleout-with-sql-server/samples/sample2.sql)]

![Screenshot of the S Q L Query 1 dot S Q L tab displayed in the Service Broker, showing the Results and Messages tabs.](scaleout-with-sql-server/_static/image3.png)

To enable Service Broker, use the following SQL query:

[!code-sql[Main](scaleout-with-sql-server/samples/sample3.sql)]

> [!NOTE]
> If this query appears to deadlock, make sure there are no applications connected to the DB.

If you have enabled tracing, the traces will also show whether Service Broker is enabled.

## Create a SignalR Application

Create a SignalR application by following either of these tutorials:

- [Getting Started with SignalR](../getting-started/tutorial-getting-started-with-signalr.md)
- [Getting Started with SignalR and MVC 4](tutorial-getting-started-with-signalr-and-mvc-4.md)

Next, we'll modify the chat application to support scaleout with SQL Server. First, add the SignalR.SqlServer NuGet package to your project. In Visual Studio, from the **Tools** menu, select **NuGet Package Manager**, then select **Package Manager Console**. In the Package Manager Console window, enter the following command:

[!code-powershell[Main](scaleout-with-sql-server/samples/sample4.ps1)]

Next, open the Global.asax file. Add the following code to the **Application\_Start** method:

[!code-csharp[Main](scaleout-with-sql-server/samples/sample5.cs)]

## Deploy and Run the Application

Prepare your Windows Server instances to deploy the SignalR application.

Add the IIS role. Include "Application Development" features, including the WebSocket Protocol.

![Screenshot of the Add Roles and Features Wizard screen with the Server Roles and Web Socket Protocol options being highlighted.](scaleout-with-sql-server/_static/image4.png)

Also include the Management Service (listed under "Management Tools").

![Screenshot of the Add Roles and Features Wizard screen with the Server Roles and I I S Management Scripts and Tools options being highlighted.](scaleout-with-sql-server/_static/image5.png)

**Install Web Deploy 3.0.** When you run IIS Manager, it will prompt you to install Microsoft Web Platform, or you can [download the installer](/iis/publish/using-web-deploy/microsoft-web-deploy-v3-readme). In the Platform Installer, search for Web Deploy and install Web Deploy 3.0

![Screenshot of the Web Platform Installer 4 point 5 screen displaying search results with the Web Deploy 3 point 0 option being highlighted.](scaleout-with-sql-server/_static/image6.png)

Check that the Web Management Service is running. If not, start the service. (If you don't see Web Management Service in the list of Windows services, make sure that you installed the Management Service when you added the IIS role.)

Finally, open port 8172 for TCP. This is the port that the Web Deploy tool uses.

Now you are ready to deploy the Visual Studio project from your development machine to the server. In Solution Explorer, right-click the solution and click **Publish**.

For more detailed documentation about web deployment, see [Web Deployment Content Map for Visual Studio and ASP.NET](../../../whitepapers/aspnet-web-deployment-content-map.md).

If you deploy the application to two servers, you can open each instance in a separate browser window and see that they each receive SignalR messages from the other. (Of course, in a production environment, the two servers would sit behind a load balancer.)

![Screenshot of the Internet Explorer browser window showing the Index screen which displays Signal R messages.](scaleout-with-sql-server/_static/image7.png)

After you run the application, you can see that SignalR has automatically created tables in the database:

![Screenshot of the Object Explorer screen with the MIKE dash S Q L server being highlighted and showing its contained folders and servers.](scaleout-with-sql-server/_static/image8.png)

SignalR manages the tables. As long as your application is deployed, don't delete rows, modify the table, and so forth.
