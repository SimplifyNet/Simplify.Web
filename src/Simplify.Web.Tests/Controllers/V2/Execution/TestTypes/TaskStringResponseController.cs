using System.Threading.Tasks;

namespace Simplify.Web.Tests.Controllers.V2.Execution.TestTypes;

public class TaskStringResponseController : Controller2
{
	public bool Invoked { get; private set; }

	public Task<ControllerResponse> Invoke()
	{
		Invoked = true;

		return Task.FromResult((ControllerResponse)Content("Foo"));
	}
}