---
uid: mvc/overview/older-versions/getting-started-with-aspnet-mvc4/adding-a-new-field-to-the-movie-model-and-table
title: "Adding a New Field to the Movie Model and Table | Microsoft Docs"
author: Rick-Anderson
description: "In this tutorial you'll use Entity Framework Code First Migrations to migrate some changes to the model classes so the change is applied to the database."
ms.author: riande
ms.date: 08/28/2012
ms.assetid: 9ef2c4f1-a305-4e0a-9fb8-bfbd9ef331d9
msc.legacyurl: /mvc/overview/older-versions/getting-started-with-aspnet-mvc4/adding-a-new-field-to-the-movie-model-and-table
msc.type: authoredcontent
---
# Adding a New Field to the Movie Model and Table

by [Rick Anderson](https://twitter.com/RickAndMSFT)

> > [!NOTE]
> > An updated version of this tutorial is available [here](../../getting-started/introduction/getting-started.md) that uses ASP.NET MVC 5 and Visual Studio 2013. It's more secure, much simpler to follow and demonstrates more features.

In this section you'll use Entity Framework Code First Migrations to migrate some changes to the model classes so the change is applied to the database.

By default, when you use Entity Framework Code First to automatically create a database, as you did earlier in this tutorial, Code First adds a table to the database to help track whether the schema of the database is in sync with the model classes it was generated from. If they aren't in sync, the Entity Framework throws an error. This makes it easier to track down issues at development time that you might otherwise only find (by obscure errors) at run time.

## Setting up Code First Migrations for Model Changes

If you are using Visual Studio 2012, double click the *Movies.mdf* file from Solution Explorer to open the database tool. Visual Studio Express for Web will show Database Explorer, Visual Studio 2012 will show Server Explorer. If you are using Visual Studio 2010, use SQL Server Object Explorer.

In the database tool (Database Explorer, Server Explorer or SQL Server Object Explorer), right click on `MovieDBContext` and select **Delete** to drop the movies database.

![Screenshot that shows the Server Explorer window. Delete is selected in the Movie D B Context right click menu.](adding-a-new-field-to-the-movie-model-and-table/_static/image1.png)

Navigate back to Solution Explorer. Right click on the *Movies.mdf* file and select **Delete** to remove the movies database.

![Screenshot that shows the Solution Explorer window. Delete is selected in the Movies dot m d f right click menu.](adding-a-new-field-to-the-movie-model-and-table/_static/image2.png)

Build the application to make sure there are no errors.

From the **Tools** menu, click **NuGet Package Manager** and then **Package Manager Console**.

![Add Pack Man](adding-a-new-field-to-the-movie-model-and-table/_static/image3.png)

In the **Package Manager Console** window at the `PM>` prompt enter "Enable-Migrations -ContextTypeName MvcMovie.Models.MovieDBContext".

![Screenshot that shows the Package Manager Console window. The Enable Migrations command is entered.](adding-a-new-field-to-the-movie-model-and-table/_static/image4.png)

The **Enable-Migrations** command (shown above) creates a *Configuration.cs* file in a new *Migrations* folder.

![Screenshot that shows the Solution Explorer window. The Migrations folder and Configuration dot c s file are circled in red.](adding-a-new-field-to-the-movie-model-and-table/_static/image5.png)

Visual Studio opens the *Configuration.cs* file. Replace the `Seed` method in the *Configuration.cs* file with the following code:

[!code-csharp[Main](adding-a-new-field-to-the-movie-model-and-table/samples/sample1.cs)]

Right click on the red squiggly line under `Movie` and select **Resolve** then **using** **MvcMovie.Models;**

![Screenshot that shows Resolve selected in the Movie right click menu.](adding-a-new-field-to-the-movie-model-and-table/_static/image6.png)

Doing so adds the following using statement:

[!code-csharp[Main](adding-a-new-field-to-the-movie-model-and-table/samples/sample2.cs)]

> [!NOTE] 
> 
> Code First Migrations calls the `Seed` method after every migration (that is, calling **update-database** in the Package Manager Console), and this method updates rows that have already been inserted, or inserts them if they don't exist yet.

**Press CTRL-SHIFT-B to build the project.**(The following steps will fail if your don't build at this point.)

The next step is to create a `DbMigration` class for the initial migration. This migration to creates a new database, that's why you deleted the *movie.mdf* file in a previous step.

In the **Package Manager Console** window, enter the command "add-migration Initial" to create the initial migration. The name "Initial" is arbitrary and is used to name the migration file created.

![Screenshot that shows the Package Manager Console window. The paragraph that begins with The Designer Code for this migration file is highlighted.](adding-a-new-field-to-the-movie-model-and-table/_static/image7.png)

Code First Migrations creates another class file in the *Migrations* folder (with the name *{DateStamp}\_Initial.cs* ), and this class contains code that creates the database schema. The migration filename is pre-fixed with a timestamp to help with ordering. Examine the *{DateStamp}\_Initial.cs* file, it contains the instructions to create the Movies table for the Movie DB. When you update the database in the instructions below, this *{DateStamp}\_Initial.cs* file will run and create the DB schema. Then the **Seed** method will run to populate the DB with test data.

In the **Package Manager Console**, enter the command "update-database" to create the database and run the **Seed** method.

![Screenshot that shows the Package Manager Console window. The update database command is entered.](adding-a-new-field-to-the-movie-model-and-table/_static/image8.png)

If you get an error that indicates a table already exists and can't be created, it is probably because you ran the application after you deleted the database and before you executed `update-database`. In that case, delete the *Movies.mdf* file again and retry the `update-database` command. If you still get an error, delete the migrations folder and contents then start with the instructions at the top of this page (that is delete the *Movies.mdf* file then proceed to Enable-Migrations).

Run the application and navigate to the */Movies* URL. The seed data is displayed.

![Screenshot that shows the M V C Movie Index page with a list of four movies.](adding-a-new-field-to-the-movie-model-and-table/_static/image9.png)

## Adding a Rating Property to the Movie Model

Start by adding a new `Rating` property to the existing `Movie` class. Open the *Models\Movie.cs* file and add the `Rating` property like this one:

[!code-csharp[Main](adding-a-new-field-to-the-movie-model-and-table/samples/sample3.cs)]

The complete `Movie` class now looks like the following code:

[!code-csharp[Main](adding-a-new-field-to-the-movie-model-and-table/samples/sample4.cs?highlight=8)]

Build the application using the **Build** &gt;**Build Movie** menu command or by pressing CTRL-SHIFT-B.

Now that you've updated the `Model` class, you also need to update the *\Views\Movies\Index.cshtml* and *\Views\Movies\Create.cshtml* view templates in order to display the new `Rating` property in the browser view.

Open the<em>\Views\Movies\Index.cshtml</em> file and add a `<th>Rating</th>` column heading just after the <strong>Price</strong> column. Then add a `<td>` column near the end of the template to render the `@item.Rating` value. Below is what the updated <em>Index.cshtml</em> view template looks like:

[!code-cshtml[Main](adding-a-new-field-to-the-movie-model-and-table/samples/sample5.cshtml?highlight=26-28,46-48)]

Next, open the *\Views\Movies\Create.cshtml* file and add the following markup near the end of the form. This renders a text box so that you can specify a rating when a new movie is created.

[!code-cshtml[Main](adding-a-new-field-to-the-movie-model-and-table/samples/sample6.cshtml)]

You've now updated the application code to support the new `Rating` property.

Now run the application and navigate to the */Movies* URL. When you do this, though, you'll see one of the following errors:

![Screenshot that shows the error Invalid Operation Exception was unhandled by user code.](adding-a-new-field-to-the-movie-model-and-table/_static/image10.png)

![Screenshot that shows the browser window with an error that states Server Error in Application.](adding-a-new-field-to-the-movie-model-and-table/_static/image11.png)

You're seeing this error because the updated `Movie` model class in the application is now different than the schema of the `Movie` table of the existing database. (There's no `Rating` column in the database table.)

There are a few approaches to resolving the error:

1. Have the Entity Framework automatically drop and re-create the database based on the new model class schema. This approach is very convenient when doing active development on a test database; it allows you to quickly evolve the model and database schema together. The downside, though, is that you lose existing data in the database — so you *don't* want to use this approach on a production database! Using an initializer to automatically seed a database with test data is often a productive way to develope an application. For more information on Entity Framework database initializers, see Tom Dykstra's [ASP.NET MVC/Entity Framework tutorial](../../getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application.md).
2. Explicitly modify the schema of the existing database so that it matches the model classes. The advantage of this approach is that you keep your data. You can make this change either manually or by creating a database change script.
3. Use Code First Migrations to update the database schema.

For this tutorial, we'll use Code First Migrations.

Update the Seed method so that it provides a value for the new column. Open Migrations\Configuration.cs file and add a Rating field to each Movie object.

[!code-csharp[Main](adding-a-new-field-to-the-movie-model-and-table/samples/sample7.cs?highlight=6)]

Build the solution, and then open the **Package Manager Console** window and enter the following command:

`add-migration AddRatingMig`

The `add-migration` command tells the migration framework to examine the current movie model with the current movie DB schema and create the necessary code to migrate the DB to the new model. The AddRatingMig is arbitrary and is used to name the migration file. It's helpful to use a meaningful name for the migration step.

When this command finishes, Visual Studio opens the class file that defines the new `DbMigration` derived class, and in the `Up` method you can see the code that creates the new column.

[!code-csharp[Main](adding-a-new-field-to-the-movie-model-and-table/samples/sample8.cs)]

Build the solution, and then enter the "update-database" command in the **Package Manager Console** window.

The following image shows the output in the **Package Manager Console** window (The date stamp prepending AddRatingMig will be different.)

![Screenshot that shows the update database command.](adding-a-new-field-to-the-movie-model-and-table/_static/image12.png)

Re-run the application and navigate to the /Movies URL. You can see the new Rating field.

![Screenshot that shows the M V C Movie Index page with four movies listed.](adding-a-new-field-to-the-movie-model-and-table/_static/image13.png)

Click the **Create New** link to add a new movie. Note that you can add a rating.

![7_CreateRioII](adding-a-new-field-to-the-movie-model-and-table/_static/image14.png)

Click **Create**. The new movie, including the rating, now shows up in the movies listing:

![7_ourNewMovie_SM](adding-a-new-field-to-the-movie-model-and-table/_static/image15.png)

You should also add the `Rating` field to the Edit, Details and SearchIndex view templates.

You could enter the "update-database" command in the **Package Manager Console** window again and no changes would be made, because the schema matches the model.

In this section you saw how you can modify model objects and keep the database in sync with the changes. You also learned a way to populate a newly created database with sample data so you can try out scenarios. Next, let's look at how you can add richer validation logic to the model classes and enable some business rules to be enforced.

> [!div class="step-by-step"]
> [Previous](examining-the-edit-methods-and-edit-view.md)
> [Next](adding-validation-to-the-model.md)
