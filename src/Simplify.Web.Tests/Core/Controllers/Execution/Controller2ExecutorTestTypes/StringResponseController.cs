using Moq;

namespace Simplify.Web.Tests.Core.Controllers.Execution.Controller2ExecutorTestTypes;

public class StringResponseController : Controller2
{
	public virtual ControllerResponse Invoke() => Content("Test!");
}
