using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Controllers.HttpErrors
{
	[Http404]
	public class Http404Controller : Controller
	{
		public override ControllerResponse Invoke() => new StaticTpl("HttpErrors/Http404", StringTable.PageTitle404, 404);
	}
}