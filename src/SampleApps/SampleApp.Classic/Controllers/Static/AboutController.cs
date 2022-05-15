using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Controllers.Static;

[Get("about")]
public class AboutController : Controller
{
	public override ControllerResponse Invoke() => new StaticTpl("Static/About", StringTable.PageTitleAbout);
}