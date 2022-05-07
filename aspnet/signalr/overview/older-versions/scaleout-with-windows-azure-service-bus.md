---
uid: signalr/overview/older-versions/scaleout-with-windows-azure-service-bus
title: SignalR Scaleout with Azure Service Bus (SignalR 1.x) | Microsoft Docs
author: bradygaster
description: Describes the SignalR Scaleout with Azure Service Bus application and provides pricing and an overview on how to deploy to Azure.
ms.author: bradyg
ms.date: 05/01/2013
ms.assetid: 501db899-e68c-49ff-81b2-1dc561bfe908
msc.legacyurl: /signalr/overview/older-versions/scaleout-with-windows-azure-service-bus
msc.type: authoredcontent
---
# SignalR Scaleout with Azure Service Bus (SignalR 1.x)

by [Patrick Fletcher](https://github.com/pfletcher)

[!INCLUDE [Consider ASP.NET Core SignalR](~/includes/signalr/signalr-version-disambiguation.md)]

In this tutorial, you will deploy a SignalR application to a Windows Azure Web Role, using the Service Bus backplane to distribute messages to each role instance.

![Diagram that illustrates the relationship between the Service Bus Namespace Topic, Web Roles, and available computers and accounts.](scaleout-with-windows-azure-service-bus/_static/image1.png)

Prerequisites:

- A Windows Azure account.
- The [Windows Azure SDK](https://go.microsoft.com/fwlink/?linkid=254364&amp;clcid=0x409).
- Visual Studio 2012.

The service bus backplane is also compatible with [Service Bus for Windows Server](https://msdn.microsoft.com/library/windowsazure/dn282144.aspx), version 1.1. However, it is not compatible with version 1.0 of Service Bus for Windows Server.

## Pricing

The Service Bus backplane uses topics to send messages. For the latest pricing information, see [Service Bus](https://azure.microsoft.com/pricing/details/service-bus/). At the time of this writing, you can send 1,000,000 messages per month for less than $1. The backplane sends a service bus message for each invocation of a SignalR hub method. There are also some control messages for connections, disconnections, joining or leaving groups, and so forth. In most applications, the majority of the message traffic will be hub method invocations.

## Overview

Before we get to the detailed tutorial, here is a quick overview of what you will do.

1. Use the Windows Azure portal to create a new Service Bus namespace.
2. Add these NuGet packages to your application: 

    - [Microsoft.AspNet.SignalR](http://nuget.org/packages/Microsoft.AspNet.SignalR)
    - [Microsoft.AspNet.SignalR.ServiceBus](http://www.nuget.org/packages/SignalR.WindowsAzureServiceBus)
3. Create a SignalR application.
4. Add the following code to Global.asax to configure the backplane: 

    [!code-csharp[Main](scaleout-with-windows-azure-service-bus/samples/sample1.cs)]

For each application, pick a different value for "YourAppName". Do not use the same value across multiple applications.

## Create the Azure Services

Create a Cloud Service, as described in [How to Create and Deploy a Cloud Service](/azure/cloud-services/cloud-services-how-to-create-deploy). Follow the steps in the section "How to: Create a cloud service using Quick Create". For this tutorial, you do not need to upload a certificate.

![Screenshot of the NEW pane with the Cloud Service option and icon being highlighted in the application as well as a red circle.](scaleout-with-windows-azure-service-bus/_static/image2.png)

Create a new Service Bus namespace, as described in [How to Use Service Bus Topics/Subscriptions](/azure/service-bus-messaging/service-bus-dotnet-how-to-use-topics-subscriptions). Follow the steps in the section "Create a Service Namespace".

![Screenshot of the Add a new namespace screen with entries entered in the Namespace Name and Region fields.](scaleout-with-windows-azure-service-bus/_static/image3.png)

> [!NOTE]
> Make sure to select the same region for the cloud service and the Service Bus namespace.

## Create the Visual Studio Project

Start Visual Studio. From the **File** menu, click **New Project**.

In the **New Project** dialog box, expand **Visual C#**. Under **Installed Templates**, select **Cloud** and then select **Windows Azure Cloud Service**. Keep the default .NET Framework 4.5. Name the application ChatService and click **OK**.

![Screenshot of the New Project screen with the Windows Azure Cloud Service Visual C # option being highlighted.](scaleout-with-windows-azure-service-bus/_static/image4.png)

In the **New Windows Azure Cloud Service** dialog, select ASP.NET MVC 4 Web Role. Click the right-arrow button (**&gt;**) to add the role to your solution.

Hover the mouse over the new role, so the pencil icon visible. Click this icon to rename the role. Name the role "SignalRChat" and click **OK**.

![Screenshot of the New Windows Azure Cloud Service screen with the Signal R Chat option highlighted in the Windows Azure Cloud Service solution pane.](scaleout-with-windows-azure-service-bus/_static/image5.png)

In the **New ASP.NET MVC 4 Project** wizard, select **Internet Application**. Click **OK**. The project wizard creates two projects:

- ChatService: This project is the Windows Azure application. It defines the Azure roles and other configuration options.
- SignalRChat: This project is your ASP.NET MVC 4 project.

## Create the SignalR Chat Application

To create the chat application, follow the steps in the tutorial [Getting Started with SignalR and MVC 4](tutorial-getting-started-with-signalr-and-mvc-4.md).

Use NuGet to install the required libraries. From the **Tools** menu, select **NuGet Package Manager**, then select **Package Manager Console**. In the **Package Manager Console** window, enter the following commands:

[!code-powershell[Main](scaleout-with-windows-azure-service-bus/samples/sample2.ps1)]

Use the `-ProjectName` option to install the packages to the ASP.NET MVC project, rather than the Windows Azure project.

## Configure the Backplane

In your application's Global.asax file, add the following code:

[!code-csharp[Main](scaleout-with-windows-azure-service-bus/samples/sample3.cs)]

Now you need to get your service bus connection string. In the Azure portal, select the service bus namespace that you created and click the Access Key icon.

![Screenshot of the Create, Access Key, and Delete options and icons in the service bus namespace with a focus on the Create option.](scaleout-with-windows-azure-service-bus/_static/image6.png)

Copy the connection string to the clipboard, then paste it into the *connectionString* variable.

![Screenshot of the Access Key Connect to your namespace screen, showing the Connection String, Default Issuer, and Default Key fields.](scaleout-with-windows-azure-service-bus/_static/image7.png)

[!code-csharp[Main](scaleout-with-windows-azure-service-bus/samples/sample4.cs)]

## Deploy to Azure

In Solution Explorer, expand the **Roles** folder inside the ChatService project.

![Screenshot of the Solution Explorer tree showing the Signal R Chat option contained in the Roles folder of the Chat Service project.](scaleout-with-windows-azure-service-bus/_static/image8.png)

Right-click the SignalRChat role and select **Properties**. Select the **Configuration** tab. Under **Instances** select 2. You can also set the VM size to **Extra Small**.

![Screenshot of the Configuration Tab's Instances section, showing a 2 entered into the Instance count field and the V M Size field set to Extra Small.](scaleout-with-windows-azure-service-bus/_static/image9.png)

Save the changes.

In Solution Explorer, right-click the ChatService project. Select **Publish**.

![Screenshot of the Solution Explorer screen's Chat Service project, with a right-click dropdown menu showing the Publish... option.](scaleout-with-windows-azure-service-bus/_static/image10.png)

If this is your first time publishing to Windows Azure, you must download your credentials. In the **Publish** wizard, click "Sign in to download credentials". This will prompt you to sign into the Windows Azure portal and download a publish settings file.

![Screenshot of the Publish Windows Azure Application screen's Sign In tab with the Sign in to download credentials link being highlighted.](scaleout-with-windows-azure-service-bus/_static/image11.png)

Click **Import** and select the publish settings file that you downloaded.

Click **Next**. In the **Publish Settings** dialog, under **Cloud Service**, select the cloud service that you created earlier.

![Screenshot of hte Publish Windows Azure Application screen's Settings tab, showing the Cloud Service field in the Common Settings tab.](scaleout-with-windows-azure-service-bus/_static/image12.png)

Click **Publish**. It can take a few minutes to deploy the application and start the VMs.

Now when you run the chat application, the role instances communicate through Azure Service Bus, using a Service Bus topic. A topic is a message queue that allows multiple subscribers.

The backplane automatically creates the topic and the subscriptions. To see the subscriptions and message activity, open the Azure portal, select the Service Bus namespace, and click on "Topics".

![Screenshot of the selected Cloud Service field now populating in the Azure portal, with the Cloud Service's Name field being highlighted.](scaleout-with-windows-azure-service-bus/_static/image13.png)

It make take a few minutes for the message activity to show up in the dashboard.

![Screenshot of the Azure portal dashboard displaying message activity timeline, showing a blue and purple line to indicate different message histories.](scaleout-with-windows-azure-service-bus/_static/image14.png)

SignalR manages the topic lifetime. As long as your application is deployed, don't try to manually delete topics or change settings on the topic.
