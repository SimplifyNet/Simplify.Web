using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Controllers.Shared;

public class NavbarController : Controller
{
	public override ControllerResponse Invoke()
	{
		return new InlineTpl("Navbar", TemplateFactory.Load("Navbar"));
	}
}