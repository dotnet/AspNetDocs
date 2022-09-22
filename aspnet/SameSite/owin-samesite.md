---
title: Work with SameSite cookies and the Open Web Interface for .NET (OWIN)
author: rick-anderson
description: Work with SameSite cookies and the Open Web Interface for .NET (OWIN)
ms.author: riande
ms.date: 12/6/2019
uid: owin-samesite
---

# SameSite cookies and the Open Web Interface for .NET (OWIN)

By [Rick Anderson](https://twitter.com/RickAndMSFT)

`SameSite` is an [IETF](https://ietf.org/about/) draft designed to provide some protection against cross-site request forgery (CSRF) attacks. The [SameSite 2019 draft](https://tools.ietf.org/html/draft-west-cookie-incrementalism-00):

* Treats cookies as `SameSite=Lax` by default.
* States cookies that explicitly assert `SameSite=None` in order to enable cross-site delivery should be marked as `Secure`.

`Lax` works for most app cookies. Some forms of authentication like [OpenID Connect](https://openid.net/connect/) (OIDC) and [WS-Federation](https://auth0.com/docs/protocols/ws-fed) default to POST based redirects. The POST based redirects trigger the `SameSite` browser protections, so `SameSite` is disabled for these components. Most [OAuth](https://oauth.net/) logins aren't affected due to differences in how the request flows. All other components do **not** set `SameSite` by default and use the clients default behavior (old or new).

The `None` parameter causes compatibility problems with clients that implemented the prior [2016 draft standard](https://tools.ietf.org/html/draft-west-first-party-cookies-07) (for example, iOS 12). See [Supporting older browsers](#sob) in this document.

Each OWIN component that emits cookies needs to decide if `SameSite` is appropriate.

For the ASP.NET 4.x version of this article, see <xref:samesite/system-web-samesite>.

## API usage with SameSite

`Microsoft.Owin` has its own `SameSite` implementation:

* That is not directly dependent on the one in `System.Web`.
* `SameSite` works on all versions targetable by the `Microsoft.Owin` packages, .NET 4.5 and later.
* Only the [SystemWebCookieManager](https://github.com/aspnet/AspNetKatana/blob/dev/src/Microsoft.Owin.Host.SystemWeb/SystemWebCookieManager.cs) component directly interacts with the `System.Web` `HttpCookie` class.

`SystemWebCookieManager` depends on the .NET 4.7.2 `System.Web` APIs to enable `SameSite` support, and the patches to change the behavior.

The reasons to use `SystemWebCookieManager` are outlined in [OWIN and System.Web response cookie integration issues](https://github.com/aspnet/AspNetKatana/wiki/System.Web-response-cookie-integration-issues). `SystemWebCookieManager` is recommended when running on `System.Web`.

The following code sets `SameSite` to `Lax`:

```csharp
owinContext.Response.Cookies.Append("My Key", "My Value", new CookieOptions()
{
    SameSite = SameSiteMode.Lax
});
```

The following APIs use `SameSite`:

* [Microsoft.Owin.SameSiteMode](https://github.com/aspnet/AspNetKatana/blob/dev/src/Microsoft.Owin/SameSiteMode.cs)
* [CookieOptions.SameSite](xref:Microsoft.AspNetCore.Http.CookieOptions.SameSite)
* [CookieAuthenticationOptions Class](/previous-versions/aspnet/dn385599(v%3Dvs.113)) <!-- CookieAuthenticationOptions.CookieSameSite not published -->
* [CookieAuthenticationOptions.CookieSameSite](https://github.com/aspnet/AspNetKatana/blob/dev/src/Microsoft.Owin.Security.Cookies/CookieAuthenticationOptions.cs#L68-#L72)
* [ICookieManager](/previous-versions/aspnet/dn800238(v%3Dvs.113))
* [SystemWebCookieManager](https://github.com/aspnet/AspNetKatana/blob/dev/src/Microsoft.Owin.Host.SystemWeb/SystemWebCookieManager.cs)
* [SystemWebChunkingCookieManager](https://github.com/aspnet/AspNetKatana/blob/dev/src/Microsoft.Owin.Host.SystemWeb/SystemWebChunkingCookieManager.cs)
* [CookieAuthenticationOptions.CookieManager](https://github.com/aspnet/AspNetKatana/blob/dev/src/Microsoft.Owin.Security.Cookies/CookieAuthenticationOptions.cs#L143-#AL148)
* [OpenIdConnectAuthenticationOptions.CookieManager](https://github.com/aspnet/AspNetKatana/blob/dev/src/Microsoft.Owin.Security.OpenIdConnect/OpenIdConnectAuthenticationOptions.cs#L315-#L318)

## History and changes

[Microsoft.Owin](https://www.nuget.org/packages/Microsoft.Owin/) never supported the [`SameSite` 2016 draft standard](https://tools.ietf.org/html/draft-west-first-party-cookies-07#section-4.1).

Support for the [SameSite 2019 draft](https://tools.ietf.org/html/draft-west-cookie-incrementalism-00) is only available in `Microsoft.Owin` 4.1.0 and later. There are no patches for prior versions.

The 2019 draft of the `SameSite` specification:

* Is **not** backwards compatible with the 2016 draft. For more information, see [Supporting older browsers](#sob) in this document.
* Specifies cookies are treated as `SameSite=Lax` by default.
* Specifies cookies that explicitly assert `SameSite=None` in order to enable cross-site delivery should be marked as `Secure`. `None` is a new entry to opt out.
* Is scheduled to be enabled by [Chrome](https://chromestatus.com/feature/5088147346030592) by default in [Feb 2020](https://blog.chromium.org/2019/10/developers-get-ready-for-new.html). Browsers started moving to this standard in 2019.
* Is supported by patches issued as described in KB articles. For more information, see <xref:samesite/kbs-samesite>.

<a name="sob"></a>

## Supporting older browsers

The 2016 `SameSite` standard mandated that unknown values must be treated as `SameSite=Strict` values. Apps accessed from older browsers which support the 2016 `SameSite` standard may break when they get a `SameSite` property with a value of `None`. Web apps must implement browser detection if they intend to support older browsers. ASP.NET doesn't implement browser detection because User-Agents values are highly volatile and change frequently. An extension point in [ICookieManager](/previous-versions/aspnet/dn800238(v%3Dvs.113)) allows plugging in User-Agent specific logic.
<!-- https://learn.microsoft.com/previous-versions/aspnet/dn800238(v%3Dvs.113) -->

In `Startup.Configuration`, add code similar to the following:

[!code-csharp[](sample/Startup1.cs?name=snippet)]

The preceding code requires the .NET 4.7.2 or later `SameSite` patch.

The following code shows an example implementation of `SameSiteCookieManager`:

[!code-csharp[](sample/SameSiteCookieManager.cs?name=snippet)]

In the preceding sample, `DisallowsSameSiteNone` is called in the `CheckSameSite` method. `DisallowsSameSiteNone` is a user method that detects if the user agent doesn't support `SameSite` `None`:

[!code-csharp[](sample/SameSiteCookieManager.cs?name=snippet3&highlight=4)]

The following code shows a sample `DisallowsSameSiteNone` method:

> [!WARNING]
> The following code is for demonstration only:
> * It should not be considered complete.
> * It is not maintained or supported.

[!code-csharp[](sample/SameSiteCookieManager.cs?name=snippet2)]

## Test apps for SameSite problems

Apps that interact with remote sites such as through third-party login need to:

* Test the interaction on multiple browsers.
* Apply the [browser detection and mitigation](#sob) discussed in this document.

Test web apps using a client version that can opt-in to the new `SameSite` behavior. Chrome, Firefox, and Chromium Edge all have new opt-in feature flags that can be used for testing. After your app applies the `SameSite` patches, test it with older client versions, especially Safari. For more information, see [Supporting older browsers](#sob) in this document.

### Test with Chrome

Chrome 78+ gives misleading results because it has a temporary mitigation in place. The Chrome 78+ temporary mitigation allows cookies less than two minutes old. Chrome 76 or 77 with the appropriate test flags enabled provides more accurate results. To test the new `SameSite` behavior toggle `chrome://flags/#same-site-by-default-cookies` to **Enabled**. Older versions of Chrome (75 and below) are reported to fail with the new `None` setting. See [Supporting older browsers](#sob) in this document.

Google does not make older chrome versions available. Follow the instructions at [Download Chromium](https://www.chromium.org/getting-involved/download-chromium) to test older versions of Chrome. Do **not** download Chrome from links provided by searching for older versions of chrome.

* [Chromium 76 Win64](https://commondatastorage.googleapis.com/chromium-browser-snapshots/index.html?prefix=Win_x64/664998/)
* [Chromium 74 Win64](https://commondatastorage.googleapis.com/chromium-browser-snapshots/index.html?prefix=Win_x64/638880/)

### Test with Safari

Safari 12 strictly implemented the prior draft and fails when the new `None` value is in a cookie. `None` is avoided via the browser detection code [Supporting older browsers](#sob) in this document. Test Safari 12, Safari 13, and WebKit based OS style logins using [MSAL](/azure/active-directory/develop/msal-overview) or whatever library you are using. The problem is dependent on the underlying OS version. OSX Mojave (10.14) and iOS 12 are known to have compatibility problems with the new `SameSite` behavior. Upgrading the OS to OSX Catalina (10.15) or iOS 13 fixes the problem. Safari does not currently have an opt-in flag for testing the new spec behavior.

### Test with Firefox

Firefox support for the new standard can be tested on version 68+ by opting in on the `about:config` page with the feature flag `network.cookie.sameSite.laxByDefault`. There haven't been reports of compatibility issues with older versions of Firefox.

### Test with Edge browser

Edge supports the old `SameSite` standard. Edge version 44 doesn't have any known compatibility problems with the new standard.

### Test with Edge (Chromium)

`SameSite` flags are set on the `edge://flags/#same-site-by-default-cookies` page. No compatibility issues were discovered with Edge Chromium.

### Test with Electron

Versions of Electron include older versions of Chromium. For example, the version of Electron used by Teams is Chromium 66, which exhibits the older behavior. You must perform your own compatibility testing with the version of Electron your product uses. See [Supporting older browsers](#sob) in the following section.

## Additional resources

* [Chromium Blog:Developers: Get Ready for New SameSite=None; Secure Cookie Settings](https://blog.chromium.org/2019/10/developers-get-ready-for-new.html)
* [SameSite cookies explained](https://web.dev/samesite-cookies-explained/)
* [OWIN and System.Web response cookie integration issues](https://github.com/aspnet/AspNetKatana/wiki/System.Web-response-cookie-integration-issues)
