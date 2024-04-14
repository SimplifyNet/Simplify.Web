using Simplify.Web.Old;

namespace Simplify.Web.Tests.Old.Core.Controllers.Execution.Controller2ExecutorTestTypes;

public class StringResponseController : Controller2
{
	public virtual Web.Old.ControllerResponse Invoke() => Content("Test!");
}