using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Classic.Controllers.User;

[Authorize]
[Get("profile")]
public class ProfileController : Controller
{
	public override ControllerResponse Invoke() => StaticTpl("User/Profile", StringTable.PageTitleProfile);
}