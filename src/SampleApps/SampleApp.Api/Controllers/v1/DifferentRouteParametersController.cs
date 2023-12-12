using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Api.Controllers.v1;

[Get("api/v1/different-route-parameters/{StringParam}/{IntParam:int}/{BoolParam:bool}/{StringArrayParam:string[]}")]
public class DifferentRouteParametersController : ControllerV2
{
	public ControllerResponse Invoke(string stringParam, int intParam, bool boolParam, string[] stringArrayParam) =>
		Content($@"
String param: {stringParam}
Integer param: {intParam}
bool param: {boolParam},
String array param: {stringArrayParam}");
}