using System.Threading.Tasks;

namespace Simplify.Web.Tests.Controllers.V2.Execution.TestTypes;

public class TaskResultController : Controller2
{
	public bool Invoked { get; private set; }

	public Task Invoke()
	{
		Invoked = true;

		return Task.CompletedTask;
	}
}