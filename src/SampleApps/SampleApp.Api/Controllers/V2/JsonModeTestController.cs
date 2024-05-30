using SampleApp.Api.ViewModels;
using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Api.Controllers.V2;

[Get("api/v2/json-model-test")]
public class JsonModeTestController : Controller2<TestModel>
{
	public ControllerResponse Invoke() =>
		Content($"""
		         String param: {Model.StringParam}
		         Integer param: {Model.IntParam}
		         bool param: {Model.BoolParam}
		         String list param: {string.Join(", ", Model.StringListParam!)}
		         """);
}