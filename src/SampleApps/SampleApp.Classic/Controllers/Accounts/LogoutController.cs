using Microsoft.AspNetCore.Authentication;
using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Controllers.Accounts
{
	[Get("logout")]
	public class LogoutController : Controller
	{
		public override ControllerResponse Invoke()
		{
			Context.Context.SignOutAsync().Wait();

			return new Redirect();
		}
	}
}