string destinationURL = "https://www.contoso.com/default.aspx?user=test";
NextPage.NavigateUrl = "~/Finish?url=" + Server.UrlEncode(destinationURL);