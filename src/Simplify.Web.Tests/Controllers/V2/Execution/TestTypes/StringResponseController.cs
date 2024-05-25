namespace Simplify.Web.Tests.Controllers.V2.Execution.TestTypes;

public class StringResponseController : Controller2
{
	public bool Invoked { get; private set; }

	public virtual ControllerResponse Invoke()
	{
		Invoked = true;

		return Content("Foo");
	}
}