using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Controllers.HttpErrors;

[Http404]
public class Http404Controller : Controller
{
	public override ControllerResponse Invoke()
	{
		return new StaticTpl("HttpErrors/Http404", StringTable.PageTitle404);
	}
}