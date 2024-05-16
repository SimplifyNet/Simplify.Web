using Simplify.Web.Tests.Controllers.V2.Execution.TestTypes;

namespace Simplify.Web.Tests.Controllers.V2.Execution.Controller2ExecutorTestTypes;

public class ModelControllerController : Controller2<TestModel>
{
	public TestModel? CheckModel { get; set; }

	public virtual void Invoke() => CheckModel = Model;
}