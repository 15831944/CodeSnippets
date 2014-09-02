// In Global.asax

protected void Application_EndRequest(object sender, System.EventArgs e)
{
    try
    {
        //Check if the user is Authenticated and been redirected to login page
        if (Request.IsAuthenticated && Response.StatusCode == 302 && Response.RedirectLocation.ToUpper().Contains("MEMBERSHIPLOGIN.ASPX"))
        {
            // check if the user has access to the page
            if (!UrlAuthorizationModule.CheckUrlAccessForPrincipal(Request.FilePath, User, "GET"))
            {
                //Pass a parameter to the login.aspx page 
                //FormsAuthentication.RedirectToLoginPage("errCode=401");

                //Or you can redirect him to another page like AuthenticationFaild.aspx
                Response.Redirect("~/NoAccess.htm");
            }
        }
    }
    catch (Exception ex)
    {
        //Do nothing
    }
}
