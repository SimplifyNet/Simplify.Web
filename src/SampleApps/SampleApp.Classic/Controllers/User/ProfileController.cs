using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Controllers.User;

[Authorize]
[Get("profile")]
public class ProfileController : Controller
{
	public override ControllerResponse Invoke() => new StaticTpl("User/Profile", StringTable.PageTitleProfile);
}