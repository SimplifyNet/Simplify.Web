using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Execution.WorkOrder.Director;
using Simplify.Web.RequestHandling;
using Simplify.Web.RequestHandling.Handlers;

namespace Simplify.Web.Tests.RequestHandling.Handlers;

[TestFixture]
public class ControllersHandlerTests
{
	private Mock<IExecutionWorkOrderBuildDirector> _buildDirector = null!;
	private Mock<IControllersExecutor> _controllersExecutor = null!;

	private ControllersHandler _handler = null!;

	[SetUp]
	public void Initialize()
	{
		_buildDirector = new Mock<IExecutionWorkOrderBuildDirector>();
		_controllersExecutor = new Mock<IControllersExecutor>();

		_handler = new ControllersHandler(_buildDirector.Object, _controllersExecutor.Object);
	}

	[Test]
	public async Task HandleAsync_HttpStatusCodeIsNotNull_ReturnedNotExecuted()
	{
		// Arrange

		var context = Mock.Of<HttpContext>(x => x.Response.StatusCode == 0);
		var next = new Mock<RequestHandlerAsync>();

		_buildDirector.Setup(x => x.CreateWorkOrder(It.Is<HttpContext>(c => c == context)))
			.Returns(Mock.Of<IExecutionWorkOrder>(x => x.HttpStatusCode == HttpStatusCode.Unauthorized));

		// Act
		await _handler.HandleAsync(context, next.Object);

		// Assert

		Assert.That(context.Response.StatusCode, Is.EqualTo(401));

		_controllersExecutor.Verify(x => x.ExecuteAsync(It.IsAny<IReadOnlyList<IMatchedController>>()), Times.Never);
		next.Verify(x => x.Invoke(), Times.Never);
	}

	[Test]
	public async Task HandleAsync_NoHttpStatusCodeDefaultResultOnExecution_ExecutedAndCalledNext()
	{
		// Arrange

		var context = Mock.Of<HttpContext>();
		var controllers = new List<IMatchedController>();
		var next = new Mock<RequestHandlerAsync>();

		_buildDirector.Setup(x => x.CreateWorkOrder(It.Is<HttpContext>(c => c == context)))
			.Returns(Mock.Of<IExecutionWorkOrder>(x => x.Controllers == controllers));

		_controllersExecutor.Setup(x => x.ExecuteAsync(It.Is<IReadOnlyList<IMatchedController>>(c => c == controllers)))
			.ReturnsAsync(ResponseBehavior.Default);

		// Act
		await _handler.HandleAsync(context, next.Object);

		// Assert

		_controllersExecutor.Verify(x => x.ExecuteAsync(It.Is<IReadOnlyList<IMatchedController>>(c => c == controllers)));
		next.Verify(x => x.Invoke());
	}

	[TestCase(ResponseBehavior.RawOutput)]
	[TestCase(ResponseBehavior.Redirect)]
	public async Task HandleAsync_NoHttpStatusCodeAndNonDefaultResultOnExecution_ExecutedAndNonDefaultResultIsReturned(ResponseBehavior behavior)
	{
		// Arrange

		var context = Mock.Of<HttpContext>();
		var controllers = new List<IMatchedController>();
		var next = new Mock<RequestHandlerAsync>();

		_buildDirector.Setup(x => x.CreateWorkOrder(It.Is<HttpContext>(c => c == context)))
			.Returns(Mock.Of<IExecutionWorkOrder>(x => x.Controllers == controllers));

		_controllersExecutor.Setup(x => x.ExecuteAsync(It.Is<IReadOnlyList<IMatchedController>>(c => c == controllers)))
			.ReturnsAsync(behavior);

		// Act
		await _handler.HandleAsync(context, next.Object);

		// Assert

		_controllersExecutor.Verify(x => x.ExecuteAsync(It.Is<IReadOnlyList<IMatchedController>>(c => c == controllers)));
		next.Verify(x => x.Invoke(), Times.Never);
	}
}