namespace Simplify.Web.Tests.Core.Controllers.Execution.Controller2ExecutorTestTypes;

public class BadReturnTypeController : Controller2
{
	public object? Invoke() => null;
}
