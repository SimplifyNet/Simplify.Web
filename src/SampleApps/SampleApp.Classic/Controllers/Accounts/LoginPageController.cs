using SampleApp.Classic.Views.Accounts;
using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Controllers.Accounts
{
	[Get("login")]
	public class LoginPageController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new Tpl(GetView<LoginView>().Get(), StringTable.PageTitleLogin);
		}
	}
}