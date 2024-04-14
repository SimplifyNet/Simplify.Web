using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Simplify.Web.Old;
using Simplify.Web.Old.Attributes;
using Simplify.Web.Old.Responses;

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