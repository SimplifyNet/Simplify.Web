using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.WindowsService.Controllers;

[Get("/")]
public class DefaultController : Controller
{
	public override ControllerResponse Invoke()
	{
		return new Tpl("Hello from OWIN self-hosted windows service application with HttpListener server!");
	}
}