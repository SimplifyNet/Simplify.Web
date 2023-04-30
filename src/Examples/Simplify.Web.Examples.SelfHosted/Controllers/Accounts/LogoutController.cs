using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Controllers.Accounts;

[Get("logout")]
public class LogoutController : Controller
{
	public override ControllerResponse Invoke()
	{
		Context.Context.Authentication.SignOut();

		return new Redirect();
	}
}