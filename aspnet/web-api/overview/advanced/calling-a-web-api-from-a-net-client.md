---
uid: web-api/overview/advanced/calling-a-web-api-from-a-net-client
title: Call a Web API From a .NET Client (C#) - ASP.NET 4.x
author: Rick-Anderson
description: This tutorial shows how to call a web API from a .NET 4.x application.
ms.author: riande
ms.date: 11/24/2017
msc.legacyurl: /web-api/overview/advanced/calling-a-web-api-from-a-net-client
msc.type: authoredcontent
ms.custom: contperf-fy21q3
---
# Call a Web API From a .NET Client (C#)


This content is for a previous version of .NET. New development should use [ASP.NET Core](/aspnet/core/introduction-to-aspnet-core). For more information on using ASP.NET Core Web API, see:

* [Tutorial: Create a web API with ASP.NET Core](/aspnet/core/tutorials/first-web-api)
* [Call an HTTP endpoint from a .NET client](/dotnet/csharp/tutorials/console-webapiclient)

[Download Completed Project](https://github.com/dotnet/AspNetDocs/tree/main/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client/sample). 

This tutorial shows how to call a web API from a .NET application, using [System.Net.Http.HttpClient.](https://msdn.microsoft.com/library/system.net.http.httpclient(v=vs.110).aspx)

In this tutorial, a client app is written that consumes the following web API:

| Action | HTTP method | Relative URI |
| --- | --- | --- |
| Get a product by ID | GET | /api/products/*id* |
| Create a new product | POST | /api/products |
| Update a product | PUT | /api/products/*id* |
| Delete a product | DELETE | /api/products/*id* |

To learn how to implement this API with ASP.NET Web API, see [Creating a Web API that Supports CRUD Operations](xref:web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api
).

For simplicity, the client application in this tutorial is a Windows console application. **HttpClient** is also supported for Windows Phone and Windows Store apps. For more information, see [Writing Web API Client Code for Multiple Platforms Using Portable Libraries](https://devblogs.microsoft.com/aspnet/writing-web-api-client-code-for-multiple-platforms-using-portable-libraries/)

**NOTE:** If you pass base URLs and relative URIs as hard-coded values, be mindful of the rules for utilizing the `HttpClient` API. The `HttpClient.BaseAddress` property should be set to an address with a trailing forward slash (`/`). For example, when passing hard-coded resource URIs to the `HttpClient.GetAsync` method, don't include a leading forward slash. To get a `Product` by ID:

1. Set `client.BaseAddress = new Uri("https://localhost:5001/");`
1. Request a `Product`. For example, `client.GetAsync<Product>("api/products/4");`.

<a id="CreateConsoleApp"></a>
## Create the Console Application

In Visual Studio, create a new Windows console app named **HttpClientSample** and paste in the following code:

[!code-csharp[Main](calling-a-web-api-from-a-net-client/sample/client/Program.cs?name=snippet_all)]

The preceding code is the complete client app.

`RunAsync` runs and blocks until it completes. Most **HttpClient** methods are async, because they perform network I/O. All of the async tasks are done inside `RunAsync`. Normally an app doesn't block the main thread, but this app doesn't allow any interaction.

[!code-csharp[Main](calling-a-web-api-from-a-net-client/sample/client/Program.cs?name=snippet_run)]

<a id="InstallClientLib"></a>
## Install the Web API Client Libraries

Use NuGet Package Manager to install the Web API Client Libraries package.

From the **Tools** menu, select **NuGet Package Manager** > **Package Manager Console**. In the Package Manager Console (PMC), type the following command:

`Install-Package Microsoft.AspNet.WebApi.Client`

The preceding command adds the following NuGet packages to the project:

* Microsoft.AspNet.WebApi.Client
* Newtonsoft.Json

Newtonsoft.Json (also known as Json.NET) is a popular high-performance JSON framework for .NET.

<a id="AddModelClass"></a>
## Add a Model Class

Examine the `Product` class:

[!code-csharp[Main](calling-a-web-api-from-a-net-client/sample/client/Program.cs?name=snippet_prod)]

This class matches the data model used by the web API. An app can use **HttpClient** to read a `Product` instance from an HTTP response. The app doesn't have to write any deserialization code.

<a id="InitClient"></a>
## Create and Initialize HttpClient

Examine the static **HttpClient** property:

[!code-csharp[Main](calling-a-web-api-from-a-net-client/sample/client/Program.cs?name=snippet_HttpClient)]

**HttpClient** is intended to be instantiated once and reused throughout the life of an application. The following conditions can result in **SocketException** errors:

* Creating a new **HttpClient** instance per request.
* Server under heavy load.

Creating a new **HttpClient** instance per request can exhaust the available sockets.

The following code initializes the **HttpClient** instance:

[!code-csharp[Main](calling-a-web-api-from-a-net-client/sample/client/Program.cs?name=snippet5)]

The preceding code:

* Sets the base URI for HTTP requests. Change the port number to the port used in the server app. The app won't work unless port for the server app is used.
* Sets the Accept header to "application/json". Setting this header tells the server to send data in JSON format.

<a id="GettingResource"></a>
## Send a GET request to retrieve a resource

The following code sends a GET request for a product:

[!code-csharp[Main](calling-a-web-api-from-a-net-client/sample/client/Program.cs?name=snippet_GetProductAsync)]

The **GetAsync** method sends the HTTP GET request. When the method completes, it returns an **HttpResponseMessage** that contains the HTTP response. If the status code in the response is a success code, the response body contains the JSON representation of a product. Call **ReadAsAsync** to deserialize the JSON payload to a `Product` instance. The **ReadAsAsync** method is asynchronous because the response body can be arbitrarily large.

**HttpClient** does not throw an exception when the HTTP response contains an error code. Instead, the **IsSuccessStatusCode** property is **false** if the status is an error code. If you prefer to treat HTTP error codes as exceptions, call [HttpResponseMessage.EnsureSuccessStatusCode](https://msdn.microsoft.com/library/system.net.http.httpresponsemessage.ensuresuccessstatuscode(v=vs.110).aspx) on the response object. `EnsureSuccessStatusCode` throws an exception if the status code falls outside the range 200&ndash;299. Note that **HttpClient** can throw exceptions for other reasons &mdash; for example, if the request times out.

<a id="MediaTypeFormatters"></a>
### Media-Type Formatters to Deserialize

When **ReadAsAsync** is called with no parameters, it uses a default set of *media formatters* to read the response body. The default formatters support JSON, XML, and Form-url-encoded data.

Instead of using the default formatters, you can provide a list of formatters to the **ReadAsAsync** method.  Using a list of formatters is useful if you have a custom media-type formatter:

```csharp
var formatters = new List<MediaTypeFormatter>() {
    new MyCustomFormatter(),
    new JsonMediaTypeFormatter(),
    new XmlMediaTypeFormatter()
};
resp.Content.ReadAsAsync<IEnumerable<Product>>(formatters);
```

For more information, see [Media Formatters in ASP.NET Web API 2](../formats-and-model-binding/media-formatters.md)

## Sending a POST Request to Create a Resource

The following code sends a POST request that contains a `Product` instance in JSON format:

[!code-csharp[Main](calling-a-web-api-from-a-net-client/sample/client/Program.cs?name=snippet_CreateProductAsync)]

The **PostAsJsonAsync** method:

* Serializes an object to JSON.
* Sends the JSON payload in a POST request.

If the request succeeds:

* It should return a 201 (Created) response.
* The response should include the URL of the created resources in the Location header.

<a id="PuttingResource"></a>
## Sending a PUT Request to Update a Resource

The following code sends a PUT request to update a product:

[!code-csharp[Main](calling-a-web-api-from-a-net-client/sample/client/Program.cs?name=snippet_UpdateProductAsync)]

The **PutAsJsonAsync** method works like **PostAsJsonAsync**, except that it sends a PUT request instead of POST.

<a id="DeletingResource"></a>
## Sending a DELETE Request to Delete a Resource

The following code sends a DELETE request to delete a product:

[!code-csharp[Main](calling-a-web-api-from-a-net-client/sample/client/Program.cs?name=snippet_DeleteProductAsync)]

Like GET, a DELETE request does not have a request body. You don't need to specify JSON or XML format with DELETE.

## Test the sample

To test the client app:

1. [Download](https://github.com/dotnet/AspNetDocs/tree/main/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client/sample/server) and run the server app. Verify the server app is working. For example, `http://localhost:64195/api/products` should return a list of products.
2. Set the base URI for HTTP requests. Change the port number to the port used in the server app.
    [!code-csharp[Main](calling-a-web-api-from-a-net-client/sample/client/Program.cs?name=snippet5)]

3. Run the client app. The following output is produced:

   ```console
   Created at http://localhost:64195/api/products/4
   Name: Gizmo     Price: 100.0    Category: Widgets
   Updating price...
   Name: Gizmo     Price: 80.0     Category: Widgets
   Deleted (HTTP Status = 204)
   ```
