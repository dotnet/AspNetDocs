---
uid: signalr/overview/getting-started/tutorial-getting-started-with-signalr
title: "Tutorial: Real-time chat with SignalR 2 | Microsoft Docs"
author: bradygaster
description: "This tutorial shows how to use SignalR to create a real-time chat application. You add SignalR to an empty ASP.NET web application."
ms.author: bradyg
ms.date: 01/22/2019
ms.assetid: a8b3b778-f009-4369-85c7-e90f9878d8b4
msc.legacyurl: /signalr/overview/getting-started/tutorial-getting-started-with-signalr
msc.type: authoredcontent
ms.topic: tutorial
---

# Tutorial: Real-time chat with SignalR 2

This tutorial shows you how to use SignalR to create a real-time chat application. You add SignalR to an empty ASP.NET web application and create an HTML page to send and display messages.

In this tutorial, you:

> [!div class="checklist"]
> * Set up the project
> * Run the sample
> * Examine the code

[!INCLUDE [Consider ASP.NET Core SignalR](~/includes/signalr/signalr-version-disambiguation.md)]

## Prerequisites

* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) with the **ASP.NET and web development** workload.

## Set up the Project

This section shows how to use Visual Studio 2017 and SignalR 2 to create an empty ASP.NET web application, add SignalR, and create the chat application.

1. In Visual Studio, create an ASP.NET Web Application.

    ![Create web](tutorial-getting-started-with-signalr/_static/image2.png)

1. In the **New ASP.NET Project - SignalRChat** window, leave **Empty** selected and select **OK**.

1. In **Solution Explorer**, right-click the project and select **Add** > **New Item**.

1. In **Add New Item - SignalRChat**, select **Installed** > **Visual C#** > **Web** > **SignalR**  and then select **SignalR Hub Class (v2)**.

1. Name the class *ChatHub* and add it to the project.

    This step creates the *ChatHub.cs* class file and adds a set of script files and assembly references that support SignalR to the project.

1. Replace the code in the new *ChatHub.cs* class file with this code:

    [!code-csharp[Main](tutorial-getting-started-with-signalr/samples/sample1.cs)]

1. In **Solution Explorer**, right-click the project and select **Add** > **New Item**.

1. In **Add New Item - SignalRChat** select **Installed** > **Visual C#** > **Web**  and then select **OWIN Startup Class**.

1. Name the class *Startup* and add it to the project.

1. Replace the default code in *Startup* class with this code:

    [!code-csharp[Main](tutorial-getting-started-with-signalr/samples/sample2.cs)]

1. In **Solution Explorer**, right-click the project and select **Add** > **HTML Page**.

1. Name the new page *index* and select **OK**.

1. In **Solution Explorer**, right-click the HTML page you created and select **Set as Start Page**.

1. Replace the default code in the HTML page with this code:

    [!code-html[Main](tutorial-getting-started-with-signalr/samples/sample3.html)]

1. In **Solution Explorer**, expand **Scripts**.

    Script libraries for jQuery and SignalR are visible in the project.

    > [!IMPORTANT]
    > The package manager may have installed a later version of the SignalR scripts.

1. Check that the script references in the code block correspond to the versions of the script files in the project.

    Script references from the original code block:

    ```html
    <!--Script references. -->
    <!--Reference the jQuery library. -->
    <script src="Scripts/jquery-3.1.1.min.js" ></script>
    <!--Reference the SignalR library. -->
    <script src="Scripts/jquery.signalR-2.2.1.min.js"></script>
    ```

1. If they don't match, update the *.html* file.

1. From the menu bar, select **File** > **Save All**.

## Run the Sample

1. In the toolbar, turn on **Script Debugging** and then select the play button to run the sample in Debug mode.

    ![Enter user name](tutorial-getting-started-with-signalr/_static/image3.png)

1. When the browser opens, enter a name for your chat identity.

1. Copy the URL from the browser, open two other browsers, and paste the URLs into the address bars.

1. In each browser, enter a unique name.

1. Now, add a comment and select **Send**. Repeat that in the other browsers. The comments appear in real-time.

    > [!NOTE]
    > This simple chat application does not maintain the discussion context on the server. The hub broadcasts comments to all current users. Users who join the chat later will see messages added from the time they join.

    See how the chat application runs in three different browsers. When Tom, Anand, and Susan send messages, all browsers update in real time:

    ![All three browsers display the same chat history](tutorial-getting-started-with-signalr/_static/image4.png)

1. In **Solution Explorer**, inspect the **Script Documents** node for the running application. There's a script file named *hubs* that the SignalR library generates at runtime. This file manages the communication between jQuery script and server-side code.

    ![autogenerated hubs script in the Script Documents node](tutorial-getting-started-with-signalr/_static/image5.png)

## Examine the Code

The SignalRChat application demonstrates two basic SignalR development tasks. It shows you how to create a hub. The server uses that hub as the main coordination object. The hub uses the SignalR jQuery library to send and receive messages.

### SignalR Hubs in the ChatHub.cs

In the code sample above, the `ChatHub` class derives from the `Microsoft.AspNet.SignalR.Hub` class. Deriving from the `Hub` class is a useful way to build a SignalR application. You can create public methods on your hub class and then use those methods by calling them from scripts in a web page.

In the chat code, clients call the `ChatHub.Send` method to send a new message. The hub then sends the message to all clients by calling `Clients.All.broadcastMessage`.

The `Send` method demonstrates several hub concepts:

* Declare public methods on a hub so that clients can call them.

* Use the `Microsoft.AspNet.SignalR.Hub.Clients` dynamic property to communicate with all clients connected to this hub.

* Call a function on the client (like the `broadcastMessage` function) to update clients.

    [!code-csharp[Main](tutorial-getting-started-with-signalr/samples/sample4.cs)]

### SignalR and jQuery in the index.html

The *index.html* page in the code sample shows how to use the SignalR jQuery library to communicate with a SignalR hub. The code carries out many important tasks. It declares a proxy to reference the hub, declares a function that the server can call to push content to clients, and it starts a connection to send messages to the hub.

[!code-javascript[Main](tutorial-getting-started-with-signalr/samples/sample5.js)]

> [!NOTE]
> In JavaScript the reference to the server class and its members has to be camelCase. The code sample references the C# *ChatHub* class in JavaScript as `chatHub`.

In this code block, you create a callback function in the script.

[!code-html[Main](tutorial-getting-started-with-signalr/samples/sample6.html)]

The hub class on the server calls this function to push content updates to each client. The two lines that HTML-encode the content before displaying it are optional and show a good way to prevent script injection.

This code opens a connection with the hub.

[!code-javascript[Main](tutorial-getting-started-with-signalr/samples/sample7.js)]

> [!NOTE]
> This approach ensures that the code establishes a connection before the event handler executes.

The code starts the connection and then passes it a function to handle the click event on the **Send** button in the HTML page.

## Get the code

[Download Completed Project](https://code.msdn.microsoft.com/SignalR-Getting-Started-b9d18aa9)

## Additional resources

For more about SignalR, see the following resources:

* [SignalR Project](http://signalr.net)

* [SignalR Github and Samples](https://github.com/SignalR/SignalR)

* [SignalR Wiki](https://github.com/SignalR/SignalR/wiki)

## Next steps

In this tutorial you:

> [!div class="checklist"]
> * Set up the project
> * Ran the sample
> * Examined the code

Advance to the next article to learn how to use SignalR and MVC 5.
> [!div class="nextstepaction"]
> [SignalR 2 and MVC 5](tutorial-getting-started-with-signalr-and-mvc.md)