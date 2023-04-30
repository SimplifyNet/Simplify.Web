using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Controllers.Static;

[Get("about")]
public class AboutController : Controller
{
	public override ControllerResponse Invoke()
	{
		return new StaticTpl("Static/About", StringTable.PageTitleAbout);
	}
}