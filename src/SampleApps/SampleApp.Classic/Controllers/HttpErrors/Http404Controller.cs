using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Classic.Controllers.HttpErrors;

[Http404]
public class Http404Controller : Controller2
{
	public ControllerResponse Invoke() => StaticTpl("HttpErrors/Http404", StringTable["PageTitle404"], 404);
}