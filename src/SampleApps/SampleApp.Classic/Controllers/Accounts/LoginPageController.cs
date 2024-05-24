using SampleApp.Classic.Views.Accounts;
using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Classic.Controllers.Accounts;

[Get("login")]
public class LoginPageController : Controller
{
	public override ControllerResponse Invoke() => Tpl(GetView<LoginView>().Get(), StringTable.PageTitleLogin);
}