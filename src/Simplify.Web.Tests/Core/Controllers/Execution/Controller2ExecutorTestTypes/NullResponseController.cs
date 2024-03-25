namespace Simplify.Web.Tests.Core.Controllers.Execution.Controller2ExecutorTestTypes;

public class NullResponseController : Controller2
{
	public ControllerResponse? Invoke() => null;
}
