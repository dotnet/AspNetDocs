---
uid: mvc/overview/getting-started/mvc-learning-sequence
title: "MVC recommended tutorials and articles | Microsoft Docs"
author: Rick-Anderson
description: "This page contains links to ASP.NET MVC tutorials and a suggested sequence to follow them."
ms.author: riande
ms.date: 05/22/2015
ms.assetid: 8513a57a-2d45-4d6b-881c-15a01c5cbb1c
msc.legacyurl: /mvc/overview/getting-started/mvc-learning-sequence
msc.type: authoredcontent
---
# MVC recommended tutorials and articles

by [Rick Anderson](https://twitter.com/RickAndMSFT)

<a id="pwd"></a>
## Getting Started

- [Getting Started with ASP.NET MVC 5](introduction/getting-started.md) This 11 part series is a good place to start.
- [Pluralsight ASP.NET MVC 5 Fundamentals](https://pluralsight.com/training/Player?author=scott-allen&amp;name=aspdotnet-mvc5-fundamentals-m1-introduction&amp;mode=live&amp;clip=0&amp;course=aspdotnet-mvc5-fundamentals) (video course)
- [Lifecycle of an ASP.NET MVC 5 Application](lifecycle-of-an-aspnet-mvc-5-application.md) PDF document that charts the lifecycle of an ASP.NET MVC 5 app.

<a id="con"></a>
## Working with data

- [Getting Started with EF 6 Code First using MVC 5](getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application.md) Tom Dykstra's award winning series dives deep into EF.

<a id="wj"></a>
## Security

- [Create an ASP.NET MVC app with auth and SQL DB and deploy to Azure](https://azure.microsoft.com/documentation/articles/web-sites-dotnet-deploy-aspnet-mvc-app-membership-oauth-sql-database/) This popular tutorial walks you through creating a simple app and adding membership and roles.
- [Create an ASP.NET MVC 5 App with Facebook, Twitter, LinkedIn and Google OAuth2 Sign-on](../security/create-an-aspnet-mvc-5-app-with-facebook-and-google-oauth2-and-openid-sign-on.md) This tutorial shows you how to build an ASP.NET MVC 5 web application that enables users to log in using OAuth 2.0 with credentials from an external authentication provider, such as Facebook, Twitter, LinkedIn, Microsoft, or Google.
- [Create a secure ASP.NET MVC 5 web app with log in, email confirmation and password reset](../security/create-an-aspnet-mvc-5-web-app-with-email-confirmation-and-password-reset.md) First in a series on Identity, includes code to [resend a confirmation link](../security/create-an-aspnet-mvc-5-web-app-with-email-confirmation-and-password-reset.md#rsend).
- [ASP.NET MVC 5 app with SMS and email Two-Factor Authentication](../security/aspnet-mvc-5-app-with-sms-and-email-two-factor-authentication.md) Second on Identity series.
- [Best practices for deploying passwords and other sensitive data to ASP.NET and Azure App Service](../../../identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure.md)
- [Two-factor authentication using SMS and email with ASP.NET Identity](../../../identity/overview/features-api/two-factor-authentication-using-sms-and-email-with-aspnet-identity.md) `isPersistent` and the security cookie, code to require a user to have a validated email account before they can log on, how SignInManager checks for 2FA requirement, and more.
- [Account Confirmation and Password Recovery with ASP.NET Identity](../../../identity/overview/features-api/account-confirmation-and-password-recovery-with-aspnet-identity.md) Provides details on Identity not found in [Create a secure ASP.NET MVC 5 web app with log in, email confirmation and password reset](../security/create-an-aspnet-mvc-5-web-app-with-email-confirmation-and-password-reset.md) such as how to let users reset their forgotten password.

<a id="da"></a>
## Azure

- [Create an ASP.NET web app in Azure](https://azure.microsoft.com/documentation/articles/web-sites-dotnet-get-started/) Short and simple tutorial for deployment to Azure.
- [Create an ASP.NET MVC app with auth and SQL DB and deploy to Azure](https://azure.microsoft.com/documentation/articles/web-sites-dotnet-deploy-aspnet-mvc-app-membership-oauth-sql-database/)

<a id="perf"></a>
## Performance and Debugging

- [Profile and debug your ASP.NET MVC app with Glimpse](../performance/profile-and-debug-your-aspnet-mvc-app-with-glimpse.md)

## ASP.NET MVC DropDownListFor with SelectListItem

When using the <xref:System.Web.Mvc.Html.SelectExtensions.DropDownListFor%2A> helper and passing to it the collection of `SelectListItem` from which it is populated, the `DropdownListFor` modifies the passed collection after it is called. `DropdownListFor` changes the `SelectListItems` Selected properties to whatever was selected by the dropdown list. This leads to unexpected behavior.

The <xref:System.Web.Mvc.Html.SelectExtensions.DropDownListFor%2A>, <xref:System.Web.Mvc.Html.SelectExtensions.DropDownList%2A>, <xref:System.Web.Mvc.Html.SelectExtensions.EnumDropDownListFor%2A>, <xref:System.Web.Mvc.Html.SelectExtensions.ListBox%2A>, and <xref:System.Web.Mvc.Html.SelectExtensions.ListBoxFor%2A> update the Selected property of any `IEnumerable<SelectListItem>` passed or found in ViewData.

The workaround is to create separate enumerables, containing distinct `SelectListItem` instances, for each property in the model.

For more information, see [GetSelectListWithDefaultValue Modifies IEnumerable\<SelectListItem\> selectList](https://github.com/aspnet/AspNetWebStack/issues/271)
