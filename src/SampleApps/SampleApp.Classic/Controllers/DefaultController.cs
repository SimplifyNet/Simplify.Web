using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Classic.Controllers;

[Get("/")]
public class DefaultController : Controller2
{
	public ControllerResponse Invoke() => StaticTpl("Default");
}