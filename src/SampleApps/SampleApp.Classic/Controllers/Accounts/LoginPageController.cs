using SampleApp.Classic.Views.Accounts;
using Simplify.Web;
using Simplify.Web.Old;
using Simplify.Web.Old.Attributes;
using Simplify.Web.Old.Responses;

namespace SampleApp.Classic.Controllers.Accounts;

[Get("login")]
public class LoginPageController : Controller
{
	public override ControllerResponse Invoke() => new Tpl(GetView<LoginView>().Get(), StringTable.PageTitleLogin);
}