using Simplify.Web.Attributes;
using Simplify.Web.Examples.SelfHosted.Views.Accounts;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Controllers.Accounts;

[Get("login")]
public class LoginPageController : Controller
{
	public override ControllerResponse Invoke()
	{
		return new Tpl(GetView<LoginView>().Get(), StringTable.PageTitleLogin);
	}
}