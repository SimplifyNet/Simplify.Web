using System.Threading.Tasks;
using SampleApp.Classic.Views.Shared;
using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Controllers.Shared;

[Priority(-1)]
public class LoginPanelController : AsyncController
{
	public override async Task<ControllerResponse> Invoke()
	{
		return !Context.IsAuthenticated
			? new InlineTpl("LoginPanel", await TemplateFactory.LoadAsync("Shared/LoginPanel/GuestPanel"))
			: new InlineTpl("LoginPanel", await GetView<LoggedUserPanelView>().Get(Context.Context.User.Identity.Name));
	}
}