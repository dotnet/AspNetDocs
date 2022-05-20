---
uid: signalr/overview/security/introduction-to-security
title: "Introduction to SignalR Security | Microsoft Docs"
author: bradygaster
description: "Describes the security issues you must consider when developing a SignalR application."
ms.author: bradyg
ms.date: 06/10/2014
ms.assetid: ed562717-8591-4936-8e10-c7e63dcb570a
msc.legacyurl: /signalr/overview/security/introduction-to-security
msc.type: authoredcontent
---
# Introduction to SignalR Security

by [Patrick Fletcher](https://github.com/pfletcher), [Tom FitzMacken](https://github.com/tfitzmac)

[!INCLUDE [Consider ASP.NET Core SignalR](~/includes/signalr/signalr-version-disambiguation.md)]

> This article describes the security issues you must consider when developing a SignalR application.
>
> ## Software versions used in this topic
>
>
> - [Visual Studio 2013](https://my.visualstudio.com/Downloads?q=visual%20studio%202013)
> - .NET 4.5
> - SignalR version 2
>
>
>
> ## Previous versions of this topic
>
> For information about earlier versions of SignalR, see [SignalR Older Versions](../older-versions/index.md).
>
> ## Questions and comments
>
> Please leave feedback on how you liked this tutorial and what we could improve in the comments at the bottom of the page. If you have questions that are not directly related to the tutorial, you can post them to the [ASP.NET SignalR forum](https://forums.asp.net/1254.aspx/1?ASP+NET+SignalR) or [StackOverflow.com](http://stackoverflow.com/).

## Overview

This document contains the following sections:

- [SignalR Security Concepts](#concepts)

    - [Authentication and authorization](#authentication)
    - [Connection token](#connectiontoken)
    - [Rejoining groups when reconnecting](#rejoingroup)
- [How SignalR prevents Cross-Site Request Forgery](#csrf)
- [SignalR Security Recommendations](#recommendations)

    - [Secure Socket Layers (SSL) protocol](#ssl)
    - [Do not use groups as a security mechanism](#groupsecurity)
    - [Safely handling input from clients](#input)
    - [Reconciling a change in user status with an active connection](#reconcile)
    - [Automatically generated JavaScript proxy files](#autogen)
    - [Exceptions](#exceptions)

<a id="concepts"></a>

## SignalR Security Concepts

<a id="authentication"></a>

### Authentication and authorization

SignalR does not provide any features for authenticating users. Instead, you integrate the SignalR features into the existing authentication structure for an application. You authenticate users as you would normally in your application, and work with the results of the authentication in your SignalR code. For example, you might authenticate your users with ASP.NET forms authentication, and then in your hub, enforce which users or roles are authorized to call a method. In your hub, you can also pass authentication information, such as user name or whether a user belongs to a role, to the client.

SignalR provides the [Authorize](https://msdn.microsoft.com/library/microsoft.aspnet.signalr.authorizeattribute(v=vs.111).aspx) attribute to specify which users have access to a hub or method. You apply the Authorize attribute to either a hub or particular methods in a hub. Without the Authorize attribute, all public methods on the hub are available to a client that is connected to the hub. For more information about hubs, see [Authentication and Authorization for SignalR Hubs](hub-authorization.md).

You apply the `Authorize` attribute to hubs, but not persistent connections. To enforce authorization rules when using a `PersistentConnection` you must override the `AuthorizeRequest` method. For more information about persistent connections, see [Authentication and Authorization for SignalR Persistent Connections](persistent-connection-authorization.md).

<a id="connectiontoken"></a>

### Connection token

SignalR mitigates the risk of executing malicious commands by validating the identity of the sender. For each request, the client and server pass a connection token which contains the connection id and username for authenticated users. The connection id uniquely identifies each connected client. The server randomly generates the connection id when a new connection is created, and persists that id for the duration of the connection. The authentication mechanism for the web application provides the username. SignalR uses encryption and a digital signature to protect the connection token.

![Diagram that shows an arrow from Client New Connection Request to Server Received Connection Request to Server Response to Client Received Response. The Authentication System generates a Connection Token in the Response and Received Response boxes.](introduction-to-security/_static/image2.png)

For each request, the server validates the contents of the token to ensure that the request is coming from the specified user. The username must correspond to the connection id. By validating both the connection id and the username, SignalR prevents a malicious user from easily impersonating another user. If the server cannot validate the connection token, the request fails.

![Diagram that shows an arrow from Client Request to Server Received Request to Saved Token. Connection Token and Message are in both the Client box and the Server box.](introduction-to-security/_static/image4.png)

Because the connection id is part of the verification process, you should not reveal one user's connection id to other users or store the value on the client, such as in a cookie.

#### Connection tokens vs. other token types

Connection tokens are occasionally flagged by security tools because they appear to be session tokens or authentication tokens, which poses a risk if exposed.

SignalR's connection token isn't an authentication token. It is used to confirm that the user making this request is the same one that created the connection. The connection token is necessary because ASP.NET SignalR allows connections to move between servers. The token associates the connection with a particular user but doesn't assert the identity of the user making the request. For a SignalR request to be properly authenticated, it must have some other token that asserts the identity of the user, such as a cookie or bearer token. However, the connection token itself makes no claim that the request was made by that user, only that the connection ID contained within the token is associated with that user.

Since the connection token provides no authentication claim of its own, it isn't considered a "session" or "authentication" token. Taking a given user's connection token and replaying it in a request authenticated as a different user (or an unauthenticated request) will fail, because the user identity of the request and the identity stored in the token won't match.

<a id="rejoingroup"></a>

### Rejoining groups when reconnecting

By default, the SignalR application will automatically re-assign a user to the appropriate groups when reconnecting from a temporary disruption, such as when a connection is dropped and re-established before the connection times out. When reconnecting, the client passes a group token that includes the connection id and the assigned groups. The group token is digitally signed and encrypted. The client retains the same connection id after a reconnection; therefore, the connection id passed from the reconnected client must match the previous connection id used by the client. This verification prevents a malicious user from passing requests to join unauthorized groups when reconnecting.

However, it's important to note, that the group token does not expire. If a user belonged to a group in the past, but was banned from that group, that user may be able to mimic a group token that includes the prohibited group. If you need to securely manage which users belong to which groups, you need to store that data on the server, such as in a database. Then, add logic to your application that verifies on the server whether a user belongs to a group. For an example of verifying group membership, see [Working with groups](../guide-to-the-api/working-with-groups.md).

Automatically rejoining groups only applies when a connection is reconnected after a temporary disruption. If a user disconnects by navigating away from the application or the application restarts, your application must handle how to add that user to the correct groups. For more information, see [Working with groups](../guide-to-the-api/working-with-groups.md).

<a id="csrf"></a>

## How SignalR prevents Cross-Site Request Forgery

Cross-Site Request Forgery (CSRF) is an attack where a malicious site sends a request to a vulnerable site where the user is currently logged in. SignalR prevents CSRF by making it extremely unlikely for a malicious site to create a valid request for your SignalR application.

### Description of CSRF attack

Here is an example of a CSRF attack:

1. A user logs into www.example.com, using forms authentication.
2. The server authenticates the user. The response from the server includes an authentication cookie.
3. Without logging out, the user visits a malicious web site. This malicious site contains the following HTML form:

    [!code-html[Main](introduction-to-security/samples/sample1.html)]

   Notice that the form action posts to the vulnerable site, not to the malicious site. This is the "cross-site" part of CSRF.
4. The user clicks the submit button. The browser includes the authentication cookie with the request.
5. The request runs on the example.com server with the user's authentication context, and can do anything that an authenticated user is allowed to do.

Although this example requires the user to click the form button, the malicious page could just as easily run a script that sends an AJAX request to your SignalR application. Moreover, using SSL does not prevent a CSRF attack, because the malicious site can send an "https://" request.

Typically, CSRF attacks are possible against web sites that use cookies for authentication, because browsers send all relevant cookies to the destination web site. However, CSRF attacks are not limited to exploiting cookies. For example, Basic and Digest authentication are also vulnerable. After a user logs in with Basic or Digest authentication, the browser automatically sends the credentials until the session ends.

### CSRF mitigations taken by SignalR

SignalR takes the following steps to prevent a malicious site from creating valid requests to your application. SignalR takes these steps by default, you do not need to take any action in your code.

- **Disable cross domain requests**
 SignalR disables cross domain requests to prevent users from calling a SignalR endpoint from an external domain. SignalR considers any request from an external domain to be invalid and blocks the request. We recommend that you keep this default behavior; otherwise, a malicious site could trick users into sending commands to your site. If you need to use cross domain requests, see     [How to establish a cross-domain connection](../guide-to-the-api/hubs-api-guide-javascript-client.md#crossdomain) .
- **Pass connection token in query string, not cookie**
 SignalR passes the connection token as a query string value, instead of as a cookie. Storing the connection token in a cookie is unsafe because the browser can inadvertently forward the connection token when malicious code is encountered. Also, passing the connection token in the query string prevents the connection token from persisting beyond the current connection. Therefore, a malicious user cannot make a request under another user's authentication credentials.
- **Verify connection token**
 As described in the     [Connection token](#connectiontoken) section, the server knows which connection id is associated with each authenticated user. The server does not process any request from a connection id that does not match the user name. It is unlikely a malicious user could guess a valid request because the malicious user would have to know the user name and the current randomly-generated connection id. That connection id becomes invalid as soon as the connection is ended. Anonymous users should not have access to any sensitive information.

<a id="recommendations"></a>

## SignalR Security Recommendations

<a id="ssl"></a>

### Secure Socket Layers (SSL) protocol

The SSL protocol uses encryption to secure the transport of data between a client and server. If your SignalR application transmits sensitive information between the client and server, use SSL for the transport. For more information about setting up SSL, see [How to set up SSL on IIS 7](https://www.iis.net/learn/manage/configuring-security/how-to-set-up-ssl-on-iis).

<a id="groupsecurity"></a>

### Do not use groups as a security mechanism

Groups are a convenient way of collecting related users, but they are not a secure mechanism for limiting access to sensitive information. This is especially true when users can automatically rejoin groups during a reconnect. Instead, consider adding privileged users to a role and limiting access to a hub method to only members of that role. For an example of restricting access based on a role, see [Authentication and Authorization for SignalR Hubs](hub-authorization.md). For an example of checking user access to groups when reconnecting, see [Working with groups](../guide-to-the-api/working-with-groups.md).

<a id="input"></a>

### Safely handling input from clients

To ensure that a malicious user does not send script to other users, you must encode all input from clients that is intended for broadcast to other clients. You should encode messages on the receiving clients rather than the server, because your SignalR application may have many different types of clients. Therefore, HTML-encoding works for a web client, but not for other types of clients. For example, a web client method to display a chat message would safely handle the user name and message by calling the `html()` function.

[!code-html[Main](introduction-to-security/samples/sample2.html?highlight=3-4)]

<a id="reconcile"></a>

### Reconciling a change in user status with an active connection

If a user's authentication status changes while an active connection exists, the user will receive an error that states, "The user identity cannot change during an active SignalR connection." In that case, your application should re-connect to the server to make sure the connection id and username are coordinated. For example, if your application allows the user to log out while an active connection exists, the username for the connection will no longer match the name that is passed in for the next request. You will want to stop the connection before the user logs out, and then restart it.

However, it is important to note that most applications will not need to manually stop and start the connection. If your application redirects users to a separate page after logging out, such as the default behavior in a Web Forms application or MVC application, or refreshes the current page after logging out, the active connection is automatically disconnected and does not require any additional action.

The following example shows how to stop and start a connection when the user status has changed.

[!code-html[Main](introduction-to-security/samples/sample3.html)]

Or, the user's authentication status may change if your site uses sliding expiration with Forms Authentication, and there is no activity to keep the authentication cookie valid. In that case, the user will be logged out and the user name will no longer match the user name in the connection token. You can fix this problem by adding some script that periodically requests a resource on the web server to keep the authentication cookie valid. The following example shows how to request a resource every 30 minutes.

[!code-javascript[Main](introduction-to-security/samples/sample4.js)]

<a id="autogen"></a>

### Automatically generated JavaScript proxy files

If you do not want to include all of the hubs and methods in the JavaScript proxy file for each user, you can disable the automatic generation of the file. You might choose this option if you have multiple hubs and methods, but do not want every user to be aware of all of the methods. You disable automatic generation by setting **EnableJavaScriptProxies** to **false**.

[!code-csharp[Main](introduction-to-security/samples/sample5.cs)]

For more information about the JavaScript proxy files, see [The generated proxy and what it does for you](../guide-to-the-api/hubs-api-guide-javascript-client.md#genproxy). <a id="exceptions"></a>

### Exceptions

You should avoid passing exception objects to clients because the objects may expose sensitive information to the clients. Instead, call a method on the client that displays the relevant error message.

[!code-csharp[Main](introduction-to-security/samples/sample6.cs)]
