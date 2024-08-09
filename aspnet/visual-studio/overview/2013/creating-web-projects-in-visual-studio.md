---
uid: visual-studio/overview/2013/creating-web-projects-in-visual-studio
title: "Creating ASP.NET Web Projects in Visual Studio 2013 | Microsoft Docs"
author: tdykstra
description: "This topic explains the options for creating ASP.NET web projects in Visual Studio 2013 with Update 3 Here are some of the new features for web development c..."
ms.author: riande
ms.date: 12/01/2014
ms.assetid: 61941e64-0c0d-4996-9270-cb8ccfd0cabc
msc.legacyurl: /visual-studio/overview/2013/creating-web-projects-in-visual-studio
msc.type: authoredcontent
---
# Creating ASP.NET Web Projects in Visual Studio 2013

by [Tom Dykstra](https://github.com/tdykstra)

> This topic explains the options for creating ASP.NET web projects in Visual Studio 2013 with Update 3
> 
> Here are some of the new features for web development compared to earlier versions of Visual Studio:
> 
> - A simple UI for creating projects that offer [support for multiple ASP.NET frameworks](#add) (Web Forms, MVC, and Web API).
> - [ASP.NET Identity](#indauth), a new ASP.NET membership system that works the same in all ASP.NET frameworks and works with web hosting software other than IIS.
> - The use of [Bootstrap](#bootstrap) to provide responsive design and theming capabilities.
> - New features for Web Forms that used to be offered only for MVC, such as [automatic test project creation](#testproj) and an [Intranet site template](#winauth).
> 
> For information about how to create web projects for Azure Cloud Services or Azure Mobile Services, see [Get Started with Azure Cloud Services and ASP.NET](https://azure.microsoft.com/documentation/articles/cloud-services-dotnet-get-started/) and [Creating a Leaderboard App with Azure Mobile Services .NET Backend](https://github.com/Huachao/azure-content/blob/master/articles/mobile-services/mobile-services-dotnet-backend-windows-store-dotnet-leaderboard.md).

<a id="prerequisites"></a>
## Prerequisites

This article applies to [Visual Studio 2013](https://go.microsoft.com/fwlink/?LinkId=306566) with [Update 3](https://go.microsoft.com/fwlink/?linkid=397827&amp;clcid=0x409) installed.

<a id="wap"></a>
## Web application projects versus web site projects

ASP.NET gives you a choice between two kinds of web projects: *web application projects* and *web site projects*. We recommend web application projects for new development, and this article applies only to web application projects. For more information, see [Web Application Projects versus Web Site Projects in Visual Studio](https://msdn.microsoft.com/library/dd547590(v=vs.120).aspx) on the MSDN site.

<a id="overview"></a>
## Overview of web application project creation

The following steps show how to create a web project:

1. Click **New Project** in the **Start** page or in the **File** menu.
2. In the **New Project** dialog, click **Web** in the left pane and **ASP.NET Web Application** in the middle pane.

    ![Screenshot showing the New Project window with ASP.NET Web Application selected.](creating-web-projects-in-visual-studio/_static/image1.png)

    You can choose **Cloud** in the left pane to create an [Azure Cloud Service](/azure/cloud-services/cloud-services-how-to-create-deploy), [Azure Mobile Service](https://msdn.microsoft.com/library/windows/apps/dn629482.aspx), or [Azure WebJob](https://github.com/Huachao/azure-content/blob/master/articles/app-service-web/websites-dotnet-deploy-webjobs.md). This topic doesn't cover those templates.
3. In the right pane, click the **Add Application Insights to Project** check box if you want health and usage monitoring for your application. For more information, see [Monitor performance in web applications](https://azure.microsoft.com/documentation/articles/app-insights-web-monitor-performance/).
4. Specify project **Name**, **Location**, and other options, and then click **OK**.

    The **New ASP.NET Project** dialog appears.

    ![Screenshot showing the New ASP.NET Project window.](creating-web-projects-in-visual-studio/_static/image2.png)
5. Click a template.

    ![Screenshot showing the Select a template window with the Web Forms template selected.](creating-web-projects-in-visual-studio/_static/image3.png)
6. If you want to add support for additional frameworks not included in the template, click the appropriate check box. (In the example shown, you could add MVC and/or Web API to a Web Forms project.)

    ![Screenshot showing the New ASP.NET Project window with the Web Forms checkbox selected.](creating-web-projects-in-visual-studio/_static/image4.png)
7. <a id="testproj"></a>If you want to add a unit test project, click **Add unit tests**.

    ![Add unit tests](creating-web-projects-in-visual-studio/_static/image5.png)
8. If you want a different authentication method than what the template provides by default, click **Change Authentication**.

    ![Configure authentication button](creating-web-projects-in-visual-studio/_static/image6.png)

    ![Screenshot showing the Change Authentication window.](creating-web-projects-in-visual-studio/_static/image7.png)

<a id="azurenewproj"></a>
### Create a web app or virtual machine in Azure

Visual Studio includes features that make it easy to work with Azure services for hosting web applications. For example, you can do all of the following right from the Visual Studio IDE:

- Create and manage web apps or virtual machines that make your application available over the Internet.
- View logs created by the application as it runs in the cloud.
- Run in debug mode remotely while the application runs in the cloud.
- View and manage other Azure services such as SQL databases.

You can [create an Azure account](https://www.windowsazure.com/pricing/free-trial/) that includes basic services such as web apps for free, and if you are an MSDN subscriber you can [activate benefits](https://azure.microsoft.com/pricing/member-offers/visual-studio-subscriptions/) that give you monthly credits toward additional Azure services. 

By default the **New ASP.NET Project** dialog box enables you to create a web app or virtual machine for a new web project. If you don't want to create a new web app or virtual machine, clear the **Host in the cloud** check box.

![Create remote resources](creating-web-projects-in-visual-studio/_static/image8.png)

The check box caption might be **Host in the cloud** or **Create remote resources**, and in either case the effect is the same. If you leave the check box selected, Visual Studio creates a web app in Azure App Service by default. You can use the drop-down box to change that to a **Virtual Machine** if you prefer. If you're not already signed in to Azure, you're prompted for Azure credentials. After you sign in, a dialog box enables you to configure the resources that Visual Studio will create for your project. The following illustration shows the dialog for a web app; different options appear if you choose to create a virtual machine.

![Configure Azure App Settings](creating-web-projects-in-visual-studio/_static/image9.png)

For more information about how to use this process for creating Azure resources, see [Get Started with Azure and ASP.NET](/azure/app-service-web/app-service-web-get-started-dotnet).

The remainder of this article provides more information about the available templates and their options. The article also introduces Bootstrap, the layout and theming framework used in the templates.

<a id="vs2013"></a>
## Visual Studio 2013 Web Project Templates

Visual Studio uses templates to create web projects. A project template can create files and folders in the new project, install NuGet packages, and provide sample code for a rudimentary working application. Templates implement the latest web standards and are intended to demonstrate best practices for how to use ASP.NET technologies as well as give you a jump start on creating your own application.

Visual Studio 2013 provides the following choices for web project templates for projects that target .NET 4.5 or later versions of the .NET framework:

- [Empty template](#empty)
- [Web Forms template](#wf)
- [MVC template](#mvc)
- [Web API template](#webapi)
- [Single Page Application template](#spa)
- [Azure Mobile Service template](https://github.com/Huachao/azure-content/blob/master/articles/mobile-services/mobile-services-dotnet-backend-windows-store-dotnet-leaderboard.md)
- [Visual Studio 2012 Templates](#vs2012)

You can also install a Visual Studio extension that provides a [Facebook template](#facebook).

For information about how to create projects that target .NET 4, see [Visual Studio 2012 Templates](#vs2012) later in this topic.

For information about how to create ASP.NET applications for mobile clients, see [Mobile Support in ASP.NET](../../../mobile/overview.md).

<a id="empty"></a>
### Empty Template

The Empty template provides the bare minimum folders and files for an ASP.NET web app, such as a project file (*.csproj* or .*vbproj*) and a *Web.config* file. You can add support for Web Forms, MVC, and/or Web API by using the check boxes under the **Add folders and core references for:** label.

For the Empty template no authentication options are available. Authentication functionality is implemented in sample applications, and the Empty template does not create a sample application.

<a id="wf"></a>
### Web Forms Template

The Web Forms framework provides the following features that let you rapidly build web sites that are rich in UI and data access features:

- A WYSIWYG designer in Visual Studio.
- Server controls that render HTML and that you can customize by setting properties and styles.
- A rich assortment of controls for data access and data display.
- An event model that exposes events which you can program like you would program a client application such as WPF.
- Automatic preservation of state (data) between HTTP requests.

In general, creating a Web Forms application requires less programming effort than creating the same application by using the ASP.NET MVC framework. However, Web Forms is not just for rapid application development. There are many complex commercial applications and frameworks built on top of Web Forms.

Because a Web Forms page and the controls on the page automatically generate much of the markup that's sent to the browser, you don't have the kind of fine-grained control over the HTML that ASP.NET MVC offers. The declarative model for configuring pages and controls minimizes the amount of code you have to write but hides some of the behavior of HTML and HTTP. For example, it's not always possible to specify exactly what markup might be generated by a control.

The Web Forms framework doesn't lend itself as readily as ASP.NET MVC to patterns-based development practices such as [test-driven development](http://en.wikipedia.org/wiki/Test-driven_development), [separation of concerns](http://en.wikipedia.org/wiki/Separation_of_concerns), [inversion of control](http://en.wikipedia.org/wiki/Inversion_of_control), and [dependency injection](http://en.wikipedia.org/wiki/Dependency_injection). If you want to write code factored that way, you can; it's just not as automatic as it is in the ASP.NET MVC framework. Microsoft SharePoint is built on Web Forms MVP.

The Web Forms template creates a sample Web Forms application that uses [Bootstrap](#bootstrap) to provide responsive design and theming features. The following illustration shows the home page.

![Screenshot showing the Web Forms template app home page.](creating-web-projects-in-visual-studio/_static/image10.png)

For more information about Web Forms, see [ASP.NET Web Forms](https://asp.net/web-forms). For information about what the Web Forms template does for you, see [Building a basic Web Forms application using Visual Studio 2013](https://devblogs.microsoft.com/dotnet/building-a-basic-web-forms-application-using-visual-studio-2013/).

<a id="mvc"></a>
### MVC Template

ASP.NET MVC was designed to facilitate patterns-based development practices such as [test-driven development](http://en.wikipedia.org/wiki/Test-driven_development), [separation of concerns](http://en.wikipedia.org/wiki/Separation_of_concerns), [inversion of control](http://en.wikipedia.org/wiki/Inversion_of_control), and [dependency injection](http://en.wikipedia.org/wiki/Dependency_injection). The framework encourages separating the business logic layer of a web application from its presentation layer. By dividing the application into models (M), views (V), and controllers (C), ASP.NET MVC can make it easier to manage complexity in larger applications.

With ASP.NET MVC, you work more directly with HTML and HTTP than in Web Forms. For example, Web Forms can automatically preserve state between HTTP requests, but you have to code that explicitly in MVC. The advantage of the MVC model is that it enables you to take complete control over exactly what your application is doing and how it behaves in the web environment. The disadvantage is that you have to write more code.

MVC was designed to be extensible, providing power developers the ability to customize the framework for their application needs. In addition, the ASP.NET MVC source code is available under an OSI license.

The MVC template creates a sample MVC 5 application that uses [Bootstrap](#bootstrap) to provide responsive design and theming features. The following illustration shows the home page.

![MVC sample application](creating-web-projects-in-visual-studio/_static/image11.png)

For more information about MVC, see [ASP.NET MVC](https://asp.net/mvc). For information about how to select the MVC 4 template, see [Visual Studio 2012 templates](#vs2012) later in this article.

<a id="webapi"></a>
### Web API Template

The Web API template creates a sample web service based on Web API, including API help pages based on MVC.

ASP.NET Web API is a framework that makes it easy to build HTTP services that reach a broad range of clients, including browsers and mobile devices. ASP.NET Web API is an ideal platform for building RESTful services on the .NET Framework.

The Web API template creates a sample web service. The following illustrations show sample help pages.

![Web API help page](creating-web-projects-in-visual-studio/_static/image12.png)

![Web API help page for GET API](creating-web-projects-in-visual-studio/_static/image13.png)

For more information about Web API, see [ASP.NET Web API](https://asp.net/web-api).

<a id="spa"></a>
### Single Page Application Template

The Single Page Application (SPA) template creates a sample application that uses JavaScript, HTML 5, and [KnockoutJS](http://knockoutjs.com/) on the client, and ASP.NET Web API on the server.

The only authentication option for the SPA template is [Individual User Accounts](#indauth).

The following illustration shows the initial state of the sample application that the SPA template builds.

![SPA sample application](creating-web-projects-in-visual-studio/_static/image14.png)

For information about how to create an application by using the SPA template, see [Web API - External Authentication Services](../../../web-api/overview/security/external-authentication-services.md).

For more information about ASP.NET Single Page Applications, and about additional SPA templates that use JavaScript frameworks other than KnockoutJS, see the following resources:

- [ASP.NET Single Page Application](../../../single-page-application/index.md).
- [Understanding Security Features in the SPA Template for VS2013 RC](https://devblogs.microsoft.com/dotnet/understanding-security-features-in-the-spa-template-for-vs2013-rc/)
- [Single-Page Applications: Build Modern, Responsive Web Apps with ASP.NET](https://msdn.microsoft.com/magazine/dn463786.aspx)

<a id="facebook"></a>
### Facebook Template

You can install a [Visual Studio extension that provides a Facebook template](https://go.microsoft.com/fwlink/?LinkID=509965&amp;clcid=0x409). This template creates a sample application that is designed to run inside the Facebook web site. It is based on ASP.NET MVC and uses Web API for real-time update functionality.

No authentication options are available for the Facebook template because Facebook applications run within the Facebook site and rely on Facebook's authentication.

For more information about ASP.NET Facebook applications, see [Updating the MVC Facebook API](https://devblogs.microsoft.com/dotnet/updating-the-mvc-facebook-api/comment-page-2/).

<a id="vs2012"></a>
### Visual Studio 2012 Templates

The Visual Studio 2013 web project creation dialog does not provide access to some templates that were available in Visual Studio 2012. If you want to use one of these templates, you can click the Visual Studio 2012 node in the left pane of the Visual Studio New Project dialog box.

![Visual Studio 2012 templates](creating-web-projects-in-visual-studio/_static/image15.png)

The **Visual Studio 2012** node lets you choose the following web templates that you don't have access to in the default list of templates for Visual Studio 2013:

- ASP.NET MVC 4 Web Application
- ASP.NET Dynamic Data Entities Web Application
- ASP.NET AJAX Server Control
- ASP.NET AJAX Server Control Extender
- ASP.NET Server Control

<a id="bootstrap"></a>
## Bootstrap in the Visual Studio 2013 web project templates

The Visual Studio 2013 project templates use [Bootstrap](http://getbootstrap.com/), a layout and theming framework created by Twitter. Bootstrap uses CSS3 to provide responsive design, which means layouts can dynamically adapt to different browser window sizes. For example, in a wide browser window the home page created by the Web Forms template looks like the following illustration:

![Screenshot showing the Web Forms template app home page in a wide browser window.](creating-web-projects-in-visual-studio/_static/image16.png)

Make the window narrower, and the horizontally arranged columns move into vertical arrangement:

![Bootstrap vertical column arrangement](creating-web-projects-in-visual-studio/_static/image17.png)

Narrow the window a little more, and the horizontal top menu turns into an icon that you can click to expand into a vertically oriented menu:

![Bootstrap menu icon](creating-web-projects-in-visual-studio/_static/image18.png)

![Bootstrap vertical menu](creating-web-projects-in-visual-studio/_static/image19.png)

You can also use Bootstrap's theming feature to easily effect a change in the application's look and feel. For example, you can do the following steps to change the theme.

1. In your browser, go to [http://Bootswatch.com](http://Bootswatch.com), choose a theme, and then click **Download**. (This downloads *bootstrap.min.css* by default; if you want to examine the CSS code, get *bootstrap.css* instead of the minified version.)
2. Copy the contents of the downloaded CSS file.
3. In Visual Studio, create a new **Style Sheet** file named *bootstrap-theme.css* in the *Content* folder and paste the downloaded CSS code into it.
4. Open *App\_Start/Bundle.config* and change *bootstrap.css* to *bootstrap-theme.css*.

Run the project again, and the application has a new look. The following illustration shows the effect of the Amelia theme:

![Bootstrap Amelia theme](creating-web-projects-in-visual-studio/_static/image20.png)

Many Bootstrap themes are available, both free and premium versions. Bootstrap also offers a wide variety of UI components, such as [drop-downs](http://twitter.github.io/bootstrap/components.html#dropdowns), [button groups](http://twitter.github.io/bootstrap/components.html#buttonGroups), and [icons](http://twitter.github.io/bootstrap/base-css.html#images). For more information about Bootstrap, see [the Bootstrap site](http://twitter.github.io/bootstrap/).

If you use the Web Forms designer in Visual Studio, note that the designer doesn't support CSS3, so it doesn't accurately show all the effects of Bootstrap themes or responsive layout changes. However, the Web Forms pages will display correctly when viewed with a browser.

<a id="add"></a>
## Adding Support for Additional Frameworks

When you select a template, the check box for the framework(s) used by the template is automatically selected. For example, if you select the **Web Forms** template, the **Web Forms** check box is selected and you can't clear it.

![Screenshot showing what happens when the Web Forms template is selected in the Select a template window.](creating-web-projects-in-visual-studio/_static/image21.png)

![Screenshot showing the New ASP.NET Project window with the Web Forms checkbox preselected.](creating-web-projects-in-visual-studio/_static/image22.png)

You can select the check box for a framework that isn't included in the template in order to add support for that framework when the project is created. For example, to enable the use of Web Forms *.aspx* pages when you've selected the MVC template, select the **Web Forms** check box. Or to enable MVC when you're using the Web Forms template, click the **MVC** check box. Adding a framework enables design-time as well as run-time support. For example, if you add MVC support to a Web Forms project, you will be able to scaffold controllers and views.

If you combine Web Forms and MVC in a project and enable [Friendly URLs](http://www.hanselman.com/blog/IntroducingASPNETFriendlyUrlsCleanerURLsEasierRoutingAndMobileViewsForASPNETWebForms.aspx) in Web Forms, there might be unexpected routing problems where one URL has multiple possible targets. The routes that are defined first will take precedence. For example, if you have a `Home` controller and a *Home.aspx* page, the `http://contoso.com/home` URL will go to *Home.aspx* if you call the `EnableFriendlyUrls` method before you call the `MapRoute` method in *RouteConfig.cs*, or the same URL will go to the default view for your `Home` controller if you call `MapRoute` before `EnableFriendlyUrls`.

Adding a framework does not add any sample application functionality. For example, if you add Web Forms support when you've selected the MVC template, no *Default.aspx* home page file is created. Only the folders, files, and references required to support the framework are added. For that reason, adding frameworks doesn't change authentication options, which are implemented by code in sample applications created by the templates. For example, if you select the Empty template and add Web Forms or MVC support, the **Configure Authentication** button will still be disabled.

The following sections describe briefly the effect of each check box.

### Add Web Forms Support

Creates empty *App\_Data* and *Models* folders and a *Global.asax* file. These are already created by all templates other than the Empty template, so selecting the Web Forms check box makes no difference for other templates.

The Web Forms template enables Friendly URLs by default, but when you add Web Forms support to other templates by selecting the Web Forms check box Friendly URLs are not automatically enabled.

### Add MVC Support

Installs MVC, Razor, and WebPages NuGet packages, creates empty *App\_Data*, *Controllers*, *Models*, and *Views* folders, creates *App\_Start* folder with *RouteConfig.cs* file, and creates *Global.asax* file.

### Add Web API Support

Installs WebApi and Newtonsoft.Json NuGet packages, creates empty *App\_Data*, *Controllers*, and *Models* folders, creates *App\_Start* folder with *WebApiConfig.cs* file, and creates *Global.asax* file.

<a id="auth"></a>

<a id="nextsteps"></a>
## Next steps

This document has provided some basic help for creating a new ASP.NET web project in Visual Studio 2013. For more information about using for Visual Studio for web development, see [https://www.asp.net/visual-studio/](../../index.md).
