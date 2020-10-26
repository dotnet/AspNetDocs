---
title: "How to: Consume Events in a Web Forms Application"
ms.date: "03/30/2017"
ms.technology: dotnet-standard
dev_langs: 
  - "csharp"
  - "vb"
helpviewer_keywords: 
  - "events [.NET], Web Forms"
  - "Web Forms controls, and events"
  - "event handlers [.NET], Web Forms"
  - "events [.NET], consuming"
  - "Web Forms, event handling"
ms.assetid: 73bf8638-c4ec-4069-b0bb-a1dc79b92e32
---
# How to: Consume events in a Web Forms app

A common scenario in ASP.NET Web Forms applications is to populate a webpage with controls, and then perform a specific action based on which control the user clicks. For example, a <xref:System.Web.UI.WebControls.Button?displayProperty=nameWithType> control raises an event when the user clicks it in the webpage. By handling the event, your application can perform the appropriate application logic for that button click.  
  
## Handle a button-click event on a webpage  
  
1. Create a ASP.NET Web Forms page (webpage) that has a <xref:System.Web.UI.WebControls.Button> control with the `OnClick` value set to the name of method that you will define in the next step.  
  
    ```xml  
    <asp:Button ID="Button1" runat="server" Text="Click Me" OnClick="Button1_Click" />  
    ```  
  
2. Define an event handler that matches the <xref:System.Web.UI.WebControls.Button.Click> event delegate signature and that has the name you defined for the `OnClick` value.  
  
    ```csharp  
    protected void Button1_Click(object sender, EventArgs e)  
    {  
        // perform action  
    }  
    ```  
  
    ```vb  
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click  
        ' perform action  
    End Sub  
    ```  
  
     The <xref:System.Web.UI.WebControls.Button.Click> event uses the <xref:System.EventHandler> class for the delegate type and the <xref:System.EventArgs> class for the event data. The ASP.NET page framework automatically generates code that creates an instance of <xref:System.EventHandler> and adds this delegate instance to the <xref:System.Web.UI.WebControls.Button.Click> event of the <xref:System.Web.UI.WebControls.Button> instance.  
  
3. In the event handler method that you defined in step 2, add code to perform any actions that are required when the event occurs.  
  
## See also

- [Events](index.md)
