using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.Katana.Controllers;

[Get("/")]
public class DefaultController : Controller
{
	public override ControllerResponse Invoke()
	{
		return new Tpl("Hello from OWIN IIS-hosted application!");
	}
}