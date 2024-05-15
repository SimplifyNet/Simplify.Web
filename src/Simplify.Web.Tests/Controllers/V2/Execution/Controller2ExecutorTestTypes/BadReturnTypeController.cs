namespace Simplify.Web.Tests.Controllers.V2.Execution.Controller2ExecutorTestTypes;

public class BadReturnTypeController : Controller2
{
	public object? Invoke() => null;
}
