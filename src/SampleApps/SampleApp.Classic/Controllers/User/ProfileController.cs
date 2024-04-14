using Simplify.Web;
using Simplify.Web.Old;
using Simplify.Web.Old.Attributes;
using Simplify.Web.Old.Responses;

namespace SampleApp.Classic.Controllers.User;

[Authorize]
[Get("profile")]
public class ProfileController : Controller
{
	public override ControllerResponse Invoke() => new StaticTpl("User/Profile", StringTable.PageTitleProfile);
}