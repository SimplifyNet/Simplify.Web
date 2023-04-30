using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Controllers.User;

[Authorize]
[Get("profile")]
public class ProfileController : Controller
{
	public override ControllerResponse Invoke()
	{
		return new StaticTpl("User/Profile", StringTable.PageTitleProfile);
	}
}