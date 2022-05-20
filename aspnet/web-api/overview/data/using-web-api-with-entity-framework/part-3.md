---
uid: web-api/overview/data/using-web-api-with-entity-framework/part-3
title: Use Code First Migrations to Seed the Database | Microsoft Docs
author: Rick-Anderson
description: Describes how to use the Code First Migrations in Entity Framework to seed the database with test data.
ms.author: riande
ms.date: 06/16/2014
ms.assetid: 76e2013a-65b7-488c-834d-9448ecea378e
msc.legacyurl: /web-api/overview/data/using-web-api-with-entity-framework/part-3
msc.type: authoredcontent
---
# Use Code First Migrations to Seed the Database

[Download Completed Project](https://github.com/MikeWasson/BookService)

In this section, you will use [Code First Migrations](https://msdn.microsoft.com/data/jj591621) in EF to seed the database with test data.

From the **Tools** menu, select **NuGet Package Manager**, then select **Package Manager Console**. In the Package Manager Console window, enter the following command:

[!code-console[Main](part-3/samples/sample1.cmd)]

This command adds a folder named Migrations to your project, plus a code file named Configuration.cs in the Migrations folder.

![Screenshot of the Solution Explorer showing the folder hierarchy with the Configuration dot c s file highlighted in blue.](part-3/_static/image1.png)

Open the Configuration.cs file. Add the following **using** statement.

[!code-csharp[Main](part-3/samples/sample2.cs)]

Then add the following code to the **Configuration.Seed** method:

[!code-csharp[Main](part-3/samples/sample3.cs)]

In the Package Manager Console window, type the following commands:

[!code-console[Main](part-3/samples/sample4.cmd)]

The first command generates code that creates the database, and the second command executes that code. The database is created locally, using [LocalDB](https://msdn.microsoft.com/library/hh510202.aspx).

![Screenshot of the Package Manager Console window with the Enable Migrations, Add Migration Initial, and Update Database lines circled in red.](part-3/_static/image2.png)

## Explore the API (Optional)

Press F5 to run the application in debug mode. Visual Studio starts IIS Express and runs your web app. Visual Studio then launches a browser and opens the app's home page.

When Visual Studio runs a web project, it assigns a port number. In the image below, the port number is 50524. When you run the application, you'll see a different port number.

![Screenshot of the application window launched from Visual Studio in debug mode with the A P I link circled in red and highlighted with a red arrow.](part-3/_static/image3.png)

The home page is implemented using ASP.NET MVC. At the top of the page, there is a link that says "API". This link brings you to an auto-generated help page for the web API. (To learn how this help page is generated, and how you can add your own documentation to the page, see [Creating Help Pages for ASP.NET Web API](../../getting-started-with-aspnet-web-api/creating-api-help-pages.md).) You can click on the help page links to see details about the API, including the request and response format.

![Screenshot of the auto-generated help page showing a list of links to documentation for API features.](part-3/_static/image4.png)

The API enables CRUD operations on the database. The following summarizes the API.

| Authors | Description |
| --- | -- |
| GET api/authors | Get all authors. |
| GET api/authors/{id} | Get an author by ID. |
| POST /api/authors | Create a new author. |
| PUT /api/authors/{id} | Update an existing author. |
| DELETE /api/authors/{id} | Delete an author. |

| Books | Description |
| --- | -- |
| GET /api/books | Get all books. |
| GET /api/books/{id} | Get a book by ID. |
| POST /api/books | Create a new book. |
| PUT /api/books/{id} | Update an existing book. |
| DELETE /api/books/{id} | Delete a book. |

## View the Database (Optional)

When you ran the Update-Database command, EF created the database and called the `Seed` method. When you run the application locally, EF uses [LocalDB](/archive/blogs/sqlexpress/introducing-localdb-an-improved-sql-express). You can view the database in Visual Studio. From the **View** menu, select **SQL Server Object Explorer**.

![Screenshot of the S Q L Server Object Explorer showing the S Q L Server item highlighted in blue and the Add S Q L Server item highlighted in yellow.](part-3/_static/image5.png)

In the **Connect to Server** dialog, in the **Server Name** edit box, type "(localdb)\v11.0". Leave the **Authentication** option as "Windows Authentication". Click **Connect**.

![Screenshot of the Connect to Server dialog showing the text local d b v 11 dot 0 in the Server name field and highlighted in blue.](part-3/_static/image6.png)

Visual Studio connects to LocalDB and shows your existing databases in the SQL Server Object Explorer window. You can expand the nodes to see the tables that EF created.

![Screenshot of the S Q L Server Object Explorer showing the folder hierarchy with the Book Service Context item highlighted in blue.](part-3/_static/image7.png)

To view the data, right-click a table and select **View Data**.

![Screenshot of the S Q L Server Object Explorer showing the d b o dot Books item highlighted in blue and the View Data item highlighted in yellow.](part-3/_static/image8.png)

The following screenshot shows the results for the Books table. Notice that EF populated the database with the seed data, and the table contains the foreign key to the Authors table.

![Screenshot of the Books table showing the database populated with seed data and the table containing the foreign key.](part-3/_static/image9.png)

> [!div class="step-by-step"]
> [Previous](part-2.md)
> [Next](part-4.md)
