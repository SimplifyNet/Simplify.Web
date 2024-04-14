using Simplify.Web;
using Simplify.Web.Old;
using Simplify.Web.Old.Attributes;
using Simplify.Web.Old.Responses;

namespace SampleApp.Classic.Controllers.Shared;

[Priority(-2)]
public class NavbarController : Controller
{
	public override ControllerResponse Invoke() => new InlineTpl("Navbar", TemplateFactory.Load("Navbar"));
}