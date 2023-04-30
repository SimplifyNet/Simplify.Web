using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Simplify.Web.Attributes;
using Simplify.Web.Examples.SelfHosted.Models.Accounts;
using Simplify.Web.Examples.SelfHosted.Views;
using Simplify.Web.Examples.SelfHosted.Views.Accounts;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Controllers.Accounts;

[Post("login")]
public class LoginController : Controller<LoginViewModel>
{
	public override ControllerResponse Invoke()
	{
		if (Model.Password == "1" && Model.UserName == "Foo")
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, Model.UserName)
			};

			var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

			var authenticationManager = Context.Context.Authentication;
			authenticationManager.SignIn(id);

			return new Redirect(RedirectionType.LoginReturnUrl);
		}

		return new Tpl(GetView<LoginView>().Get(Model, GetView<MessageBoxView>().Get(StringTable.WrongUserNameOrPassword)), StringTable.PageTitleLogin);
	}
}