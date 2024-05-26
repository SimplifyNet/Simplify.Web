using System.Threading.Tasks;
using SampleApp.Classic.Views.Shared;
using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Classic.Controllers.Shared;

[Priority(-1)]
public class LoginPanelController : Controller2
{
	public async Task<ControllerResponse> Invoke() =>
		!Context.IsAuthenticated
			? InlineTpl("LoginPanel", await TemplateFactory.LoadAsync("Shared/LoginPanel/GuestPanel"))
			: InlineTpl("LoginPanel", await GetView<LoggedUserPanelView>().Get(Context.Context.User.Identity!.Name));
}