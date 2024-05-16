using System.Net;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.MetaStore;
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
	public void Execute_ForbiddenControllerIsNotExists_ControllersClearedAndHttpStatusCodeSetToForbidden()
	{
		// Arrange

		var state = Mock.Of<IControllerResolutionState>();
		var builder = new ExecutionWorkOrderBuilder();

		builder.Controllers.Add(Mock.Of<IMatchedController>());

		ControllersMetaStore.Current = Mock.Of<IControllersMetaStore>();

		// Act
		_handler.Execute(state, builder);

		// Assert

		Assert.That(builder.Controllers, Is.Empty);
		Assert.That(builder.HttpStatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
	}

	[Test]
	public void Execute_ForbiddenControllerExists_ControllersClearedForbiddenControllerAddedAndHttpStatusCodeNotSet()
	{
		// Arrange

		var state = Mock.Of<IControllerResolutionState>();
		var builder = new ExecutionWorkOrderBuilder();

		builder.Controllers.Add(Mock.Of<IMatchedController>());

		var forbiddenController = Mock.Of<IControllerMetadata>();

		ControllersMetaStore.Current = Mock.Of<IControllersMetaStore>(x => x.ForbiddenController == forbiddenController);

		// Act
		_handler.Execute(state, builder);

		// Assert

		Assert.That(builder.HttpStatusCode, Is.Null);
		Assert.That(builder.Controllers.Count, Is.EqualTo(1));
		Assert.That(builder.Controllers[0].Controller, Is.EqualTo(forbiddenController));
	}
}