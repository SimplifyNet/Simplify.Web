using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers;
using Simplify.Web.Controllers.V1.Execution;
using Simplify.Web.Tests.Controllers.V1.Execution.TestModels;

namespace Simplify.Web.Tests.Controllers.V1.Execution;

[TestFixture]
public class Controller1ExecutorTests
{
	private readonly ControllerResponse _controllerResponse = Mock.Of<ControllerResponse>();

	private Controller1Executor _executor = null!;

	private Mock<IController1Factory> _controllerFactory = null!;

	private Mock<Controller> _syncController = null!;
	private Mock<AsyncController> _asyncController = null!;
	private Mock<Controller<TestModel>> _syncModelController = null!;
	private Mock<AsyncController<TestModel>> _asyncModelController = null!;

	[SetUp]
	public void Initialize()
	{
		_controllerFactory = new Mock<IController1Factory>();

		_executor = new Controller1Executor(_controllerFactory.Object);

		_syncController = new Mock<Controller>();
		_asyncController = new Mock<AsyncController>();
		_syncModelController = new Mock<Controller<TestModel>>();
		_asyncModelController = new Mock<AsyncController<TestModel>>();
	}

	[Test]
	public async Task ExecuteAsync_SyncControllerWithNoResponse_InvokedAndNull()
	{
		// Arrange

		var matchedController = Mock.Of<IMatchedController>();

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == matchedController)))
			.Returns(_syncController.Object);

		// Act
		var result = await _executor.ExecuteAsync(matchedController);

		// Assert

		Assert.That(result, Is.Null);

		_syncController.Verify(x => x.Invoke());
		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(x => x == matchedController)));
	}

	[Test]
	public async Task ExecuteAsync_ControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var matchedController = Mock.Of<IMatchedController>();

		_syncController.Setup(x => x.Invoke()).Returns(_controllerResponse);

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == matchedController)))
			.Returns(_syncController.Object);

		// Act
		var result = await _executor.ExecuteAsync(matchedController);

		// Assert

		Assert.That(result, Is.EqualTo(_controllerResponse));

		_syncController.Verify(x => x.Invoke());
		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(x => x == matchedController)));
	}

	[Test]
	public async Task Process_ModelControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var matchedController = Mock.Of<IMatchedController>();

		_syncModelController.Setup(x => x.Invoke()).Returns(_controllerResponse);

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == matchedController)))
			.Returns(_syncModelController.Object);

		// Act
		var result = await _executor.ExecuteAsync(matchedController);

		// Assert

		Assert.That(result, Is.EqualTo(_controllerResponse));

		_syncModelController.Verify(x => x.Invoke());
		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(x => x == matchedController)));
	}

	[Test]
	public async Task ExecuteAsync_AsyncControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var matchedController = Mock.Of<IMatchedController>();

		_asyncController.Setup(x => x.Invoke()).ReturnsAsync(_controllerResponse);

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == matchedController)))
			.Returns(_asyncController.Object);

		// Act
		var result = await _executor.ExecuteAsync(matchedController);

		// Assert

		Assert.That(result, Is.EqualTo(_controllerResponse));

		_asyncController.Verify(x => x.Invoke());
		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(x => x == matchedController)));
	}

	[Test]
	public async Task ExecuteAsync_AsyncModelControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var matchedController = Mock.Of<IMatchedController>();

		_asyncModelController.Setup(x => x.Invoke()).ReturnsAsync(_controllerResponse);

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == matchedController)))
			.Returns(_asyncModelController.Object);

		// Act
		var result = await _executor.ExecuteAsync(matchedController);

		// Assert

		Assert.That(result, Is.EqualTo(_controllerResponse));

		_asyncModelController.Verify(x => x.Invoke());
		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(x => x == matchedController)));
	}
}