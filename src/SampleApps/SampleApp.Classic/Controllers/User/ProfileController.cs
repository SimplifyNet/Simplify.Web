using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Controllers.User;

[Authorize]
[Get("profile")]
public class ProfileController : Controller
{
	public override ControllerResponse Invoke()
	{
		return new StaticTpl("User/Profile", StringTable.PageTitleProfile);
	}
}