using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Controllers.Accounts;

[Get("logout")]
public class LogoutController : AsyncController
{
	public override async Task<ControllerResponse> Invoke()
	{
		await Context.Context.SignOutAsync();

		return new Redirect();
	}
}