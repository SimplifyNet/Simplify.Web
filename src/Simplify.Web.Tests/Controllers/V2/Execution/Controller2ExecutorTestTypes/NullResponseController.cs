namespace Simplify.Web.Tests.Controllers.V2.Execution.Controller2ExecutorTestTypes;

public class NullResponseController : Controller2
{
	public ControllerResponse? Invoke() => null;
}