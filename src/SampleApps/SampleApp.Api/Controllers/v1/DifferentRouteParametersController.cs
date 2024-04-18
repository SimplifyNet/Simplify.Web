using Simplify.Web.Old;
using Simplify.Web.Old.Attributes;

namespace SampleApp.Api.Controllers.v1;

[Get("api/v1/different-route-parameters/{StringParam}/{IntParam:int}/{BoolParam:bool}/{StringArrayParam:string[]}")]
public class DifferentRouteParametersController : Controller
{
	public override ControllerResponse Invoke() =>
		Content($@"
String param: {RouteParameters.StringParam}
Integer param: {RouteParameters.IntParam}
bool param: {RouteParameters.BoolParam},
String array param: {string.Join(", ", RouteParameters.StringArrayParam)}");
}