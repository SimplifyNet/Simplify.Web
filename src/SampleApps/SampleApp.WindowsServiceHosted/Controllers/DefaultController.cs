using Simplify.Web;

namespace SampleApp.WindowsServiceHosted.Controllers;

[Get("/")]
public class DefaultController : Controller
{
	public override ControllerResponse Invoke()
	{
		return new Tpl("Hello from Simplify.Web Windows Service hosted application!");
	}
}