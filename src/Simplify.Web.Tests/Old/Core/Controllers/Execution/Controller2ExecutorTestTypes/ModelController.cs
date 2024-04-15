using Moq;
using Simplify.Web.Old;
using Simplify.Web.Tests.Old.Core.Controllers.Execution.TestTypes;

namespace Simplify.Web.Tests.Old.Core.Controllers.Execution.Controller2ExecutorTestTypes;

public class ModelControllerController : Controller2<TestModel>
{
	public virtual void Invoke() => CheckModel = Model;

	public TestModel? CheckModel { get; set; }
}