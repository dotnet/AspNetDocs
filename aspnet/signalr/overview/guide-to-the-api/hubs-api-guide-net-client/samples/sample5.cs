var querystringData = new Dictionary<string, string>();
querystringData.Add("contosochatversion", "1.0");
var connection = new HubConnection("https://contoso.com/", querystringData);