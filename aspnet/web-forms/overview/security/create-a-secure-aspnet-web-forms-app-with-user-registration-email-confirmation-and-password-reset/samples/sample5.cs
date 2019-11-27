protected void CreateUser_Click(object sender, EventArgs e)
{
	var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
	var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
	IdentityResult result = manager.Create(user, Password.Text);
	if (result.Succeeded)
	{
		// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
		string code = manager.GenerateEmailConfirmationToken(user.Id);
		string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
		manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

		if (user.EmailConfirmed)
		{
		  IdentityHelper.SignIn(manager, user, isPersistent: false);
		  IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
		} 
		else
		{
		  ErrorMessage.Text = "An email has been sent to your account. Please view the email and confirm your account to complete the registration process.";
		}
	}
	else 
	{
		ErrorMessage.Text = result.Errors.FirstOrDefault();
	}
}