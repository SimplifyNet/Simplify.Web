using System.Net;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.MetaStore;

namespace Simplify.Web.Tests.Controllers.Execution.WorkOrder.BuildStages;

[TestFixture]
public class NotFoundBuilderTests
{
	private readonly NotFoundBuilder _stage = new();

	[Test]
	public void Execute_HttpStatusCodeIsNotNull_NotFoundNotBuild()
	{
		// Arrange
		var builder = new ExecutionWorkOrderBuilder
		{
			HttpStatusCode = HttpStatusCode.BadRequest
		};

		// Act
		_stage.Execute(builder, null!);

		// Assert

		Assert.That(builder.HttpStatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
		Assert.That(builder.Controllers.Count, Is.Zero);
	}

	[Test]
	public void Execute_ControllersExists_NotFoundNotBuild()
	{
		// Arrange

		var builder = new ExecutionWorkOrderBuilder();

		builder.Controllers.Add(Mock.Of<IMatchedController>());

		// Act
		_stage.Execute(builder, null!);

		// Assert

		Assert.That(builder.HttpStatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
		Assert.That(builder.Controllers.Count, Is.EqualTo(1));
	}

	[Test]
	public void Execute_NotFoundControllerIsNotExist_NotFoundStatusSet()
	{
		// Arrange

		var builder = new ExecutionWorkOrderBuilder();

		ControllersMetaStore.Current = Mock.Of<IControllersMetaStore>();

		// Act
		_stage.Execute(builder, null!);

		// Assert

		Assert.That(builder.HttpStatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		Assert.That(builder.Controllers.Count, Is.EqualTo(0));
	}

	[Test]
	public void Execute_NotFoundControllerExist_NotFoundControllerAddedAndStatusNotSet()
	{
		// Arrange

		var builder = new ExecutionWorkOrderBuilder();
		var notFoundController = Mock.Of<IControllerMetadata>();

		ControllersMetaStore.Current = Mock.Of<IControllersMetaStore>(x => x.NotFoundController == notFoundController);

		// Act
		_stage.Execute(builder, null!);

		// Assert

		Assert.That(builder.HttpStatusCode, Is.Null);
		Assert.That(builder.Controllers.Count, Is.EqualTo(1));
		Assert.That(builder.Controllers[0].Controller, Is.EqualTo(notFoundController));
	}
}