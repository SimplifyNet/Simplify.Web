namespace Simplify.Web.Tests.Controllers.V2.Execution.Controller2ExecutorTestTypes;

public class StringResponseController : Controller2
{
	public virtual ControllerResponse Invoke() => Content("Test!");
}