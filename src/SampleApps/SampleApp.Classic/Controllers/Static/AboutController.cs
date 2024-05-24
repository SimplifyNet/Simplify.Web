using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Classic.Controllers.Static;

[Get("about")]
public class AboutController : Controller2
{
	public ControllerResponse Invoke() => StaticTpl("Static/About", StringTable["PageTitleAbout"]);
}