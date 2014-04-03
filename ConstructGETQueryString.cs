private string PrepareGetURL(Foo loginData)
{
    var parameters = HttpUtility.ParseQueryString(string.Empty);
    parameters["foo"] = JsonConvert.SerializeObject(loginData);
    parameters["bar"] = Config.Bar;
    var builder = new UriBuilder(Config.FrontendURL);
    builder.Query = parameters.ToString();
    return builder.ToString();
}
