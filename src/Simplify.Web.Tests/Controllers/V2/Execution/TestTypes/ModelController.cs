using Simplify.Web.Tests.Controllers.V2.Execution.TestModels;

namespace Simplify.Web.Tests.Controllers.V2.Execution.TestTypes;

public class ModelController : Controller2<TestModel>
{
	public bool Invoked { get; private set; }

	public virtual void Invoke() => Invoked = true;
}