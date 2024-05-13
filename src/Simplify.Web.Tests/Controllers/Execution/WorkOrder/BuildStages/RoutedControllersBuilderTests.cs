using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.MetaStore;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.Resolution.Handling;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Tests.Controllers.Execution.WorkOrder.BuildStages;

[TestFixture]
public class RoutedControllersBuilderTests
{
	private RoutedControllersBuilder _stage = null!;

	private Mock<IControllerResolutionPipeline> _resolutionPipeline = null!;
	private Mock<ICrsHandlingPipeline> _crsHandlingPipeline = null!;

	[SetUp]
	public void Initialize()
	{
		_resolutionPipeline = new Mock<IControllerResolutionPipeline>();
		_crsHandlingPipeline = new Mock<ICrsHandlingPipeline>();
		_stage = new RoutedControllersBuilder(_resolutionPipeline.Object, _crsHandlingPipeline.Object);
	}

	[Test]
	public void Execute_TwoControllers_AllParametersPassedControllersProcessed()
	{
		// Arrange

		var builder = new ExecutionWorkOrderBuilder();
		var context = Mock.Of<HttpContext>();
		var controller1 = Mock.Of<IControllerMetadata>();
		var controller2 = Mock.Of<IControllerMetadata>();

		var crs1 = Mock.Of<IControllerResolutionState>();
		var crs2 = Mock.Of<IControllerResolutionState>();

		ControllersMetaStore.Current = Mock.Of<IControllersMetaStore>(x => x.RoutedControllers == new List<IControllerMetadata> { controller1, controller2 });

		_resolutionPipeline.Setup(x => x.Execute(It.Is<IControllerMetadata>(c => c == controller1), It.Is<HttpContext>(c => c == context))).Returns(crs1);
		_resolutionPipeline.Setup(x => x.Execute(It.Is<IControllerMetadata>(c => c == controller2), It.Is<HttpContext>(c => c == context))).Returns(crs2);

		// Act
		_stage.Execute(builder, context);

		// Assert

		_resolutionPipeline.Verify(x => x.Execute(It.Is<IControllerMetadata>(c => c == controller1), It.Is<HttpContext>(c => c == context)));
		_resolutionPipeline.Verify(x => x.Execute(It.Is<IControllerMetadata>(c => c == controller2), It.Is<HttpContext>(c => c == context)));
		_crsHandlingPipeline.Verify(x => x.Execute(It.Is<IControllerResolutionState>(s => s == crs1), It.Is<ExecutionWorkOrderBuilder>(b => b == builder)));
		_crsHandlingPipeline.Verify(x => x.Execute(It.Is<IControllerResolutionState>(s => s == crs2), It.Is<ExecutionWorkOrderBuilder>(b => b == builder)));
	}


	[Test]
	public void Execute_TwoControllersCrsHandlingPipelineTerminatedOnFirstController_OnlyFirstControllerProcessed()
	{
		// Arrange

		var builder = new ExecutionWorkOrderBuilder();
		var context = Mock.Of<HttpContext>();
		var controller1 = Mock.Of<IControllerMetadata>();
		var controller2 = Mock.Of<IControllerMetadata>();

		var crs1 = Mock.Of<IControllerResolutionState>();
		var crs2 = Mock.Of<IControllerResolutionState>();

		ControllersMetaStore.Current = Mock.Of<IControllersMetaStore>(x => x.RoutedControllers == new List<IControllerMetadata> { controller1, controller2 });

		_resolutionPipeline.Setup(x => x.Execute(It.Is<IControllerMetadata>(c => c == controller1), It.Is<HttpContext>(c => c == context))).Returns(crs1);
		_resolutionPipeline.Setup(x => x.Execute(It.Is<IControllerMetadata>(c => c == controller2), It.Is<HttpContext>(c => c == context))).Returns(crs2);

		_crsHandlingPipeline.Setup(x => x.Execute(It.Is<IControllerResolutionState>(s => s == crs1), It.Is<ExecutionWorkOrderBuilder>(b => b == builder))).Returns(true);

		// Act
		_stage.Execute(builder, context);

		// Assert

		_resolutionPipeline.Verify(x => x.Execute(It.Is<IControllerMetadata>(c => c == controller1), It.Is<HttpContext>(c => c == context)));
		_resolutionPipeline.Verify(x => x.Execute(It.Is<IControllerMetadata>(c => c == controller2), It.Is<HttpContext>(c => c == context)), Times.Never);
		_crsHandlingPipeline.Verify(x => x.Execute(It.Is<IControllerResolutionState>(s => s == crs1), It.Is<ExecutionWorkOrderBuilder>(b => b == builder)));
		_crsHandlingPipeline.Verify(x => x.Execute(It.Is<IControllerResolutionState>(s => s == crs2), It.Is<ExecutionWorkOrderBuilder>(b => b == builder)), Times.Never);
	}
}
