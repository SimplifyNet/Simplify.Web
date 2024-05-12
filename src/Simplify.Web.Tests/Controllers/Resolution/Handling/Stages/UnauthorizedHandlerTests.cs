using System.Net;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.Handling.Stages;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Tests.Controllers.Resolution.Handling.Stages;

[TestFixture]
public class UnauthorizedHandlerTests
{
	private readonly UnauthorizedHandler _handler = new();

	[Test]
	public void CanHandle_SecurityStatusIsUnauthorized_True()
	{
		// Arrange
		var state = Mock.Of<IControllerResolutionState>(x => x.SecurityStatus == Web.Controllers.Security.SecurityStatus.Unauthorized);

		// Act
		var result = _handler.CanHandle(state);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void CanHandle_SecurityStatusIsNotUnauthorized_False()
	{
		// Arrange
		var state = Mock.Of<IControllerResolutionState>(x => x.SecurityStatus == Web.Controllers.Security.SecurityStatus.Ok);

		// Act
		var result = _handler.CanHandle(state);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void Execute_HttpStatusCodeSetToUnauthorized()
	{
		// Arrange

		var state = Mock.Of<IControllerResolutionState>();
		var builder = new ExecutionWorkOrderBuilder();

		// Act
		_handler.Execute(state, builder);

		// Assert
		Assert.That(builder.HttpStatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
	}
}
