---
uid: mvc/overview/older-versions/getting-started-with-aspnet-mvc3/vb/adding-a-controller
title: "Adding a Controller (VB) | Microsoft Docs"
author: Rick-Anderson
description: "This tutorial will teach you the basics of building an ASP.NET MVC Web application using Microsoft Visual Web Developer 2010 Express Service Pack 1. It will cover all of the MVC concepts (Model, Views, and Controllers) and you will learn how to use them to build an application."
ms.author: riande
ms.date: 01/12/2011
ms.assetid: 741259e1-54ac-4f71-b4e8-2bd5560bb950
msc.legacyurl: /mvc/overview/older-versions/getting-started-with-aspnet-mvc3/vb/adding-a-controller
msc.type: authoredcontent
---
# Adding a Controller (VB)

by [Rick Anderson](https://twitter.com/RickAndMSFT)

> This tutorial will teach you the basics of building an ASP.NET MVC Web application using Microsoft Visual Web Developer 2010 Express Service Pack 1, which is a free version of Microsoft Visual Studio. Before you start, make sure you've installed the prerequisites listed below. You can install all of them by clicking the following link: [Web Platform Installer](https://www.microsoft.com/web/gallery/install.aspx?appid=VWD2010SP1Pack). Alternatively, you can individually install the prerequisites using the following links:
> 
> - [Visual Studio Web Developer Express SP1 prerequisites](https://www.microsoft.com/web/gallery/install.aspx?appid=VWD2010SP1Pack)
> - [ASP.NET MVC 3 Tools Update](https://www.microsoft.com/web/gallery/install.aspx?appsxml=&amp;appid=MVC3)
> - [SQL Server Compact 4.0](https://www.microsoft.com/web/gallery/install.aspx?appid=SQLCE;SQLCEVSTools_4_0)(runtime + tools support)
> 
> If you're using Visual Studio 2010 instead of Visual Web Developer 2010, install the prerequisites by clicking the following link: [Visual Studio 2010 prerequisites](https://www.microsoft.com/web/gallery/install.aspx?appsxml=&amp;appid=VS2010SP1Pack).
> 
> A Visual Web Developer project with VB.NET source code is available to accompany this topic. [Download the VB.NET version](https://code.msdn.microsoft.com/Introduction-to-MVC-3-10d1b098). If you prefer C#, switch to the [C# version](../cs/adding-a-controller.md) of this tutorial.

MVC stands for *model-view-controller*. MVC is a pattern for developing applications such that each part has a separate responsibility:

- Model: The data for your application.
- Views: The template files your application will use to dynamically generate HTML responses.
- Controllers: Classes that handle incoming URL requests to the application, retrieve model data, and then specify view templates that render a response to the client.

We'll be covering all these concepts in this tutorial and show you how to use them to build an application.

Create a new controller by right-clicking the *Controllers* folder in **Solution Explorer** and then selecting **Add Controller**.

[![AddController](adding-a-controller/_static/image2.png "AddController")](adding-a-controller/_static/image1.png)

Name your new controller &quot;HelloWorldController&quot; and click **Add**.

[![2AddEmptyController](adding-a-controller/_static/image4.png "2AddEmptyController")](adding-a-controller/_static/image3.png)

Notice in **Solution Explorer** on the right that a new file has been created for you named *HelloWorldController.cs* and that the file is open in the IDE.

Inside the new `public class HelloWorldController` block, create two new methods that look like the following code. We'll return a string of HTML directly from the controller as an example.

[!code-vb[Main](adding-a-controller/samples/sample1.vb)]

Your controller is named `HelloWorldController` and your new method is named `Index`. Run the application (press F5 or Ctrl+F5). Once your browser has started up, append &quot;HelloWorld&quot; to the path in the address bar. (On my computer, it's `http://localhost:43246/HelloWorld`) Your browser will look like the screenshot below. In the method above, the code returned a string directly. We told the system to just return some HTML, and it did!

![Screenshot that shows the browser with the text This is my default action in the window.](adding-a-controller/_static/image5.png)

ASP.NET MVC invokes different controller classes (and different action methods within them) depending on the incoming URL. The default mapping logic used by ASP.NET MVC uses a format like this to control what code is invoked:

`/[Controller]/[ActionName]/[Parameters]`

The first part of the URL determines the controller class to execute. So */HelloWorld* maps to the `HelloWorldController` class. The second part of the URL determines the action method on the class to execute. So */HelloWorld/Index* would cause the `Index` method of the `HelloWorldController` class to execute. Notice that we only had to visit */HelloWorld* above and the `Index` method was used by default. This is because a method named `Index` is the default method that will be called on a controller if one is not explicitly specified.

Browse to `http://localhost:xxxx/HelloWorld/Welcome`. The `Welcome` method runs and returns the string &quot;This is the Welcome action method...&quot;. The default MVC mapping is `/[Controller]/[ActionName]/[Parameters]`. For this URL, the controller is `HelloWorld` and `Welcome` is the method. We haven't used the `[Parameters]` part of the URL yet.

![Screenshot that shows the browser with the text This is the Welcome action method in the window.](adding-a-controller/_static/image6.png)

Let's modify the example slightly so that we can pass some parameter information in from the URL to the controller (for example, */HelloWorld/Welcome?name=Scott&amp;numtimes=4*). Change your `Welcome` method to include two parameters as shown below. Note that we've used the VB optional parameter feature to indicate that the `numTimes` parameter should default to 1 if no value is passed for that parameter.

[!code-vb[Main](adding-a-controller/samples/sample2.vb)]

Run your application and browse to `http://localhost:xxxx/HelloWorld/Welcome?name=Scott&numtimes=4`**.** You can try different values for `name` and `numtimes`. The system automatically maps the named parameters from your query string in the address bar to parameters in your method.

![Screenshot that shows the browser with the text Hello Scott Num Times is 4 in the window.](adding-a-controller/_static/image7.png)

In both these examples the controller has been doing the VC portion of MVC — that is the view and controller work. The controller is returning HTML directly. Ordinarily we don't want controllers returning HTML directly, since that becomes very cumbersome to code. Instead we'll typically use a separate view template file to help generate the HTML response. Let's look at how we can do this.

> [!div class="step-by-step"]
> [Previous](intro-to-aspnet-mvc-3.md)
> [Next](adding-a-view.md)
