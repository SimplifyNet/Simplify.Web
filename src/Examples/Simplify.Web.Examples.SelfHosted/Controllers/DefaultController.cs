using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Controllers;

[Get("/")]
public class DefaultController : Controller
{
	public override ControllerResponse Invoke()
	{
		return new StaticTpl("Default");
	}
}