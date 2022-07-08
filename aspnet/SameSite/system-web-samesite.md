---
title: Work with SameSite cookies in ASP.NET
author: rick-anderson
description: Learn how to use to SameSite cookies in ASP.NET
ms.author: riande
ms.date: 2/15/2019
uid: samesite/system-web-samesite
---

# Work with SameSite cookies in ASP.NET

By [Rick Anderson](https://twitter.com/RickAndMSFT)

SameSite is an [IETF](https://ietf.org/about/) draft standard designed to provide some protection against cross-site request forgery (CSRF) attacks. Originally drafted in [2016](https://tools.ietf.org/html/draft-west-first-party-cookies-07), the draft standard was updated in [2019](https://tools.ietf.org/html/draft-west-cookie-incrementalism-00). The updated standard is not backward compatible with the previous standard, with the following being the most noticeable differences:

* Cookies without SameSite header are treated as `SameSite=Lax` by default.
* `SameSite=None` must be used to allow cross-site cookie use.
* Cookies that assert `SameSite=None` must also be marked as `Secure`.
* Applications that use [`<iframe>`](https://developer.mozilla.org/docs/Web/HTML/Element/iframe) may experience issues with `sameSite=Lax` or `sameSite=Strict` cookies because `<iframe>` is treated as cross-site scenarios.
* The value `SameSite=None` is not allowed by the [2016 standard](https://tools.ietf.org/html/draft-west-first-party-cookies-07) and causes some implementations to treat such cookies as `SameSite=Strict`. See [Supporting older browsers](#sob) in this document.

The `SameSite=Lax` setting works for most application cookies. Some forms of authentication like [OpenID Connect](https://openid.net/connect/) (OIDC) and [WS-Federation](https://auth0.com/docs/protocols/ws-fed) default to POST based redirects. The POST based redirects trigger the SameSite browser protections, so SameSite is disabled for these components. Most [OAuth](https://oauth.net/) logins are not affected due to differences in how the request flows.

Each ASP.NET component that emits cookies needs to decide if SameSite is appropriate.

See [Known Issues](#known) for problems with applications after installing the 2019 .Net SameSite updates.

## Using SameSite in ASP.NET 4.7.2 and 4.8

.Net 4.7.2 and 4.8 supports the [2019 draft standard](https://tools.ietf.org/html/draft-west-cookie-incrementalism-00) for SameSite since the release of updates in December 2019. Developers are able to programmatically control the value of the SameSite header using the [HttpCookie.SameSite property](/dotnet/api/system.web.httpcookie.samesite). Setting the `SameSite` property to `Strict`, `Lax`, or `None` results in those values being written on the network with the cookie. Setting it equal to `(SameSiteMode)(-1)` indicates that no SameSite header should be included on the network with the cookie. The [HttpCookie.Secure Property](/dotnet/api/system.web.httpcookie.secure), or 'requireSSL' in config files, can be used to mark the cookie as `Secure` or not.

New `HttpCookie` instances will default to `SameSite=(SameSiteMode)(-1)` and `Secure=false`. These defaults can be overridden in the `system.web/httpCookies` configuration section, where the string `"Unspecified"` is a friendly configuration-only syntax for `(SameSiteMode)(-1)`:

```xml
<configuration>
 <system.web>
  <httpCookies sameSite="[Strict|Lax|None|Unspecified]" requireSSL="[true|false]" />
 <system.web>
<configuration>
```

ASP.Net also issues four specific cookies of its own for these features: Anonymous Authentication, Forms Authentication, Session State, and Role Management. Instances of these cookies obtained in runtime can be manipulated using the `SameSite` and `Secure` properties just like any other HttpCookie instance. However, due to the patchwork emergence of the SameSite standard, configuration options for these four features cookies is inconsistent. The relevant configuration sections and attributes, with defaults, are shown below. If there is no `SameSite` or `Secure` related attribute for a feature, then the feature will fall back on the defaults configured in the `system.web/httpCookies` section discussed above.

```xml
<configuration>
 <system.web>
  <anonymousIdentification cookieRequireSSL="false" /> <!-- No config attribute for SameSite -->
  <authentication>
   <forms cookieSameSite="Lax" requireSSL="false" />
  </authentication>
  <sessionState cookieSameSite="Lax" /> <!-- No config attribute for Secure -->
  <roleManager cookieRequireSSL="false" /> <!-- No config attribute for SameSite -->
 <system.web>
<configuration>
```  

**Note**: 'Unspecified' is only available to `system.web/httpCookies@sameSite` at the moment. We hope to add similar syntax to the previously shown cookieSameSite attributes in future updates. Setting `(SameSiteMode)(-1)` in code still works on instances of these cookies.*

[!INCLUDE[](~/includes/MTcomments.md)]

<a name="retargeting"></a>

### Retarget .NET apps

To target .NET 4.7.2 or later:

* Ensure *web.config* contains the following:  <!-- review, I removed `debug="true"` -->

  ```xml
  <system.web>
    <compilation targetFramework="4.7.2"/>
    <httpRuntime targetFramework="4.7.2"/>
  </system.web>

* Verify the project file contains the correct [TargetFrameworkVersion](/visualstudio/msbuild/msbuild-target-framework-and-target-platform):

  ```xml
  <TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>
  ```

  The [.NET Migration Guide](/dotnet/framework/migration-guide/) has more details.

* Verify NuGet packages in the project are targeted at the correct framework
version. You can verify the correct framework
version by examining the *packages.config* file, for example:

  ```xml
  <?xml version="1.0" encoding="utf-8"?>
  <packages>
    <package id="Microsoft.AspNet.Mvc" version="5.2.7" targetFramework="net472" />
    <package id="Microsoft.ApplicationInsights" version="2.4.0" targetFramework="net451" />
  </packages>
  ```

  In the preceding *packages.config* file, the `Microsoft.ApplicationInsights` package:
    * Is  targeted against .NET 4.5.1.
    * Should have its `targetFramework` attribute updated to `net472` if an updated package targeting your framework target exists.

<a name="nope"></a>

### .NET versions earlier than 4.7.2

Microsoft does not support .NET versions lower that 4.7.2 for writing the same-site cookie attribute. We have not found a reliable way to:

* Ensure the attribute is written correctly based on browser version.
* Intercept and adjust authentication and session cookies on older framework versions.

### December patch behavior changes

The specific behavior change for .NET Framework is how the `SameSite` property interprets the `None` value:

* Before the patch a value of `None` meant:
  * Do not emit the attribute at all.
* After the patch:
  * A value of `None` means "Emit the attribute with a value of `None`".
  * A `SameSite` value of `(SameSiteMode)(-1)` causes the attribute not to be emitted.

The default SameSite value for forms authentication and session state cookies was changed from `None` to `Lax`.

### Summary of change impact on browsers

If you install the patch and issue a cookie with `SameSite.None`, one of two things will happen:
* Chrome v80 will treat this cookie according to the new implementation, and not enforce same site restrictions on the cookie.
* Any browser that has not been updated to support the new implementation will follow the old implementation. The old implementation says:
  * If you see a value you don't understand, ignore it and switch to strict same site restrictions.

So either the app breaks in Chrome, or you break in numerous other places.

## History and changes

SameSite support was first implemented in .NET 4.7.2 using the [2016 draft standard](https://tools.ietf.org/html/draft-west-first-party-cookies-07#section-4.1).

The November 19, 2019 updates for Windows updated .NET 4.7.2+ from the 2016 standard to the 2019 standard. Additional updates are forthcoming for other versions of Windows. For more information, see <xref:samesite/kbs-samesite>.

 The 2019 draft of the SameSite specification:

* Is **not** backwards compatible with the 2016 draft. For more information, see [Supporting older browsers](#sob) in this document.
* Specifies cookies are treated as `SameSite=Lax` by default.
* Specifies cookies that explicitly assert `SameSite=None` in order to enable cross-site delivery should also be marked as `Secure`.
* Is supported by patches issued as described in the KB's listed above.
* Is scheduled to be enabled by [Chrome](https://chromestatus.com/feature/5088147346030592) by default in [Feb 2020](https://blog.chromium.org/2019/10/developers-get-ready-for-new.html). Browsers started moving to this standard in 2019.

<a name="known"></a>

## Known Issues

Because the 2016 and 2019 draft specifications are not compatible, the November 2019 .Net Framework update introduces some changes that may be breaking.

* Session State and Forms Authentication cookies are now written to the network as `Lax` instead of unspecified.
  * While most apps work with `SameSite=Lax` cookies, apps that POST across sites or applications that make use of `iframe` may find that their session state or forms authorization cookies aren't being used as expected. To remedy this, change the `cookieSameSite` value in the appropriate configuration section as discussed previously.
* HttpCookies that explicitly set `SameSite=None` in code or configuration now have that value written with the cookie, whereas it was previously omitted. This may cause issues with older browsers that only support the 2016 draft standard.
  * When targeting browsers supporting the 2019 draft standard with `SameSite=None` cookies, remember to also mark them `Secure` or they may not be recognized.
  * To revert to the 2016 behavior of not writing `SameSite=None`, use the app setting `aspnet:SupressSameSiteNone=true`. Note that this will apply to all HttpCookies in the app.

### Azure App Service—SameSite cookie handling

See [Azure App Service—SameSite cookie handling and .NET Framework 4.7.2 patch](https://azure.microsoft.com/updates/app-service-samesite-cookie-update/) for information about how Azure App Service is configuring SameSite behaviors in .Net 4.7.2 apps.

<a name="sob"></a>

## Supporting older browsers

The 2016 SameSite standard mandated that unknown values must be treated as `SameSite=Strict` values. Apps accessed from older browsers which support the 2016 SameSite standard may break when they get a SameSite property with a value of `None`. Web apps must implement browser detection if they intend to support older browsers. ASP.NET doesn't implement browser detection because User-Agents values are highly volatile and change frequently.

Microsoft's approach to fixing the problem is to help you implement browser detection components to strip the `sameSite=None` attribute from cookies if a browser is known to not support it. Google's advice was to issue double cookies, one with the new attribute, and one without the attribute at all. However we consider Google's advice limited. Some browsers, especially mobile browsers have very small limits on the number of cookies a site, or a domain name can send. Sending multiple cookies, especially large cookies like
authentication cookies can reach the mobile browser limit very quickly, causing app failures that are hard to diagnose and fix. Furthermore as a framework there is a large
ecosystem of third party code and components that may not be updated to use a double cookie approach.

The browser detection code used in the sample projects in [this GitHub repository]() is contained in two files

* [C# SameSiteSupport.cs](https://github.com/blowdart/AspNetSameSiteSamples/blob/master/SameSiteSupport.cs)
* [VB SameSiteSupport.vb](https://github.com/blowdart/AspNetSameSiteSamples/blob/master/SameSiteSupport.vb)

These detections are the most common browser agents we have seen that support the 2016 standard and for which the attribute needs to be completely removed. It isn't meant as a complete implementation:

* Your app may see browsers that our test sites do not.
* You should be prepared to add detections as necessary for your environment.

How you wire up the detection varies according the version of .NET and the web framework that you are using. The following code can be called at the [HttpCookie](/dotnet/api/system.web.httpcookie) call site:

[!code-csharp[](sample/SameSiteCheck.cs?name=snippet)]

See the following ASP.NET 4.7.2 SameSite cookie topics:

* [C# MVC](xref:samesite/csMVC)
* [C# WebForms](xref:samesite/CSharpWebForms)
* [VB WebForms](xref:samesite/vbWF)
* [VB MVC](xref:samesite/vbMVC)
<!--
* <xref:samesite/csMVC>
* <xref:samesite/CSharpWebForms>
* <xref:samesite/vbWF>
* <xref:samesite/vbMVC>
-->

### Ensuring your site redirects to HTTPS

For ASP.NET 4.x, WebForms and MVC, [IIS's URL Rewrite](/iis/extensions/url-rewrite-module/creating-rewrite-rules-for-the-url-rewrite-module) feature can be used to redirect all requests to HTTPS. The following XML shows a sample rule:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
  <system.webServer>
    <rewrite>
      <rules>
        <rule name="Redirect to https" stopProcessing="true">
          <match url="(.*)"/>
          <conditions>
            <add input="{HTTPS}" pattern="Off"/>
            <add input="{REQUEST_METHOD}" pattern="^get$|^head$" />
          </conditions>
          <action type="Redirect" url="https://{HTTP_HOST}/{R:1}" redirectType="Permanent"/>
        </rule>
      </rules>
    </rewrite>
  </system.webServer>
</configuration>
```

In on-premises installations of [IIS URL Rewrite](https://www.iis.net/downloads/microsoft/url-rewrite) is an optional feature that may need installing.

## Test apps for SameSite problems

You must test your app with the browsers you support and go through your scenarios that involve cookies. Cookie scenarios typically involve

* Login forms
* External login mechanisms such as Facebook, Azure AD, OAuth and OIDC
* Pages that accept requests from other sites
* Pages in your app designed to be embedded in iframes

You should check that cookies are created, persisted and deleted correctly in your app.

Apps that interact with remote sites such as through third-party login need to:

* Test the interaction on multiple browsers.
* Apply the [browser detection and mitigation](#sob) discussed in this document.

Test web apps using a client version that can opt-in to the new SameSite behavior. Chrome, Firefox, and Chromium Edge all have new opt-in feature flags that can be used for testing. After your app applies the SameSite patches, test it with older client versions, especially Safari. For more information, see [Supporting older browsers](#sob) in this document.

### Test with Chrome

Chrome 78+ gives misleading results because it has a temporary mitigation in place. The Chrome 78+ temporary mitigation allows cookies less than two minutes old. Chrome 76 or 77 with the appropriate test flags enabled provides more accurate results. To test the new SameSite behavior toggle `chrome://flags/#same-site-by-default-cookies` to **Enabled**. Older versions of Chrome (75 and below) are reported to fail with the new `None` setting. See [Supporting older browsers](#sob) in this document.

Google does not make older chrome versions available. Follow the instructions at [Download Chromium](https://www.chromium.org/getting-involved/download-chromium) to test older versions of Chrome. Do **not** download Chrome from links provided by searching for older versions of chrome.

* [Chromium 76 Win64](https://commondatastorage.googleapis.com/chromium-browser-snapshots/index.html?prefix=Win_x64/664998/)
* [Chromium 74 Win64](https://commondatastorage.googleapis.com/chromium-browser-snapshots/index.html?prefix=Win_x64/638880/)
* If you're not using a 64bit version of Windows you can use the [OmahaProxy viewer](https://omahaproxy.appspot.com/) to look up which Chromium branch corresponds to Chrome 74 (v74.0.3729.108) using the [instructions provided by Chromium](https://www.chromium.org/getting-involved/download-chromium).

Starting in Canary version `80.0.3975.0`, the Lax+POST temporary mitigation can be disabled for testing purposes using the new flag `--enable-features=SameSiteDefaultChecksMethodRigorously` to allow testing of sites and services in the eventual end state of the feature where the mitigation has been removed. For more information, see The Chromium Projects [SameSite Updates](https://www.chromium.org/updates/same-site)

#### Test with Chrome 80+

[Download](https://www.google.com/chrome/) a version of Chrome that supports their new attribute. At the time of writing, the current version is Chrome 80. Chrome 80 needs the flag `chrome://flags/#same-site-by-default-cookies` enabled to use the new behavior. You should also enable (`chrome://flags/#cookies-without-same-site-must-be-secure`) to test the upcoming behavior for cookies which have no sameSite attribute enabled. Chrome 80 is on target to make the switch to treat cookies without the attribute as `SameSite=Lax`, albeit with a timed grace period for certain requests. To disable the timed grace period Chrome 80 can be launched with the following command line argument:

`--enable-features=SameSiteDefaultChecksMethodRigorously`

Chrome 80 has warning messages in the browser console about missing sameSite attributes. Use F12 to open the browser console.

### Test with Safari

Safari 12 strictly implemented the prior draft and fails when the new `None` value is in a cookie. `None` is avoided via the browser detection code [Supporting older browsers](#sob) in this document. Test Safari 12, Safari 13, and WebKit based OS style logins using MSAL, ADAL or whatever library you are using. The problem is dependent on the underlying OS version. OSX Mojave (10.14) and iOS 12 are known to have compatibility problems with the new SameSite behavior. Upgrading the OS to OSX Catalina (10.15) or iOS 13 fixes the problem. Safari does not currently have an opt-in flag for testing the new spec behavior.

### Test with Firefox

Firefox support for the new standard can be tested on version 68+ by opting in on the `about:config` page with the feature flag `network.cookie.sameSite.laxByDefault`. There haven't been reports of compatibility issues with older versions of Firefox.

### Test with Edge (Legacy) browser

Edge supports the old SameSite standard. Edge version 44+ doesn't have any known compatibility problems with the new standard.

### Test with Edge (Chromium)

SameSite flags are set on the `edge://flags/#same-site-by-default-cookies` page. No compatibility issues were discovered with Edge Chromium.

### Test with Electron

Versions of Electron include older versions of Chromium. For example, the version of Electron used by Teams is Chromium 66, which exhibits the older behavior. You must perform your own compatibility testing with the version of Electron your product uses. See [Supporting older browsers](#sob).

## Reverting SameSite patches

You can revert the updated sameSite behavior in .NET Framework apps to its previous behavior where the sameSite attribute is not emitted for a value of `None`, and revert the authentication and session cookies to not emit the value. This should be viewed as an *extremely temporary fix*, as the Chrome changes will break any external POST requests or authentication for users using browsers which support the changes to the standard.

### Reverting .NET 4.7.2 behavior

Update *web.config* to include the following configuration settings:

```xml
<configuration> 
  <appSettings>
    <add key="aspnet:SuppressSameSiteNone" value="true" />
  </appSettings>
 
  <system.web> 
    <authentication> 
      <forms cookieSameSite="None" /> 
    </authentication> 
    <sessionState cookieSameSite="None" /> 
  </system.web> 
</configuration>
```

## Additional resources

* [Upcoming SameSite Cookie Changes in ASP.NET and ASP.NET Core](https://devblogs.microsoft.com/aspnet/upcoming-samesite-cookie-changes-in-asp-net-and-asp-net-core/)
* [Tips for testing and debugging SameSite-by-default and “SameSite=None; Secure” cookies](https://www.chromium.org/updates/same-site/test-debug)
* [Chromium Blog:Developers: Get Ready for New SameSite=None; Secure Cookie Settings](https://blog.chromium.org/2019/10/developers-get-ready-for-new.html)
* [SameSite cookies explained](https://web.dev/samesite-cookies-explained/)
* [Chrome Updates](https://www.chromium.org/updates/same-site)
* [.NET SameSite Patches](/aspnet/samesite/kbs-samesite)
* [Azure Web Applications Same Site Information](https://azure.microsoft.com/updates/app-service-samesite-cookie-update/)
* [Azure ActiveDirectory Same Site Information](/azure/active-directory/develop/howto-handle-samesite-cookie-changes-chrome-browser)
