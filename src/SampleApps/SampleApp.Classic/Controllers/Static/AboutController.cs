using Simplify.Web.Old;
using Simplify.Web.Old.Attributes;
using Simplify.Web.Old.Responses;

namespace SampleApp.Classic.Controllers.Static;

[Get("about")]
public class AboutController : Controller
{
	public override ControllerResponse Invoke() => new StaticTpl("Static/About", StringTable.PageTitleAbout);
}