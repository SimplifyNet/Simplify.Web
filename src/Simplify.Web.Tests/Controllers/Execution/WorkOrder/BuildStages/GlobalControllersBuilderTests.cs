using System.Collections.Generic;
using System.Net;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.MetaStore;

namespace Simplify.Web.Tests.Controllers.Execution.WorkOrder.BuildStages;

[TestFixture]
public class GlobalControllersBuilderTests
{
	private readonly GlobalControllersBuilder _stage = new();

	[Test]
	public void Execute_HttpStatusCodeIsNotNull_IgnoredStatusCodeNotChanged()
	{
		// Arrange
		var builder = new ExecutionWorkOrderBuilder
		{
			HttpStatusCode = HttpStatusCode.BadRequest
		};

		// Act
		_stage.Execute(builder, null!);

		// Assert

		Assert.That(builder.HttpStatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		Assert.That(builder.Controllers.Count, Is.Zero);
	}

	[Test]
	public void Execute_ControllersExists_NotFoundNotBuild()
	{
		// Arrange

		var builder = new ExecutionWorkOrderBuilder();
		var controller1 = Mock.Of<IControllerMetadata>();
		var controller2 = Mock.Of<IControllerMetadata>();

		ControllersMetaStore.Current = Mock.Of<IControllersMetaStore>(x => x.GlobalControllers == new List<IControllerMetadata> { controller1, controller2 });

		// Act
		_stage.Execute(builder, null!);

		// Assert

		Assert.That(builder.Controllers.Count, Is.EqualTo(2));
		Assert.That(builder.Controllers[0].Controller, Is.EqualTo(controller1));
		Assert.That(builder.Controllers[1].Controller, Is.EqualTo(controller2));
	}
}
