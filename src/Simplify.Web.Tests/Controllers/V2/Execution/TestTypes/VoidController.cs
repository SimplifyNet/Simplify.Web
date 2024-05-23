namespace Simplify.Web.Tests.Controllers.V2.Execution.TestTypes;

public class VoidController : Controller2
{
	public bool Invoked { get; private set; }

	public void Invoke() => Invoked = true;
}