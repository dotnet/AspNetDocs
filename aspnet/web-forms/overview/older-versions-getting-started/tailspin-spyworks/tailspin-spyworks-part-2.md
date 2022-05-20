---
uid: web-forms/overview/older-versions-getting-started/tailspin-spyworks/tailspin-spyworks-part-2
title: "Part 2: Data Access Layer | Microsoft Docs"
author: JoeStagner
description: "This tutorial series details all of the steps taken to build the Tailspin Spyworks sample application. Part 2 covers adding the data access layer."
ms.author: riande
ms.date: 07/21/2010
ms.assetid: 5a9d5429-d70b-411c-8474-f42cf7ef8a2b
msc.legacyurl: /web-forms/overview/older-versions-getting-started/tailspin-spyworks/tailspin-spyworks-part-2
msc.type: authoredcontent
---
# Part 2: Data Access Layer

by [Joe Stagner](https://github.com/JoeStagner)

> Tailspin Spyworks demonstrates how extraordinarily simple it is to create powerful, scalable applications for the .NET platform. It shows off how to use the great new features in ASP.NET 4 to build an online store, including shopping, checkout, and administration.
> 
> This tutorial series details all of the steps taken to build the Tailspin Spyworks sample application. Part 2 covers adding the data access layer.

## <a id="_Toc260221668"></a>  Adding the Data Access Layer

Our ecommerce application will depend on two databases.

For customer information we'll use the standard ASP.NET Membership database. For our shopping cart and product catalog we'll implement a SQL Express database as follows.

![Screenshot that shows the S Q L Express database structure.](tailspin-spyworks-part-2/_static/image1.jpg)

Having created the database (Commerce.mdf) in the application's App\_Data folder we can proceed to create our Data Access Layer using the .NET Entity Framework.

We'll create a folder named "Data\_Access" and them right click on that folder and select "Add New Item".

In the "Installed Templates" item and then select "ADO.NET Entity Data Model" enter EDM\_Commerce.edmx as the name and click the "Add" button.

![Screenshot that shows where to select A D O dot NET Entity Data Model.](tailspin-spyworks-part-2/_static/image2.jpg)

Choose "Generate from Database".

![Screenshot that shows where to select Generate from database.](tailspin-spyworks-part-2/_static/image1.png)

![Screenshot that shows the entity connection string.](tailspin-spyworks-part-2/_static/image2.png)

![Screenshot that shows where to select the database objects.](tailspin-spyworks-part-2/_static/image3.png)

![Screenshot that shows the built database structure.](tailspin-spyworks-part-2/_static/image3.jpg)

Save and build.

Now we are ready to add our first feature – a product category menu.

> [!div class="step-by-step"]
> [Previous](tailspin-spyworks-part-1.md)
> [Next](tailspin-spyworks-part-3.md)
