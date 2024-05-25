namespace Simplify.Web.Tests.Controllers.V2.Execution.TestTypes;

public class NullResponseController : Controller2
{
	public bool Invoked { get; private set; }

	public ControllerResponse? Invoke()
	{
		Invoked = true;

		return null;
	}
}