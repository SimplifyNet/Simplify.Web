using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Execution.Resolver;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Response;

namespace Simplify.Web.Tests.Controllers.Execution;

[TestFixture]
public class ControllersExecutorTests
{
	private readonly IControllerMetadata _controllerMetadata1 = Mock.Of<IControllerMetadata>();
	private readonly IControllerMetadata _controllerMetadata2 = Mock.Of<IControllerMetadata>();
	private readonly IControllerMetadata _controllerMetadata3 = Mock.Of<IControllerMetadata>();

	private IMatchedController _controller1 = null!;
	private IMatchedController _controller2 = null!;
	private IMatchedController _controller3 = null!;

	private ControllerResponse _controllerResponse1 = null!;
	private ControllerResponse _controllerResponse2 = null!;

	private Mock<IControllerExecutorResolver> _resolver = null!;

	private Mock<IControllerExecutor> _controllerExecutor1 = null!;
	private Mock<IControllerExecutor> _controllerExecutor2 = null!;

	private Mock<IControllerResponseExecutor> _responseExecutor = null!;

	[SetUp]
	public void Initialize()
	{
		_controller1 = Mock.Of<IMatchedController>(x => x.Controller == _controllerMetadata1);
		_controller2 = Mock.Of<IMatchedController>(x => x.Controller == _controllerMetadata2);
		_controller3 = Mock.Of<IMatchedController>(x => x.Controller == _controllerMetadata3);

		_resolver = new Mock<IControllerExecutorResolver>();

		_controllerExecutor1 = new Mock<IControllerExecutor>();
		_controllerExecutor2 = new Mock<IControllerExecutor>();

		_controllerResponse1 = Mock.Of<ControllerResponse>();
		_controllerResponse2 = Mock.Of<ControllerResponse>();

		_responseExecutor = new Mock<IControllerResponseExecutor>();
	}

	[Test]
	public async Task ExecuteAsync_ControllerWithNoResponse_ExecutedDefaultBehavior()
	{
		// Arrange

		var controllers = new List<IMatchedController> { _controller1 };

		_resolver.Setup(x => x.Resolve(It.Is<IControllerMetadata>(m => m == _controllerMetadata1)))
			.Returns(_controllerExecutor1.Object);

		var executor = new ControllersExecutor(_resolver.Object, _responseExecutor.Object);

		// Act
		var result = await executor.ExecuteAsync(controllers);

		// Assert

		Assert.That(result, Is.EqualTo(ResponseBehavior.Default));

		_controllerExecutor1.Verify(x => x.ExecuteAsync(It.Is<IMatchedController>(c => c == _controller1)));
		_responseExecutor.Verify(x => x.ExecuteAsync(It.IsAny<ControllerResponse>()), Times.Never);
	}

	[Test]
	public async Task ExecuteAsync_ControllerWithRawOutputBehavior_ExecutedRawOutputBehavior()
	{
		// Arrange

		var controllers = new List<IMatchedController> { _controller1 };

		_resolver.Setup(x => x.Resolve(It.Is<IControllerMetadata>(m => m == _controllerMetadata1)))
			.Returns(_controllerExecutor1.Object);

		_controllerExecutor1.Setup(x => x.ExecuteAsync(It.Is<IMatchedController>(c => c == _controller1)))
			.ReturnsAsync(_controllerResponse1);

		_responseExecutor.Setup(x => x.ExecuteAsync(It.Is<ControllerResponse>(r => r == _controllerResponse1)))
			.ReturnsAsync(ResponseBehavior.RawOutput);

		var executor = new ControllersExecutor(_resolver.Object, _responseExecutor.Object);

		// Act
		var result = await executor.ExecuteAsync(controllers);

		// Assert

		Assert.That(result, Is.EqualTo(ResponseBehavior.RawOutput));

		_controllerExecutor1.Verify(x => x.ExecuteAsync(It.Is<IMatchedController>(c => c == _controller1)));
		_responseExecutor.Verify(x => x.ExecuteAsync(It.Is<ControllerResponse>(r => r == _controllerResponse1)));
	}

	[TestCase(ResponseBehavior.RawOutput)]
	[TestCase(ResponseBehavior.Redirect)]
	public async Task ExecuteAsync_ThreeControllersSecondNonDefaultBehavior_TwoExecutedNonDefaultResponse(ResponseBehavior secondControllerResult)
	{
		// Arrange

		var controllers = new List<IMatchedController> { _controller1, _controller2, _controller3 };

		_resolver.Setup(x => x.Resolve(It.Is<IControllerMetadata>(m => m == _controllerMetadata1)))
			.Returns(_controllerExecutor1.Object);

		_resolver.Setup(x => x.Resolve(It.Is<IControllerMetadata>(m => m == _controllerMetadata2)))
			.Returns(_controllerExecutor2.Object);

		_controllerExecutor1.Setup(x => x.ExecuteAsync(It.Is<IMatchedController>(c => c == _controller1)))
			.ReturnsAsync(_controllerResponse1);

		_controllerExecutor2.Setup(x => x.ExecuteAsync(It.Is<IMatchedController>(c => c == _controller2)))
			.ReturnsAsync(_controllerResponse2);

		_responseExecutor.Setup(x => x.ExecuteAsync(It.Is<ControllerResponse>(r => r == _controllerResponse1)));
		_responseExecutor.Setup(x => x.ExecuteAsync(It.Is<ControllerResponse>(r => r == _controllerResponse2))).ReturnsAsync(secondControllerResult);

		var executor = new ControllersExecutor(_resolver.Object, _responseExecutor.Object);

		// Act
		var result = await executor.ExecuteAsync(controllers);

		// Assert

		Assert.That(result, Is.EqualTo(secondControllerResult));

		_controllerExecutor1.Verify(x => x.ExecuteAsync(It.Is<IMatchedController>(c => c == _controller1)));
		_controllerExecutor2.Verify(x => x.ExecuteAsync(It.Is<IMatchedController>(c => c == _controller2)));

		_responseExecutor.Verify(x => x.ExecuteAsync(It.Is<ControllerResponse>(r => r == _controllerResponse1)));
		_responseExecutor.Verify(x => x.ExecuteAsync(It.Is<ControllerResponse>(r => r == _controllerResponse2)));
	}
}