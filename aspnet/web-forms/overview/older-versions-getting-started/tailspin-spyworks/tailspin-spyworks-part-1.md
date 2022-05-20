---
uid: web-forms/overview/older-versions-getting-started/tailspin-spyworks/tailspin-spyworks-part-1
title: "Part 1: File-> New Project | Microsoft Docs"
author: JoeStagner
description: "This tutorial series details all of the steps taken to build the Tailspin Spyworks sample application. Part 1 covers Overview and File/New Project."
ms.author: riande
ms.date: 07/21/2010
ms.assetid: 15d4652b-d5aa-4172-b186-2c7f96ba316d
msc.legacyurl: /web-forms/overview/older-versions-getting-started/tailspin-spyworks/tailspin-spyworks-part-1
msc.type: authoredcontent
---
# Part 1: File-> New Project

by [Joe Stagner](https://github.com/JoeStagner)

> Tailspin Spyworks demonstrates how extraordinarily simple it is to create powerful, scalable applications for the .NET platform. It shows off how to use the great new features in ASP.NET 4 to build an online store, including shopping, checkout, and administration.
> 
> This tutorial series details all of the steps taken to build the Tailspin Spyworks sample application. Part 1 covers Overview and File/New Project.

## <a id="_Toc260221666"></a>  Overview

This tutorial is an introduction to ASP.NET WebForms. We'll be starting slowly, so beginner level web development experience is okay.

The application we'll be building is a simple on-line store.

![Screenshot that shows a simple online store.](tailspin-spyworks-part-1/_static/image1.jpg)

Visitors can browse Products by Category:

![Screenshot that shows that visitors can browse products by category.](tailspin-spyworks-part-1/_static/image2.jpg)

They can view a single product and add it to their cart:

![Screenshot that shows that visitors can view a single product and add it to their cart.](tailspin-spyworks-part-1/_static/image3.jpg)

They can review their cart, removing any items they no longer want:

![Screenshot that shows that visitors can review their cart and remove items they no longer want.](tailspin-spyworks-part-1/_static/image4.jpg)

Proceeding to Checkout will prompt them to

![Screenshot that shows the prompt to sign in at checkout.](tailspin-spyworks-part-1/_static/image5.jpg)

![Screenshot that shows the prompt to create a new account at checkout.](tailspin-spyworks-part-1/_static/image6.jpg)

After ordering, they see a simple confirmation screen:

![Screenshot that shows the confirmation screen.](tailspin-spyworks-part-1/_static/image7.jpg)

We'll begin by creating a new ASP.NET WebForms project in Visual Studio 2010, and we'll incrementally add features to create a complete functioning application. Along the way, we'll cover database access, list and grid views, data update pages, data validation, using master pages for consistent page layout, AJAX, validation, user membership, and more.

You can follow along step by step, or you can download the completed application from [http://tailspinspyworks.codeplex.com/](http://tailspinspyworks.codeplex.com/)

You can use either Visual Studio 2010 or the free Visual Web Developer 2010 from [https://www.microsoft.com/express/Web/](https://www.microsoft.com/express/Web/). To build the application, you can use either SQL Server or the free SQL Server Express to host the database.

## <a id="_Toc260221667"></a>  File / New Project

We'll start by selecting the New Project from the File menu in Visual Studio. This brings up the New Project dialog.

![Screenshot that shows the New Project screen.](tailspin-spyworks-part-1/_static/image8.jpg)

We'll select the Visual C# / Web Templates group on the left, and then choose the "ASP.NET Web Application" template in the center column. Name your project TailspinSpyworks and press the OK button.

![Screenshot that shows where to name your project.](tailspin-spyworks-part-1/_static/image9.jpg)

This will create our project. Let's take a look at the folders that are included in our application in the Solution Explorer on the right side.

![Screenshot that shows the folders that appear when you create a project.](tailspin-spyworks-part-1/_static/image10.jpg)

The Empty Solution isn't completely empty – it adds a basic folder structure:

![Screenshot that shows the basic folder structure.](tailspin-spyworks-part-1/_static/image1.png)

Note the conventions implemented by the ASP.NET 4 default project template.

- The "Account" folder implements a basic user interface for ASP.NET's membership subsystem.
- The "Scripts" folder serves as the repository for client side JavaScript files and the core jQuery .js files are made available by default.
- The "Styles" folder is used to organize our web site visuals (CSS Style Sheets)

When we press F5 to run our application and render the default.aspx page we see the following.

![Screenshot that shows the application when you press F 5.](tailspin-spyworks-part-1/_static/image11.jpg)

Our first application enhancement will be to replace the Style.css file from the default WebForms template with the CSS classes and associated image files that will render the visual asthetics that we want for our Tailspin Spyworks application.

After doing so our default.aspx page renders like this.

![Screenshot that shows how the default page renders.](tailspin-spyworks-part-1/_static/image12.jpg)

Notice the image links at the top right of the page and the menu items that have been added to the master page. Only the "Sign In" and "Account" links point to pages that exist (generated by the default template) and the rest of the pages we will implement as we build our application.

We're also going to relocate the Master Page to the Styles directory. Though this is only a preference it may make things a little easier if we decide to make our application "skinable" in the future.

After doing this we'll need to change the master page references in all the .aspx files generated by the default ASP.NET WebForms pages.

> [!div class="step-by-step"]
> [Next](tailspin-spyworks-part-2.md)
