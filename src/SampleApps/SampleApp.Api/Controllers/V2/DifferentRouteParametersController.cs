using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Api.Controllers.V2;

[Get("api/v2/different-route-parameters/{StringParam}/{IntParam}/{BoolParam}/{StringArrayParam}")]
public class DifferentRouteParametersController : Controller2
{
	public ControllerResponse Invoke(string stringParam, int intParam, bool boolParam, string[] stringArrayParam) =>
		Content($"""
		         String param: {stringParam}
		         Integer param: {intParam}
		         bool param: {boolParam},
		         String array param: {string.Join(", ", stringArrayParam)}
		         """);
}