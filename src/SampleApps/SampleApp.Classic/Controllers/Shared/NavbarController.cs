using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Controllers.Shared
{
	[Priority(-2)]
	public class NavbarController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new InlineTpl("Navbar", TemplateFactory.Load("Navbar"));
		}
	}
}