---
uid: web-api/samples-list
title: Web API Samples List - ASP.NET 4.x
author: rick-anderson
description: ASP.NET Web API samples list for ASP.NET 4.x
ms.author: riande
ms.date: 09/18/2012
ms.custom: seoapril2019
ms.assetid: 8cbd9d7f-7027-4390-b098-cb81a63ecd6f
msc.legacyurl: /web-api/samples-list
msc.type: content
---
# Web API Samples List

## HttpClient Samples

**Bing Translate Sample** | [VS 2012 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/HttpClient/BingTranslateSample)

Shows how to call the [Microsoft Translator service](https://msdn.microsoft.com/library/ff512419.aspx) using the **HttpClient** class. The Microsoft Translator service API requires an OAuth token, which the application obtains by sending a request to the Azure token server for each request to the translator service. The result from the token server is fed into the request sent to the translation service. Before running this sample, you must obtain an [application key from Azure Marketplace](https://msdn.microsoft.com/library/hh454950.aspx) and fill in the information in the AccessTokenMessageHandler sample class.

**Google Maps Sample** | [detailed description](/archive/blogs/henrikn/httpclient-downloading-to-a-local-file) | [VS 2012 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/HttpClient/GoogleMapsSample)

Uses **HttpClient** to download a map of Redmond, WA from [Google Maps API](https://developers.google.com/maps/), saves it as a local file, and opens the default image viewer.

**Twitter Client Sample** | [detailed description](/archive/blogs/henrikn/extending-httpclient-with-oauth-to-access-twitter) | [VS 2012 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/HttpClient/TwitterSample)

Shows how to write a simple Twitter client using **HttpClient**. The sample uses an **HttpMessageHandler** to insert OAuth authentication information into the outgoing **HttpRequestMessage**. The result from Twitter is read using JSON.NET. Before running this sample, you must obtain an [application key from Twitter](https://dev.twitter.com/), and fill in the information in the OAuthMessageHandler sample class.

**World Bank Sample** | [detailed description](https://blogs.msdn.com/b/henrikn/archive/2012/02/16/httpclient-is-here.aspx) | [VS 2010 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/HttpClient/WorldBankSample/Net40) | [VS 2012 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/HttpClient/WorldBankSample/Net45)

Shows how to retrieve data from the World Bank data site, using JSON.NET to parse the result.

## Web API Samples

**Getting Started with ASP.NET Web API** | [VS 2012 source](overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api.md)

Shows how to create a basic web API that supports HTTP GET requests. Contains the source code for the tutorial [Your First ASP.NET Web API](overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api.md).

**ASP.NET Web API JavaScript Scenarios – Comments** | [VS 2012 source](https://code.msdn.microsoft.com/ASPNET-Web-API-JavaScript-d0d64dd7)

Shows how to use ASP.NET Web API to build web APIs that support browser clients and can be easily called using jQuery.

**Contact Manager** | [VS 2010 source](https://code.msdn.microsoft.com/Contact-Manager-Web-API-0e8e373d)

This sample uses ASP.NET Web API to build a simple contact manager application. The application consists of a contact manager web API that is used by an ASP.NET MVC application and a Windows Phone application to display and manage a list of contacts.

**Batching Sample** | [detailed description](http://trocolate.wordpress.com/2012/07/19/mitigate-issue-260-in-batching-scenario/) | [VS 2012 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/WebApi/BatchSample)

Shows how to implement HTTP batching within ASP.NET. The batching consists of putting multiple HTTP requests within a single MIME multipart entity body, which is then sent to the server as an HTTP POST. The requests are processed individually, and the responses are put into another MIME multipart entity body, which is returned to the client.

**Content Controller Sample** | [detailed description](/archive/blogs/henrikn/async-streaming-in-asp-net-web-api) | [VS 2010 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/WebApi/ContentControllerSample/Net40) | [VS 2012 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/WebApi/ContentControllerSample/Net45)

Shows how to read and write request and response entities asynchronously using streams. The sample controller has two actions: a PUT action that reads the request entity body asynchronously and stores it in a local file, and a GET action that returns the contents of the local file.

**Custom Assembly Resolver Sample** | [VS 2012 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/WebApi/CustomAssemblyResolverSample)

Shows how to modify ASP.NET Web API to support discovery of controllers from a dynamically loaded library assembly. The sample implements a custom **IAssembliesResolver** which calls the default implementation and then adds the library assembly to the default results.

**Custom Media Type Formatter Sample** | [detailed description](/archive/blogs/henrikn/recent-asp-net-web-api-updates-april-24) | [VS 2010 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/WebApi/CustomMediaTypeFormatterSample)

Shows how to create a custom media type formatter using the **BufferedMediaTypeFormatter** base class. This base class is intended for formatters which primarily use synchronous read and write operations. In addition to showing the media type formatter, the sample shows how to hook it up by registering it as part of the **HttpConfiguration** for your application. Note that it is also possible to use the **MediaTypeFormatter** base class directly, for formatters which primarily use asynchronous read and write operations.

**Custom Parameter Binding Sample** | [detailed description](https://blogs.msdn.com/b/jmstall/archive/2012/05/11/webapi-parameter-binding-under-the-hood.aspx) | [VS 2010 source](https://github.com/aspnet/samples/blob/master/samples/aspnet/WebApi/CustomParameterBinding)

Shows how to customize the parameter binding process, which is the process that determines how information from a request is bound to action parameters. In this sample, the Home controller has four actions:

1. BindPrincipal shows how to bind an IPrincipal parameter from a custom generic principal, not from an HTTP GET message;
2. BindCustomComplexTypeFromUriOrBody shows how to bind a complex-type parameter, which could come either from the message body or from the request URI of an HTTP POST message;
3. BindCustomComplexTypeFromUriWithRenamedProperty shows how to bind a complex-type parameter with a renamed property which comes from the request URI of an HTTP POST message;
4. PostMultipleParametersFromBody shows how to bind multiple parameters from the body for a POST message;

**File Upload Sample** | [detailed description](/archive/blogs/henrikn/asynchronous-file-upload-using-asp-net-web-api) | [VS 2012 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/FileUploadSample)

Shows how to upload files to an **ApiController** using MIME Multipart File Upload, and how to set up progress notifications with **HttpClient** using **ProgressNotificationHandler**. The controller reads the contents of an HTML file upload asynchronously and writes one or more body parts to a local file. The response contains information about the uploaded file (or files).

**File Upload to Azure Blob Store Sample** | [detailed description](/archive/blogs/yaohuang1/asp-net-web-api-and-azure-blob-storage) | [VS 2012 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/AzureBlobsFileUploadSample)

This sample is similar to the File Upload Sample, but instead of saving the uploaded files on local disk, it asynchronously uploads the files to [Azure Blob Store](/azure/storage/blobs/storage-dotnet-how-to-use-blobs) using [Windows Azure SDK for .NET](https://www.windowsazure.com/develop/net/). It also provides a mechanism for listing the blobs currently present in an [Azure Blob Storage Container](/azure/storage/blobs/storage-dotnet-how-to-use-blobs). You can try out the sample running against **Azure Storage Emulator** that comes with the Azure SDK. If you have an [Azure Storage Account](/azure/storage/blobs/storage-dotnet-how-to-use-blobs), you can run against the real storage service as well.

**Http Message Handler Pipeline Sample** | [detailed description](/archive/blogs/henrikn/httpclient-httpclienthandler-and-webrequesthandler-explained) | [VS 2010 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/HttpMessageHandlerPipelineSample)

Shows how to wire up **HttpMessageHandler** instances on both the client (**HttpClient**) and server (ASP.NET Web API). In the sample, the same handler is used on both the client and server. While it is rare that the exact same handler would run in both places, the object model is the same on client and server side.

**JSON Upload Sample** | [VS 2012 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/JsonUploadSample)

Shows how to upload and download JSON to and from an **ApiController**. The sample uses a minimal **ApiController** and accesses it using **HttpClient**.

**Mashup Sample** | [detailed description](/archive/blogs/henrikn/async-mashups-using-asp-net-web-api) | [VS 2012 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/MashupSample)

Shows how to access multiple remote sites asynchronously from within an **ApiController** action. Each time the action is hit, the requests are performed asynchronously, so that no threads are blocked.

**Memory Tracing Sample** | [detailed description](/archive/blogs/roncain/tracing-in-asp-net-web-api) | [VS 2010 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/MemoryTracingSample)

This sample project creates a Nuget package that will install a custom in-memory trace writer into ASP.NET Web API applications.

**MongoDB Sample** | [detailed description](/archive/blogs/henrikn/using-mongodb-with-asp-net-web-api) | [VS 2012 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/MongoSample)

Shows how to use MongoDB as the persistent store for an **ApiController**, using a repository pattern.

**Response Body Processor Sample** | [VS 2012 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/ResponseEntityProcessorSample)

Shows how to copy a response entity (that is, an HTTP response body) to a local file before it is transmitted to the client, and perform additional processing on that file asynchronously. The sample implements an **HttpMessageHandler** that wraps the response entity with one that both writes itself to the output as normal and to a local file.

**Upload XDocument Sample** | [detailed description](/archive/blogs/henrikn/push-and-pull-streams-using-httpclient) | [VS 2012 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/UploadXDocumentSample)

Shows how to upload an XDocument to an **ApiController** using **PushStreamContent** and **HttpClient**.

**Validation Sample** | [VS 2010 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/ValidationSample)

Shows how you can use validation attributes on your models in ASP.NET WebAPI to validate the contents of the HTTP request. Demonstrates how to mark properties as required, how to use both framework-defined and custom validation attributes to annotate your model, and how to return error responses for invalid model states.

**Web Form Sample** | [detailed description](/archive/blogs/henrikn/using-asp-net-web-api-with-asp-net-web-forms) | [VS 2010 source](https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/WebFormSample)

Shows an ApiController added to a Web Forms project.

**[RestBugs Sample](https://github.com/howarddierking/RestBugs)**

RestBugs is a simple bug tracking application that shows how to use ASP.NET Web API and the new HTTP Client library to create a hypermedia-driven system. The sample includes both client and server implementations, using ASP.NET Web API. The server uses a custom Razor formatter to generate resource representations. The sample also provides a node.js server to illustrate the benefits that come from using a hypermedia design to decouple clients and servers.
