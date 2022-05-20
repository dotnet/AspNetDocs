---
uid: mvc/overview/older-versions/mvc-music-store/mvc-music-store-part-1
title: "Part 1: Overview and File->New Project | Microsoft Docs"
author: jongalloway
description: "This tutorial series details all of the steps taken to build the ASP.NET MVC Music Store sample application. Part 1 covers Overview and File->New Project."
ms.author: riande
ms.date: 04/21/2011
ms.assetid: bd356ca3-5bdb-4067-9dac-c9e9923a86e8
msc.legacyurl: /mvc/overview/older-versions/mvc-music-store/mvc-music-store-part-1
msc.type: authoredcontent
---
# Part 1: Overview and File->New Project

by [Jon Galloway](https://github.com/jongalloway)

> The MVC Music Store is a tutorial application that introduces and explains step-by-step how to use ASP.NET MVC and Visual Studio for web development.  
>   
> The MVC Music Store is a lightweight sample store implementation which sells music albums online, and implements basic site administration, user sign-in, and shopping cart functionality.  
>   
> This tutorial series details all of the steps taken to build the ASP.NET MVC Music Store sample application. Part 1 covers Overview and File-&gt;New Project.

## Overview

The MVC Music Store is a tutorial application that introduces and explains step-by-step how to use ASP.NET MVC and Visual Web Developer for web development. We'll be starting slowly, so beginner level web development experience is okay.

The application we'll be building is a simple music store. There are three main parts to the application: shopping, checkout, and administration.

![Screenshot of the A S P dot Net Music Store overview menu, with options for selecting a genre or from the top picks selections.](mvc-music-store-part-1/_static/image1.jpg)

Visitors can browse Albums by Genre:

![Screenshot of the A S P dot Net music store genre albums selections menu that shows the collection of albums in a given genre.](mvc-music-store-part-1/_static/image2.jpg)

They can view a single album and add it to their cart:

![Screenshot of the album selection window, showing the album's name, genre, artist, and price, with an option to add to cart.](mvc-music-store-part-1/_static/image3.jpg)

They can review their cart, removing any items they no longer want:

![Screenshot of the 'Review your cart' menu, with total price information and options to edit your cart or checkout. ](mvc-music-store-part-1/_static/image4.jpg)

Proceeding to Checkout will prompt them to login or register for a user account.

![Screenshot of the Log On menu bar, requesting the user to enter a user name and password, in addition to the option to click a 'remember me' button.](mvc-music-store-part-1/_static/image1.png)

![Screenshot of the Create New Account menu bar, which requests a user name, email address, and a password that contains six or more characters. The register button is at the bottom of the screen..](mvc-music-store-part-1/_static/image2.png)

After creating an account, they can complete the order by filling out shipping and payment information. To keep things simple, we're running an amazing promotion: everything's free if they enter promotion code "FREE"!

![Screnshot showing the entry options for purchaser's shipping and payment information, with a placeholder for entering promo codes.](mvc-music-store-part-1/_static/image5.jpg)

After ordering, they see a simple confirmation screen:

![Screenshot of the confirmation screen that thanks the customer for their order and provides the order number.](mvc-music-store-part-1/_static/image6.jpg)

In addition to customer-facing pages, we'll also build an administrator section that shows a list of albums from which Administrators can Create, Edit, and Delete albums:

![Screenshot of the administrator section menu that shows a list of owned albums' title, artist, and genre; with the options to edit or delete each one.](mvc-music-store-part-1/_static/image7.jpg)

## 1. File -&gt; New Project

### Installing the software

This tutorial will begin by creating a new ASP.NET MVC 3 project using the free Visual Web Developer 2010 Express (which is free), and then we'll incrementally add features to create a complete functioning application. Along the way, we'll cover database access, form posting scenarios, data validation, using master pages for consistent page layout, using AJAX for page updates and validation, user login, and more.

You can follow along step by step, or you can download the completed application from [MVC-Music-Store](https://github.com/evilDave/MVC-Music-Store).

You can use either Visual Studio 2010 SP1 or Visual Web Developer 2010 Express SP1 (a free version of Visual Studio 2010) to build the application. We'll be using the SQL Server Compact (also free) to host the database. Before you start, make sure you've installed the prerequisites listed below.

- [Visual Studio Web Developer Express SP1 prerequisites]
- [ASP.NET MVC 3 Tools Update]
- [SQL Server Compact 4.0] - including both runtime and tools support

### Creating a new ASP.NET MVC 3 project

We'll start by selecting "New Project" from the File menu in Visual Web Developer. This brings up the New Project dialog.

![Screenshot of the Visual Web Developer file menu that shows the selection and short cut keyboard commands for creating a new project.](mvc-music-store-part-1/_static/image5.png)

We'll select the Visual C# -&gt; Web Templates group on the left, then choose the "ASP.NET MVC 3 Web Application" template in the center column. Name your project MvcMusicStore and press the OK button.

![Screenshot of the New Project Dialog window menu, providing different application option templates.](mvc-music-store-part-1/_static/image8.jpg)

This will display a secondary dialog which allows us to make some MVC specific settings for our project. Select the following:

Project Template - select Empty

View Engine - select Razor

Use HTML5 semantic markup - checked

Verify that your settings are as shown below, then press the OK button.

![Screenshot of a secondary dialog box, allowing user to select different settings for their project.](mvc-music-store-part-1/_static/image9.jpg)

This will create our project. Let's take a look at the folders that have been added to our application in the Solution Explorer on the right side.

![Screenshot of the Solution Explorer window, after project creation, which shows a list of the folders that have been added to the application.](mvc-music-store-part-1/_static/image10.jpg)

The Empty MVC 3 template isn't completely empty – it adds a basic folder structure:

![Zoomed screenshot view of the list of folders mentioned above, highlighting the name of the project that was created.](mvc-music-store-part-1/_static/image6.png)

ASP.NET MVC makes use of some basic naming conventions for folder names:

| **Folder** | **Purpose** |
| --- | --- |
| **/Controllers** | Controllers respond to input from the browser, decide what to do with it, and return response to the user. |
| **/Views** | Views hold our UI templates |
| **/Models** | Models hold and manipulate data |
| **/Content** | This folder holds our images, CSS, and any other static content |
| **/Scripts** | This folder holds our JavaScript files |

These folders are included even in an Empty ASP.NET MVC application because the ASP.NET MVC framework by default uses a "convention over configuration" approach and makes some default assumptions based on folder naming conventions. For instance, controllers look for views in the Views folder by default without you having to explicitly specify this in your code. Sticking with the default conventions reduces the amount of code you need to write, and can also make it easier for other developers to understand your project. We'll explain these conventions more as we build our application.

> [!div class="step-by-step"]
> [Next](mvc-music-store-part-2.md)
