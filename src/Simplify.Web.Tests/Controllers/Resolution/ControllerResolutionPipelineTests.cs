using System;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.Resolution.State;
using Simplify.Web.Controllers.V1.Metadata;

namespace Simplify.Web.Tests.Controllers.Resolution;

[TestFixture]
public class ControllerResolutionPipelineTests
{
	private readonly IControllerMetadata _initialController = Mock.Of<IController1Metadata>();
	private readonly HttpContext _context = Mock.Of<HttpContext>();

	private ControllerResolutionPipeline _pipeline = null!;

	[Test]
	public void Execute_TwoStage_BothExecuted()
	{
		// Arrange

		var stage1 = new Mock<IControllerResolutionStage>();
		var stage2 = new Mock<IControllerResolutionStage>();

		_pipeline = new ControllerResolutionPipeline([stage1.Object, stage2.Object]);

		// Act
		var result = _pipeline.Execute(_initialController, _context);

		// Assert

		Assert.That(result.Controller, Is.EqualTo(_initialController));

		stage1.Verify(x => x.Execute(It.IsAny<IControllerResolutionState>(), It.Is<HttpContext>(c => c == _context), It.IsAny<Action>()));
		stage2.Verify(x => x.Execute(It.IsAny<IControllerResolutionState>(), It.Is<HttpContext>(c => c == _context), It.IsAny<Action>()));
	}

	[Test]
	public void Execute_ThreeStagesSecondStopProcessing_TwoExecuted()
	{
		// Arrange

		var stage1 = new Mock<IControllerResolutionStage>();
		var stage2 = new Mock<IControllerResolutionStage>();
		var stage3 = new Mock<IControllerResolutionStage>();

		stage2.Setup(x => x.Execute(It.IsAny<IControllerResolutionState>(), It.Is<HttpContext>(c => c == _context), It.IsAny<Action>()))
			.Callback<IControllerResolutionState, HttpContext, Action>((state, context, stopProcessing) =>
			 {
				 stopProcessing();
			 });

		_pipeline = new ControllerResolutionPipeline([stage1.Object, stage2.Object, stage3.Object]);

		// Act
		var result = _pipeline.Execute(_initialController, _context);

		// Assert

		Assert.That(result.Controller, Is.EqualTo(_initialController));

		stage1.Verify(x => x.Execute(It.IsAny<IControllerResolutionState>(), It.Is<HttpContext>(c => c == _context), It.IsAny<Action>()));
		stage2.Verify(x => x.Execute(It.IsAny<IControllerResolutionState>(), It.Is<HttpContext>(c => c == _context), It.IsAny<Action>()));
		stage3.Verify(x => x.Execute(It.IsAny<IControllerResolutionState>(), It.Is<HttpContext>(c => c == _context), It.IsAny<Action>()), Times.Never);
	}
}