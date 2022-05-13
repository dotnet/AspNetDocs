---
uid: mvc/overview/older-versions/getting-started-with-aspnet-mvc3/vb/improving-the-details-and-delete-methods
title: "Improving the Details and Delete Methods (VB) | Microsoft Docs"
author: Rick-Anderson
description: "This tutorial will teach you the basics of building an ASP.NET MVC Web application using Microsoft Visual Web Developer 2010 Express Service Pack 1. You will learn how to use a few small bits of code to enhance the application and make improvements to the automatically generated Details and Delete methods."
ms.author: riande
ms.date: 01/12/2011
ms.assetid: c5c14ef0-c128-4dc1-8c01-7f0fdb09e411
msc.legacyurl: /mvc/overview/older-versions/getting-started-with-aspnet-mvc3/vb/improving-the-details-and-delete-methods
msc.type: authoredcontent
---
# Improving the Details and Delete Methods (VB)

by [Rick Anderson](https://twitter.com/RickAndMSFT)

> This tutorial will teach you the basics of building an ASP.NET MVC Web application using Microsoft Visual Web Developer 2010 Express Service Pack 1, which is a free version of Microsoft Visual Studio. Before you start, make sure you've installed the prerequisites listed below. You can install all of them by clicking the following link: [Web Platform Installer](https://www.microsoft.com/web/gallery/install.aspx?appid=VWD2010SP1Pack). Alternatively, you can individually install the prerequisites using the following links:
> 
> - [Visual Studio Web Developer Express SP1 prerequisites](https://www.microsoft.com/web/gallery/install.aspx?appid=VWD2010SP1Pack)
> - [ASP.NET MVC 3 Tools Update](https://www.microsoft.com/web/gallery/install.aspx?appsxml=&amp;appid=MVC3)
> - [SQL Server Compact 4.0](https://www.microsoft.com/web/gallery/install.aspx?appid=SQLCE;SQLCEVSTools_4_0)(runtime + tools support)
> 
> If you're using Visual Studio 2010 instead of Visual Web Developer 2010, install the prerequisites by clicking the following link: [Visual Studio 2010 prerequisites](https://www.microsoft.com/web/gallery/install.aspx?appsxml=&amp;appid=VS2010SP1Pack).
> 
> A Visual Web Developer project with VB.NET source code is available to accompany this topic. [Download the VB.NET version](https://code.msdn.microsoft.com/Introduction-to-MVC-3-10d1b098). If you prefer C#, switch to the [C# version](../cs/improving-the-details-and-delete-methods.md) of this tutorial.

In this part of the tutorial, you'll make some improvements to the automatically generated `Details` and `Delete` methods. These changes aren't required, but with just a few small bits of code, you can easily enhance the application.

## Improving the Details and Delete Methods

When you scaffolded the `Movie` controller, ASP.NET MVC generated code that worked great, but that can be made more robust with just a few small changes.

Open the `Movie` controller and modify the `Details` method by returning `HttpNotFound` when a movie isn't found. You should also modify the `Details` method to set a default value for the ID that's passed to it. (You made similar changes to the `Edit` method in [part 6](examining-the-edit-methods-and-edit-view.md) of this tutorial.) However, you must change the return type of the `Details` method from `ViewResult` to `ActionResult`, because the `HttpNotFound` method doesn't return a `ViewResult` object. The following example shows the modified `Details` method.

[!code-vb[Main](improving-the-details-and-delete-methods/samples/sample1.vb)]

Code First makes it easy to search for data using the `Find` method. An important security feature that we built into the method is that the code verifies that the `Find` method has found a movie before the code tries to do anything with it. For example, a hacker could introduce errors into the site by changing the URL created by the links from `http://localhost:xxxx/Movies/Details/1` to something like `http://localhost:xxxx/Movies/Details/12345` (or some other value that doesn't represent an actual movie). If you don't the check for a null movie, this could result in a database error.

Similarly, change the `Delete` and `DeleteConfirmed` methods to specify a default value for the ID parameter and to return `HttpNotFound` when a movie isn't found. The updated `Delete` methods in the `Movie` controller are shown below.

[!code-vb[Main](improving-the-details-and-delete-methods/samples/sample2.vb)]

Note that the `Delete` method doesn't delete the data. Performing a delete operation in response to a GET request (or for that matter, performing an edit operation, create operation, or any other operation that changes data) opens up a security hole. For more information about this, see Stephen Walther's blog entry [ASP.NET MVC Tip #46 — Don't use Delete Links because they create Security Holes](http://stephenwalther.com/blog/archive/2009/01/21/asp.net-mvc-tip-46-ndash-donrsquot-use-delete-links-because.aspx).

The `HttpPost` method that deletes the data is named `DeleteConfirmed` to give the HTTP POST method a unique signature or name. The two method signatures are shown below:

[!code-vb[Main](improving-the-details-and-delete-methods/samples/sample3.vb)]

The common language runtime (CLR) requires overloaded methods to have a unique signature (same name, different list of parameters). However, here you need two Delete methods -- one for GET and one for POST -- that both require the same signature. (They both need to accept a single integer as a parameter.)

To sort this out, you can do a couple of things. One is to give the methods different names. That's what we did in he preceding example. However, this introduces a small problem: ASP.NET maps segments of a URL to action methods by name, and if you rename a method, routing normally wouldn't be able to find that method. The solution is what you see in the example, which is to add the `ActionName("Delete")` attribute to the `DeleteConfirmed` method. This effectively performs mapping for the routing system so that a URL that includes <em>/Delete/</em>for a POST request will find the `DeleteConfirmed` method.

Another way to avoid a problem with methods that have identical names and signatures is to artificially change the signature of the POST method to include an unused parameter. For example, some developers add a parameter type `FormCollection` that is passed to the POST method, and then simply don't use the parameter:

[!code-vb[Main](improving-the-details-and-delete-methods/samples/sample4.vb)]

## Wrapping Up

You now have a complete ASP.NET MVC application that stores data in a SQL Server Compact database. You can create, read, update, delete, and search for movies.

![Screenshot that shows the Search Index page in the M V C Movie App. The page shows a list of four movies.](improving-the-details-and-delete-methods/_static/image1.png)

This basic tutorial got you started making controllers, associating them with views, and passing around hard-coded data. Then you created and designed a data model. Entity Framework Code First created a database from the data model on the fly, and the ASP.NET MVC scaffolding system automatically generated the action methods and views for basic CRUD operations. You then added a search form that let users search the database. You changed the database to include a new column of data, and then updated two pages to create and display this new data. You added validation by marking the data model with attributes from the `DataAnnotations` namespace. The resulting validation runs on the client and on the server.

If you'd like to deploy your application, it's helpful to first test the application on your local IIS 7 server. You can use this [Web Platform Installer](https://www.microsoft.com/web/gallery/install.aspx?appsxml=&amp;appid=ASPNET;) link to enable IIS setting for ASP.NET applications. See the following deployment links:

- [ASP.NET Deployment Content Map](https://msdn.microsoft.com/library/dd394698.aspx)
- [Enabling IIS 7.x](/archive/blogs/rickandy/enabling-iis-7-x-on-windows-7-vista-sp1-windows-2008-windows-2008r2)
- [Web Application Projects Deployment](https://msdn.microsoft.com/library/dd394698.aspx)

I now encourage you to move on to our intermediate-level [Creating an Entity Framework Data Model for an ASP.NET MVC Application](../../../getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application.md) and [MVC Music Store](../../mvc-music-store/mvc-music-store-part-1.md) tutorials, to explore the [ASP.NET articles on MSDN](https://msdn.microsoft.com/library/gg416514(VS.98).aspx), and to check out the many videos and resources at [https://asp.net/mvc](https://asp.net/mvc) to learn even more about ASP.NET MVC! The [ASP.NET MVC forums](https://forums.asp.net/1146.aspx) are a great place to ask questions.

Enjoy!

— Scott Hanselman ([http://hanselman.com](http://hanselman.com) and [@shanselman](http://twitter.com/shanselman) on Twitter) and Rick Anderson [blogs.msdn.com/rickAndy](/archive/blogs/rickAndy/)

> [!div class="step-by-step"]
> [Previous](adding-validation-to-the-model.md)
