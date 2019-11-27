---
uid: web-forms/overview/ajax-control-toolkit/textboxwatermark/using-textboxwatermark-with-validation-controls-vb
title: "Using TextBoxWatermark With Validation Controls (VB) | Microsoft Docs"
author: wenz
description: "The TextBoxWatermark control in the AJAX Control Toolkit extends a text box so that a text is displayed within the box. When a user clicks into the box, it i..."
ms.author: riande
ms.date: 06/02/2008
ms.assetid: e6c2cb98-f745-4bc8-973a-813879c8a891
msc.legacyurl: /web-forms/overview/ajax-control-toolkit/textboxwatermark/using-textboxwatermark-with-validation-controls-vb
msc.type: authoredcontent
---
# Using TextBoxWatermark With Validation Controls (VB)

by [Christian Wenz](https://github.com/wenz)

[Download Code](https://download.microsoft.com/download/9/3/f/93f8daea-bebd-4821-833b-95205389c7d0/TextBoxWatermark2.vb.zip) or [Download PDF](https://download.microsoft.com/download/b/6/a/b6ae89ee-df69-4c87-9bfb-ad1eb2b23373/textboxwatermark2VB.pdf)

> The TextBoxWatermark control in the AJAX Control Toolkit extends a text box so that a text is displayed within the box. When a user clicks into the box, it is emptied. If the user leaves the box without entering text, the prefilled text reappears. This may collide with ASP.NET Validation Controls on the same page, but these issues may be overcome.

## Overview

The `TextBoxWatermark` control in the AJAX Control Toolkit extends a text box so that a text is displayed within the box. When a user clicks into the box, it is emptied. If the user leaves the box without entering text, the prefilled text reappears. This may collide with ASP.NET Validation Controls on the same page, but these issues may be overcome.

## Steps

The basic setup of the sample is the following: a `TextBox` control is watermarked using a `TextBoxWatermarkExtender` control. A button triggers a postback and will later be used to trigger the validation controls on the page. Also, a `ScriptManager` control is required to initialize ASP.NET AJAX:

[!code-aspx[Main](using-textboxwatermark-with-validation-controls-vb/samples/sample1.aspx)]

Now add a `RequiredFieldValidator` control that checks whether there is text in the field when the form is submitted. The `InitialValue` property of the validator must be set to the same value that is used in the `TextBoxWatermarkExtender` control: When the form is submitted, the value of an unchanged textbox is the watermark value within it:

[!code-aspx[Main](using-textboxwatermark-with-validation-controls-vb/samples/sample2.aspx)]

However there is one problem with this approach: If the client disables JavaScript, the text field is not prefilled with the watermark text, therefore the `RequiredFieldValidator` does not trigger an error message. Therefore, a second `RequiredFieldValidator` control is required which checks for an empty text box (omitting the `InitialValue` attribute).

[!code-aspx[Main](using-textboxwatermark-with-validation-controls-vb/samples/sample3.aspx)]

Since both validators use `Display`=`"Dynamic"`, the end user cannot distinguish from the visual appearance which of the two validators was fired; instead, it looks like there was only one of them.

Finally, add some server-side code to output the text in the field if no validator issued an error message:

[!code-aspx[Main](using-textboxwatermark-with-validation-controls-vb/samples/sample4.aspx)]

[![The validator complains that there is no text in the field](using-textboxwatermark-with-validation-controls-vb/_static/image2.png)](using-textboxwatermark-with-validation-controls-vb/_static/image1.png)

The validator complains that there is no text in the field ([Click to view full-size image](using-textboxwatermark-with-validation-controls-vb/_static/image3.png))

> [!div class="step-by-step"]
> [Previous](using-textboxwatermark-in-a-formview-vb.md)
