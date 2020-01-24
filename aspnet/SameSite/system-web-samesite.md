---
title: Work with SameSite cookies in ASP.NET
author: rick-anderson
description: Learn how to use to SameSite cookies in ASP.NET
ms.author: riande
ms.date: 1/22/2019
uid: samesite/system-web-samesite
---
# Work with SameSite cookies in ASP.NET

By [Rick Anderson](https://twitter.com/RickAndMSFT)

SameSite is an [IETF](https://ietf.org/about/) draft standard designed to provide some protection against cross-site request forgery (CSRF) attacks. Originally drafted in [2016](https://tools.ietf.org/html/draft-west-first-party-cookies-07), the draft standard was recently updated in [2019](https://tools.ietf.org/html/draft-west-cookie-incrementalism-00). The updated standard is not fully compatible with the previous standard, with the following being the most noticeable differences:

* Cookies without SameSite header are treated as `SameSite=Lax` by default.
* `SameSite=None` must be used to allow cross-site cookie use.
* Cookies that assert `SameSite=None` must also be marked as `Secure`.

The `SameSite=Lax` setting works for most application cookies. Some forms of authentication like [OpenID Connect](https://openid.net/connect/) (OIDC) and [WS-Federation](https://auth0.com/docs/protocols/ws-fed) default to POST based redirects. The POST based redirects trigger the SameSite browser protections, so SameSite is disabled for these components. Most [OAuth](https://oauth.net/) logins are not affected due to differences in how the request flows.

Applications that use `iframe` may experience issues with `SameSite=Lax` or `SameSite=Strict` cookies because iframes are treated as
cross-site scenarios.

The `SameSite=None` option may cause compatibility problems with clients that implemented the prior [2016 draft standard](https://tools.ietf.org/html/draft-west-first-party-cookies-07) (for example, iOS 12). See [Supporting older browsers](#sob) in this document.

Each ASP.NET component that emits cookies needs to decide if SameSite is appropriate.

Please see [Known Issues](#known) for problems with applications after installing the 2019 .Net SameSite updates.

## Using SameSite in ASP.NET 4.7.2 and 4.8

.Net currently supports the [2019 draft standard](https://tools.ietf.org/html/draft-west-cookie-incrementalism-00) for SameSite.
Developers are able to programatically control the value of the SameSite header using the [HttpCookie.SameSite Property](/dotnet/api/system.web.httpcookie.samesite#System_Web_HttpCookie_SameSite). Setting this property to `Strict`, `Lax`, or `None`
will result in those values being written on the network with the cookie. Setting it equal to `(SameSiteMode)(-1)` indicates that
no SameSite header should be included on the network with the cookie. The [HttpCookie.Secure Property](/dotnet/api/system.web.httpcookie.secure) (or 'requireSSL' in config files) can be used to mark the cookie as `Secure` or not.

New `HttpCookie` instances will default to `SameSite=(SameSiteMode)(-1)` and `Secure=false`. These defaults can be overridden in
the `system.web/httpCookies` configuration section, where the string "Unspecified" is a friendly configuration-only syntax for `(SameSiteMode)(-1)`.
```xml
<configuration>
 <system.web>
  <httpCookies sameSite="[Strict|Lax|None|Unspecified]" requireSSL="[true|false]" />
 <system.web>
<configuration>
```

ASP.Net also issues four specific cookies of its own for these features: Anonymous Authentication, Forms Authentication, Session State,
and Role Management. Instances of these cookies obtained in runtime can be manipulated using the `SameSite` and `Secure` properties
just like any other HttpCookie instance. However, due to the patchwork emergence of the SameSite standard, configuration options for
these four features cookies is inconsistent. The relevant configuration sections and attributes (with defaults) are below. If a
SameSite/Secure-related attribute exists for a feature, then the feature will use it's own default or configured value. If there is
no corresponding attribute, then the default will come from the `system.web/httpCookies` section.
```xml
<configuration>
 <system.web>
  <anonymousIdentification cookieRequireSSL="false" />
  <authentication>
   <forms cookieSameSite="Lax" requireSSL="false" />
  </authentication>
  <sessionState cookieSameSite="Lax" />
  <roleManager cookieRequiresSSL="false" />
 <system.web>
<configuration>
```  
*Note: 'Unspecified' is only available to `system.web/httpCookies@sameSite` at the moment. We hope to add similar syntax to the cookieSameSite attributes above in future updates. Setting `(SameSiteMode)(-1)` in code still works on instances of these cookies.*

## History and changes

SameSite support was first implemented in .NET 4.7.2 using the [2016 draft standard](https://tools.ietf.org/html/draft-west-first-party-cookies-07#section-4.1).

The November 19, 2019 updates for Windows updated .NET 4.7.2+ from the 2016 standard to the 2019 standard. Additional updates are forthcoming for other versions of Windows. For more information, see <xref:samesite/kbs-samesite>.

 The 2019 draft of the SameSite specification:

* Is **not** backwards compatible with the 2016 draft. For more information, see [Supporting older browsers](#sob) in this document.
* Specifies cookies are treated as `SameSite=Lax` by default.
* Specifies cookies that explicitly assert `SameSite=None` in order to enable cross-site delivery should also be marked as `Secure`.
* Is supported by patches issued as described in the KB's listed above.
* Is scheduled to be enabled by [Chrome](https://chromestatus.com/feature/5088147346030592) by default in [Feb 2020](https://blog.chromium.org/2019/10/developers-get-ready-for-new.html). Browsers started moving to this standard in 2019.

<a name="known"><a/>

## Known Issues

Because the 2016 and 2019 draft specifications are not compatible, the November 2019 .Net Framework update introduces some changes that may be breaking.

1. Session State and Forms Authentication cookies are now written to the network as `Lax` instead of unspecified.
    - While most applications will work with `SameSite=Lax` cookies, applications that POST across sites or applications that **make use
    of `iframe`** may find that their session state or forms auth cookies are not being used as expected. To remedy this issue, change
    the `cookieSameSite` value in the appropriate config section as discussed above.

2. HttpCookies that explicitly set `SameSite=None` in code or config will now have that value written with the cookie, whereas it was
previously omitted. This may cause issues with older browsers that only support the 2016 draft standard.
    - When targeting browsers supporting the 2019 draft standard with `SameSite=None` cookies, remember to also mark them `Secure` or
    they may not be recognized.
    - To revert to the 2016 behavior of not writing `SameSite=None`, use the app setting `aspnet:SupressSameSiteNone=true`. Note that
    this will apply to all HttpCookies in the app.

### Azure App Service—SameSite cookie handling

See [Azure App Service—SameSite cookie handling and .NET Framework 4.7.2 patch](https://azure.microsoft.com/updates/app-service-samesite-cookie-update/) for information about how Azure App Service is configuring SameSite behaviors in .Net 4.7.2 apps.

<a name="sob"></a>

## Supporting older browsers

The 2016 SameSite standard mandated that unknown values must be treated as `SameSite=Strict` values. Apps accessed from older browsers which support the 2016 SameSite standard may break when they get a SameSite property with a value of `None`. Web apps must implement browser detection if they intend to support older browsers. ASP.NET doesn't implement browser detection because User-Agents values are highly volatile and change frequently. The following code can be called at the <xref:HTTP.HttpCookie> call site:

[!code-csharp[](sample/SameSiteCheck.cs?name=snippet)]

In the preceding sample, `MyUserAgentDetectionLib.DisallowsSameSiteNone` is a user supplied library that detects if the user agent doesn't support SameSite `None`. The following code shows a sample `DisallowsSameSiteNone` method:

> [!WARNING]
> The following code is for demonstration only:
> * It should not be considered complete.
> * It is not maintained or supported.

[!code-csharp[](sample/SameSiteCheck.cs?name=snippet2)]

## Test apps for SameSite problems

Apps that interact with remote sites such as through third-party login need to:

* Test the interaction on multiple browsers.
* Apply the [browser detection and mitigation](#sob) discussed in this document.

Test web apps using a client version that can opt-in to the new SameSite behavior. Chrome, Firefox, and Chromium Edge all have new opt-in feature flags that can be used for testing. After your app applies the SameSite patches, test it with older client versions, especially Safari. For more information, see [Supporting older browsers](#sob) in this document.

### Test with Chrome

Chrome 78+ gives misleading results because it has a temporary mitigation in place. The Chrome 78+ temporary mitigation allows cookies less than two minutes old. Chrome 76 or 77 with the appropriate test flags enabled provides more accurate results. To test the new SameSite behavior toggle `chrome://flags/#same-site-by-default-cookies` to **Enabled**. Older versions of Chrome (75 and below) are reported to fail with the new `None` setting. See [Supporting older browsers](#sob) in this document.

Google does not make older chrome versions available. Follow the instructions at [Download Chromium](https://www.chromium.org/getting-involved/download-chromium) to test older versions of Chrome. Do **not** download Chrome from links provided by searching for older versions of chrome.

* [Chromium 76 Win64](https://commondatastorage.googleapis.com/chromium-browser-snapshots/index.html?prefix=Win_x64/664998/)
* [Chromium 74 Win64](https://commondatastorage.googleapis.com/chromium-browser-snapshots/index.html?prefix=Win_x64/638880/)

### Test with Safari

Safari 12 strictly implemented the prior draft and fails when the new `None` value is in a cookie. `None` is avoided via the browser detection code [Supporting older browsers](#sob) in this document. Test Safari 12, Safari 13, and WebKit based OS style logins using MSAL, ADAL or whatever library you are using. The problem is dependent on the underlying OS version. OSX Mojave (10.14) and iOS 12 are known to have compatibility problems with the new SameSite behavior. Upgrading the OS to OSX Catalina (10.15) or iOS 13 fixes the problem. Safari does not currently have an opt-in flag for testing the new spec behavior.

### Test with Firefox

Firefox support for the new standard can be tested on version 68+ by opting in on the `about:config` page with the feature flag `network.cookie.sameSite.laxByDefault`. There haven't been reports of compatibility issues with older versions of Firefox.

### Test with Edge browser

Edge supports the old SameSite standard. Edge version 44 doesn't have any known compatibility problems with the new standard.

### Test with Edge (Chromium)

SameSite flags are set on the `edge://flags/#same-site-by-default-cookies` page. No compatibility issues were discovered with Edge Chromium.

### Test with Electron

Versions of Electron include older versions of Chromium. For example, the version of Electron used by Teams is Chromium 66, which exhibits the older behavior. You must perform your own compatibility testing with the version of Electron your product uses. See [Supporting older browsers](#sob) in the following section.

## Additional resources

* [Upcoming SameSite Cookie Changes in ASP.NET and ASP.NET Core](https://devblogs.microsoft.com/aspnet/upcoming-samesite-cookie-changes-in-asp-net-and-asp-net-core/)
* [Chromium Blog:Developers: Get Ready for New SameSite=None; Secure Cookie Settings](https://blog.chromium.org/2019/10/developers-get-ready-for-new.html)
* [SameSite cookies explained](https://web.dev/samesite-cookies-explained/)
