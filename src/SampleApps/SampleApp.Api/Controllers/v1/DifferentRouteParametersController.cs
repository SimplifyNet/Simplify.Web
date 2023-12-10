using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Api.Controllers.v1;

[Get("api/v1/different-route-parameters/{StringParam}/{IntParam:int}/{BoolParam:bool}/{StringArrayParam:string[]}")]
public class DifferentRouteParametersController : Controllerv2
{
	public ControllerResponse Invoke(string stringParam) =>
		Content($"String param: {stringParam}");
}