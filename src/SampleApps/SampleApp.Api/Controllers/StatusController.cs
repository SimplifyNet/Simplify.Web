using Simplify.Web;
using Simplify.Web.Old;
using Simplify.Web.Old.Attributes;

namespace SampleApp.Api.Controllers;

[Get("status")]
public class StatusController : Controller
{
	public override ControllerResponse Invoke() => Content("Service is running!");
}