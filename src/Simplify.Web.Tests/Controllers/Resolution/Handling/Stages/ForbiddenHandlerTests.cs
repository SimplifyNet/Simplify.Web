using System.Net;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.Handling.Stages;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Tests.Controllers.Resolution.Handling.Stages;

[TestFixture]
public class ForbiddenHandlerTests
{
	private readonly ForbiddenHandler _handler = new();

	[Test]
	public void CanHandle_SecurityStatusIsForbidden_True()
	{
		// Arrange
		var state = Mock.Of<IControllerResolutionState>(x => x.SecurityStatus == Web.Controllers.Security.SecurityStatus.Forbidden);

		// Act
		var result = _handler.CanHandle(state);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void CanHandle_SecurityStatusIsNotForbidden_False()
	{
		// Arrange
		var state = Mock.Of<IControllerResolutionState>(x => x.SecurityStatus == Web.Controllers.Security.SecurityStatus.Ok);

		// Act
		var result = _handler.CanHandle(state);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void Execute_HttpStatusCodeSetToForbidden()
	{
		// Arrange

		var state = Mock.Of<IControllerResolutionState>();
		var builder = new ExecutionWorkOrderBuilder();

		// Act
		_handler.Execute(state, builder);

		// Assert
		Assert.That(builder.HttpStatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
	}
}
