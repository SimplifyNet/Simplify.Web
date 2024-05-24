using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Classic.Controllers.Shared;

[Priority(-2)]
public class NavbarController : Controller
{
	public override ControllerResponse Invoke() => InlineTpl("Navbar", TemplateFactory.Load("Navbar"));
}