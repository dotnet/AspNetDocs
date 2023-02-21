## Why are external resources in my page being blocked in Web Live Preview?

If you have any external resources from a third party domain that are in your page, you may notice that the designer in Web Live Preview has prevented them from loading, and shows this toast:

![Blocked Resources Toast](https://user-images.githubusercontent.com/8541576/220452947-52dc80dc-19c5-40e9-9f10-12007d209491.png)

You may be wondering why the resources have been blocked from loading in the designer, and why the toast is showing. This article explains why resources have been blocked.

### Overview of Web Live Preview and BrowserLink

[Web Live Preview](https://devblogs.microsoft.com/dotnet/introducing-web-live-preview/) (WLP) is a Visual Studio extension built on top of [BrowserLink](/aspnet/core/client-side/using-browserlink). WLP uses BrowserLink to provide a channel for two-way communication between Visual Studio and the designer. This two-way communication allows WLP to provide many features:

* Synchronizing the contents and currently selected node in the designer with that of the editor.
* Pushing new content created from the designer into the editor.
* Executing Action Panel commands.

## Potential Security Vulnerabilities with Web Live Preview and BrowserLink

If you inject any scripts or resources from a third party into your web page, this could expose you to a potential security vulnerability by using WLP and BrowserLink to make a [Cross-Site Scripting (XSS) attack](/aspnet/web-forms/videos/how-do-i/how-do-i-understand-and-defend-against-script-injection-attacks-in-aspnet).

If a third party resource can inject malicious code into the site, the code could use the Browser Link script injected into the designer to make calls back to Visual Studio. The injected script could allow the code to write arbitrary content in files open in Visual Studio or open up other attack vectors.

Currently, there's no good way to prevent attackers from communicating via the BrowserLink script back to Visual Studio.

## Security vulnerability mitigation

As much as possible, we've limited and mitigated the security vulnerabilities from the communication via the BrowserLink script back to Visual Studio. However, some attacks can still be made possible through an XSS attack.

To reduce the possibility of such an XSS attack, by default, WLP blocks all external resources from being loaded. If any external resource is blocked, WLP shows the following toast in the upper-right-hand corner of the designer:

![Blocked Resources Toast](https://user-images.githubusercontent.com/8541576/220452947-52dc80dc-19c5-40e9-9f10-12007d209491.png)

*(In this example, we use the following SVG, which is an external resource used in a sample web app: https://visualstudio.microsoft.com/wp-content/uploads/2021/10/Product-Icon.svg)*

If the [Microsoft Edge DevTools](/microsoft-edge/devtools-guide-chromium/overview) window is opened, you may also notice the following message explaining why an external resource failed to load:

![Blocked Resources Message DevTools](https://user-images.githubusercontent.com/8541576/220453093-773a6592-375c-43f3-8580-bae1207c04b4.png)

In the toast of the designer, if you select the **click here** link, it brings up the following dialog, with the domains of the blocked resources automatically added to the dialog's list:

![Allow Domains](https://user-images.githubusercontent.com/8541576/220453127-86114eb1-b3c6-465d-87c6-7bc6a31ea686.png)

Any resources coming from external domains stored in the list of the dialog won't be blocked by default in WLP and loads as usual. After clicking **OK**, the designer reloads the page and loads any previously blocked resources whose domains were added to the dialog. ***Make sure that you only allow external domains that you have verified to be trustworthy and safe.***

Clicking the **Web Live Preview - external domains** link in the toast brings up the same dialog, but won't add any of the domains of the blocked resources into the dialog:

![Allow Domains without Add](https://user-images.githubusercontent.com/8541576/220453161-81d77e15-5e70-415f-bced-62421159b151.png)

You can also access the dialog through the `Tools -> Options -> Web Live Preview -> Allowed external domains during design`  setting. The dialog settings are per installed instance of Visual Studio.

![Tools Options Blocked Resources](https://user-images.githubusercontent.com/8541576/220453205-012d1d8a-0883-465f-9c25-6d4b4e2f00dd.png)

> [!WARNING]
>
>Allowing domains to not be blocked by default could expose you to the XSS attack mentioned above. **Again, we highly recommend only allowing external domains that you can verify as trustworthy and safe.**
