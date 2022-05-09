---
uid: web-api/overview/data/using-web-api-with-entity-framework/part-10
title: Publish the App to Azure Azure App Service | Microsoft Docs
author: Rick-Anderson
description: This tenth part of this tutorial series will teach you how to publish the application to Azure. The tutorial uses Entity Framework 6 for the data lay...
ms.author: riande
ms.date: 06/16/2014
ms.assetid: 10fd812b-94d6-4967-be97-a31ce9c45e2c
msc.legacyurl: /web-api/overview/data/using-web-api-with-entity-framework/part-10
msc.type: authoredcontent
---
# Publish the App to Azure Azure App Service

[Download Completed Project](https://github.com/MikeWasson/BookService)

As the last step, you will publish the application to Azure. In Solution Explorer, right-click the project and select **Publish**.

![Screenshot of the Solution Explorer window with the project right-clicked and the Publish item on the list highlighted in yellow.](part-10/_static/image1.png)

Clicking **Publish** invokes the **Publish Web** dialog. If you checked **Host in Cloud** when you first created the project, then the connection and settings are already configured. In that case, just click the **Settings** tab and check &quot;Execute Code First Migrations&quot;. (If you didn't check **Host in Cloud** at the beginning, then follow the steps in the [next section](#new-website).)

[![Screenshot of the Publish Web dialog with the Settings tab and the Publish button highlighted in blue.](part-10/_static/image3.png)](part-10/_static/image2.png)

To deploy the app, click **Publish**. You can view the publishing progress in the **Web Publish Activity** window. (From the **View** menu, select **Other Windows**, then select **Web Publish Activity**.)

![Screenshot of the Web Publish Activity window showing the overall status bar and displaying a successful publishing message.](part-10/_static/image4.png)

When Visual Studio finishes deploying the app, the default browser automatically opens to the URL of the deployed website, and the application that you created is now running in the cloud. The URL in the browser address bar shows that the site is being loaded from the Internet.

[![Screenshot of the browser window showing the newly deployed Book Service website and a list of books and authors with links to details.](part-10/_static/image6.png)](part-10/_static/image5.png)

<a id="new-website"></a>
## Deploying to a New Website

If you did not check **Host in Cloud** when you first created the project, you can configure a new web app now. In Solution Explorer, right-click the project and select **Publish**. Select the **Profile** tab and click **Microsoft Azure Websites**. If you aren't currently signed in to Azure, you will be prompted to sign in.

[![Screenshot of the Publish Web dialog with the Profile button highlighted in blue and the BookService website selected from the dropdown list.](part-10/_static/image8.png)](part-10/_static/image7.png)

In the **Existing Websites** dialog, click **New**.

![Screenshot of the Existing Websites dialog showing the Existing Web Sites dropdown list and the New button.](part-10/_static/image9.png)

Enter a site name. Select your Azure subscription and the region. Under **Database server**, select **Create New Server**, or select an existing server. Click **Create**.

[![Screenshot of the Create site on Windows Azure dialog with the Database username and Database password fields highlighted in yellow.](part-10/_static/image11.png)](part-10/_static/image10.png)

Click the **Settings** tab and check &quot;Execute Code First Migrations&quot;. Then click **Publish**.

> [!div class="step-by-step"]
> [Previous](part-9.md)
