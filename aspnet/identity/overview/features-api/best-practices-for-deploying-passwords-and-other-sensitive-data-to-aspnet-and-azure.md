---
uid: identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure
title: "Deploying passwords and other sensitive data to ASP.NET and Azure App Service - ASP.NET 4.x"
author: Rick-Anderson
description: "This tutorial shows how your code can securely store and access secure information. The most important point is you should never store passwords or other sen..."
ms.author: riande
ms.date: 5/21/2024
ms.assetid: 97902c66-cb61-4d11-be52-73f962f2db0a
msc.legacyurl: /identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure
msc.type: authoredcontent
ms.sfi.ropc: t
ms.custom: sfi-ropc-nochange
---
# Best practices for deploying passwords and other sensitive data to ASP.NET and Azure App Service

by [Rick Anderson](https://twitter.com/RickAndMSFT)

> [!WARNING]
>
> * Never store passwords or other sensitive data in source code, including configuration files.
> * Never use production secrets in development and test.

We recommend using the most secure secure authentication option. For Azure services, the most secure authentication is [managed identities](/entra/identity/managed-identities-azure-resources/overview). For many apps, the most secure option is to use the [Azure Key Vault](/azure/key-vault/general/overview).

Avoid [Resource Owner Password Credentials Grant](/entra/identity-platform/developer-glossary#resource-owner) because it:

* Exposes the user's password to the client.
* Is a significant security risk.
* Should only be used when other authentication flows are not possible.

Managed identities are a secure way to authenticate to services without needing to store credentials in code, environment variables, or configuration files. Managed identities are available for Azure services, and can be used with Azure SQL, Azure Storage, and other Azure services:

* [Managed identities in Microsoft Entra for Azure SQL](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity)
* [Managed identities for App Service and Azure Functions](/azure/app-service/overview-managed-identity)
* [Secure authentication flows](/entra/identity-platform/authentication-flows-app-scenarios#web-app-that-signs-in-a-user)

When the app is deployed to a test server, an environment variable can be used to set the connection string to a test database server. For more information, see [Configuration](xref:fundamentals/configuration/index). An environment variable should ***NEVER*** be used to store a production connection string.

For more information, see:

* [Managed identity best practice recommendations](/entra/identity/managed-identities-azure-resources/managed-identity-best-practice-recommendations)
* [Connecting from your application to resources without handling credentials in your code](/entra/identity/managed-identities-azure-resources/overview-for-developers?tabs=portal%2Cdotnet)
* [Azure services that can use managed identities to access other services](/entra/identity/managed-identities-azure-resources/managed-identities-status)
* [IETF OAuth 2.0 Security Best Current Practice](https://datatracker.ietf.org/doc/html/draft-ietf-oauth-security-topics#section-2.4)
