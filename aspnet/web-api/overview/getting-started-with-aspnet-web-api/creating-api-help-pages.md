---
uid: web-api/overview/getting-started-with-aspnet-web-api/creating-api-help-pages
title: "Creating Help Pages for ASP.NET Web API - ASP.NET 4.x"
author: rick-anderson
description: "This tutorial with code shows how to create help pages for ASP.NET Web API in ASP.NET 4.x."
ms.author: riande
ms.date: 04/01/2013
ms.custom: seoapril2019
ms.assetid: 0150e67b-c50d-4613-83ea-7b4ef8cacc5a
msc.legacyurl: /web-api/overview/getting-started-with-aspnet-web-api/creating-api-help-pages
msc.type: authoredcontent
---
# Creating Help Pages for ASP.NET Web API

This tutorial with code shows how to create help pages for ASP.NET Web API in ASP.NET 4.x.

When you create a web API, it is often useful to create a help page, so that other developers will know how to call your API. You could create all of the documentation manually, but it is better to autogenerate as much as possible. To make this task easier, ASP.NET Web API provides a library for auto-generating help pages at run time.

![Screenshot of the A S P dot NET A P I help page, showing the available A P I products to select from and their descriptions.](creating-api-help-pages/_static/image1.png)

## Creating API Help Pages

Install [ASP.NET and Web Tools 2012.2 Update](https://go.microsoft.com/fwlink/?LinkId=282650). This update integrates help pages into the Web API project template.

Next, create a new ASP.NET MVC 4 project and select the Web API project template. The project template creates an example API controller named `ValuesController`. The template also creates the API help pages. All of the code files for the help page are placed in the Areas folder of the project.

![Screenshot of the Web A P I project template's menu options, circling the area and help page folders.](creating-api-help-pages/_static/image2.png)

When you run the application, the home page contains a link to the API help page. From the home page, the relative path is /Help.

![Screenshot of the home page, pointing to the A P I clickable letters that will open the link to the help page.](creating-api-help-pages/_static/image3.png)

This link brings you to an API summary page.

![Screenshot of the A P I summary help page, showing the different A P I values and their description.](creating-api-help-pages/_static/image4.png)

The MVC view for this page is defined in Areas/HelpPage/Views/Help/Index.cshtml. You can edit this page to modify the layout, introduction, title, styles, and so forth.

The main part of the page is a table of APIs, grouped by controller. The table entries are generated dynamically, using the **IApiExplorer** interface. (I'll talk more about this interface later.) If you add a new API controller, the table is automatically updated at run time.

The "API" column lists the HTTP method and relative URI. The "Description" column contains documentation for each API. Initially, the documentation is just placeholder text. In the next section, I'll show you how to add documentation from XML comments.

Each API has a link to a page with more detailed information, including example request and response bodies.

![Screenshot of one of the A P I selection values, showing the response information and response body formats.](creating-api-help-pages/_static/image5.png)

## Adding Help Pages to an Existing Project

You can add help pages to an existing Web API project by using NuGet Package Manager. This option is useful you start from a different project template than the "Web API" template.

From the **Tools** menu, select **NuGet Package Manager**, and then select **Package Manager Console**. In the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console) window, type one of the following commands:

For a **C#** application: `Install-Package Microsoft.AspNet.WebApi.HelpPage`

For a **Visual Basic** application: `Install-Package Microsoft.AspNet.WebApi.HelpPage.VB`

There are two packages, one for C# and one for Visual Basic. Make sure to use the one that matches your project.

This command installs the necessary assemblies and adds the MVC views for the help pages (located in the Areas/HelpPage folder). You'll need to manually add a link to the Help page. The URI is /Help. To create a link in a razor view, add the following:

[!code-cshtml[Main](creating-api-help-pages/samples/sample1.cshtml)]

Also, make sure to register areas. In the Global.asax file, add the following code to the **Application\_Start** method, if it is not there already:

[!code-csharp[Main](creating-api-help-pages/samples/sample2.cs?highlight=4)]

## Adding API Documentation

By default, the help pages have placeholder strings for documentation. You can use [XML documentation comments](https://msdn.microsoft.com/library/b2s063f7.aspx) to create the documentation. To enable this feature, open the file Areas/HelpPage/App\_Start/HelpPageConfig.cs and uncomment the following line:

[!code-csharp[Main](creating-api-help-pages/samples/sample3.cs)]

Now enable XML documentation. In Solution Explorer, right-click the project and select **Properties**. Select the **Build** page.

![Screenshot of the project's drop-down menu in the solution explorer window, highlighting the build option.](creating-api-help-pages/_static/image6.png)

Under **Output**, check **XML documentation file**. In the edit box, type "App\_Data/XmlDocument.xml".

![Screenshot of the Output dialog box, showing the output path and the option to select the X M L documentation file.](creating-api-help-pages/_static/image7.png)

Next, open the code for the `ValuesController` API controller, which is defined in /Controllers/ValuesController.cs. Add some documentation comments to the controller methods. For example:

[!code-csharp[Main](creating-api-help-pages/samples/sample4.cs)]

> [!NOTE]
> Tip: If you position the caret on the line above the method and type three forward slashes, Visual Studio automatically inserts the XML elements. Then you can fill in the blanks.

Now build and run the application again, and navigate to the help pages. The documentation strings should appear in the API table.

![Screenshot of the A P I table in the help pages, showing the documentation string in the A P I value and description.](creating-api-help-pages/_static/image8.png)

The help page reads the strings from the XML file at run time. (When you deploy the application, make sure to deploy the XML file.)

## Under the Hood

The help pages are built on top of the **ApiExplorer** class, which is part of the Web API framework. The **ApiExplorer** class provides the raw material for creating a help page. For each API, **ApiExplorer** contains an **ApiDescription** that describes the API. For this purpose, an "API" is defined as the combination of HTTP method and relative URI. For example, here are some distinct APIs:

- GET /api/Products
- GET /api/Products/{id}
- POST /api/Products

If a controller action supports multiple HTTP methods, the **ApiExplorer** treats each method as a distinct API.

To hide an API from the **ApiExplorer**, add the **ApiExplorerSettings** attribute to the action and set *IgnoreApi* to true.

[!code-csharp[Main](creating-api-help-pages/samples/sample5.cs)]

You can also add this attribute to the controller, to exclude the entire controller.

The ApiExplorer class gets documentation strings from the **IDocumentationProvider** interface. As you saw earlier, the Help Pages library provides an **IDocumentationProvider** that gets documentation from XML documentation strings. The code is located in /Areas/HelpPage/XmlDocumentationProvider.cs. You can get documentation from another source by writing your own **IDocumentationProvider**. To wire it up, call the **SetDocumentationProvider** extension method, defined in **HelpPageConfigurationExtensions**

**ApiExplorer** automatically calls into the **IDocumentationProvider** interface to get documentation strings for each API. It stores them in the **Documentation** property of the **ApiDescription** and **ApiParameterDescription** objects.

## Next Steps

You aren't limited to the help pages shown here. In fact, **ApiExplorer** is not limited to creating help pages. Yao Huang Lin has written some great blog posts to get you thinking out of the box:

- [Adding a simple Test Client to ASP.NET Web API Help Page](/archive/blogs/yaohuang1/adding-a-simple-test-client-to-asp-net-web-api-help-page)
- [Making ASP.NET Web API Help Page work on self-hosted services](/archive/blogs/yaohuang1/making-asp-net-web-api-help-page-work-on-self-hosted-services)
- [Design-time generation of help page (or client) for ASP.NET Web API](/archive/blogs/yaohuang1/design-time-generation-of-help-page-or-client-for-asp-net-web-api)
- [Advanced Help Page customizations](/archive/blogs/yaohuang1/asp-net-web-api-help-page-part-3-advanced-help-page-customizations)
