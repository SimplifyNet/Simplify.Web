using Simplify.Web.Old;
using Simplify.Web.Old.Attributes;
using Simplify.Web.Old.Responses;

namespace SampleApp.Classic.Controllers;

[Get("/")]
public class DefaultController : Controller
{
	public override ControllerResponse Invoke() => new StaticTpl("Default");
}