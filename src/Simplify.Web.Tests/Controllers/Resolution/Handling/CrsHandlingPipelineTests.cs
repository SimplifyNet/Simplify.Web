using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.Handling;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Tests.Controllers.Resolution.Handling;

[TestFixture]
public class CrsHandlingPipelineTests
{
	[Test]
	public void Execute_NoMatchingHandlers_False()
	{
		// Arrange
		var pipeline = new CrsHandlingPipeline([Mock.Of<ICrsHandler>()]);

		// Act
		var result = pipeline.Execute(null!, null!);

		// Assert
		Assert.That(result, Is.False);
	}

	[TestCase(true)]
	[TestCase(false)]
	public void Execute_MatchedHandler_IsTerminalCorrect(bool isTerminalHandler)
	{
		// Arrange

		var state = Mock.Of<IControllerResolutionState>();
		var builder = new ExecutionWorkOrderBuilder();
		var handler1 = new Mock<ICrsHandler>();
		var handler2 = new Mock<ICrsHandler>();

		handler1.SetupGet(x => x.IsTerminal).Returns(isTerminalHandler);
		handler1.Setup(x => x.CanHandle(It.Is<IControllerResolutionState>(s => s == state))).Returns(true);

		var pipeline = new CrsHandlingPipeline([handler1.Object, handler2.Object]);

		// Act
		var result = pipeline.Execute(state, builder);

		// Assert

		Assert.That(result, Is.EqualTo(isTerminalHandler));

		handler1.Verify(x => x.Execute(It.Is<IControllerResolutionState>(s => s == state), It.Is<ExecutionWorkOrderBuilder>(b => b == builder)));
		handler2.Verify(x => x.Execute(It.IsAny<IControllerResolutionState>(), It.IsAny<ExecutionWorkOrderBuilder>()), Times.Never);
	}
}