using System.Threading.Tasks;
using Simplify.Web.Attributes;
using Simplify.Web.Examples.SelfHosted.Views.Shared;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Controllers.Shared;

[Priority(-1)]
public class LoginPanelController : AsyncController
{
	public override async Task<ControllerResponse> Invoke()
	{
		return Context.Context.Authentication.User == null
			? new InlineTpl("LoginPanel", await TemplateFactory.LoadAsync("Shared/LoginPanel/GuestPanel"))
			: new InlineTpl("LoginPanel", await GetView<LoggedUserPanelView>().Get(Context.Context.Authentication.User.Identity.Name));
	}
}