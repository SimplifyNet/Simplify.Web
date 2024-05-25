using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Execution.WorkOrder.Director;

namespace Simplify.Web.Tests.Controllers.Execution.WorkOrder.Director;

[TestFixture]
public class ExecutionWorkOrderBuildDirectorTests
{
	[Test]
	public void CreateWorkOrder_TwoStages_BothExecuted()
	{
		// Arrange

		var stage1 = new Mock<IExecutionWorkOrderBuildStage>();
		var stage2 = new Mock<IExecutionWorkOrderBuildStage>();
		var context = Mock.Of<HttpContext>();

		var director = new ExecutionWorkOrderBuildDirector([stage1.Object, stage2.Object]);

		// Act
		var result = director.CreateWorkOrder(context);

		// Assert

		Assert.That(result, Is.Not.Null);

		stage1.Verify(x => x.Execute(It.IsAny<ExecutionWorkOrderBuilder>(), It.Is<HttpContext>(c => c == context)));
		stage2.Verify(x => x.Execute(It.IsAny<ExecutionWorkOrderBuilder>(), It.Is<HttpContext>(c => c == context)));
	}
}