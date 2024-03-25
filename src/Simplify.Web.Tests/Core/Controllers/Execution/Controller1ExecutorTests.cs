using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Tests.Core.Controllers.Execution.Controller1TestTypes;

namespace Simplify.Web.Tests.Core.Controllers.Execution;

[TestFixture]
public class Controller1ExecutorTests
{
	private readonly IControllerExecutionArgs _args = Mock.Of<IControllerExecutionArgs>();

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
	public async Task Execute_ControllerWithNoResponse_InvokedAndNull()
	{
		// Arrange

		var response = Mock.Of<ControllerResponse>();

		_controllerFactory.Setup(x =>
			x.CreateController(It.Is<IControllerExecutionArgs>(x => x == _args)))
					.Returns(_syncController.Object);
		// Act
		var result = await _executor.Execute(_args);

		// Assert

		_controllerFactory.Verify(x => x.CreateController(It.Is<IControllerExecutionArgs>(x => x == _args)));

		_syncController.Verify(x => x.Invoke());

		Assert.That(result, Is.Null);
	}

	[Test]
	public async Task Execute_ControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var response = Mock.Of<ControllerResponse>();

		_controllerFactory.Setup(x =>
			x.CreateController(It.Is<IControllerExecutionArgs>(x => x == _args)))
					.Returns(_syncController.Object);

		_syncController.Setup(x => x.Invoke())
			.Returns(response);

		// Act
		var result = await _executor.Execute(_args);

		// Assert

		_controllerFactory.Verify(x => x.CreateController(It.Is<IControllerExecutionArgs>(x => x == _args)));

		_syncController.Verify(x => x.Invoke());

		Assert.That(result, Is.EqualTo(response));
	}

	[Test]
	public async Task Process_ModelControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var response = Mock.Of<ControllerResponse>();

		_controllerFactory.Setup(x =>
			x.CreateController(It.Is<IControllerExecutionArgs>(x => x == _args)))
					.Returns(_syncModelController.Object);

		_syncModelController.Setup(x => x.Invoke())
			.Returns(response);

		// Act
		var result = await _executor.Execute(_args);

		// Assert

		_controllerFactory.Verify(x => x.CreateController(It.Is<IControllerExecutionArgs>(x => x == _args)));

		_syncModelController.Verify(x => x.Invoke());

		Assert.That(result, Is.EqualTo(response));
	}

	[Test]
	public async Task Execute_AsyncControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var response = Mock.Of<ControllerResponse>();

		_controllerFactory.Setup(x =>
			x.CreateController(It.Is<IControllerExecutionArgs>(x => x == _args)))
					.Returns(_asyncController.Object);

		_asyncController.Setup(x => x.Invoke())
			.Returns(Task.FromResult(response)!);

		// Act
		var result = await _executor.Execute(_args);

		// Assert

		_controllerFactory.Verify(x => x.CreateController(It.Is<IControllerExecutionArgs>(x => x == _args)));

		_asyncController.Verify(x => x.Invoke());

		Assert.That(result, Is.EqualTo(response));
	}

	[Test]
	public async Task Process_AsyncModelControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var response = Mock.Of<ControllerResponse>();

		_controllerFactory.Setup(x =>
			x.CreateController(It.Is<IControllerExecutionArgs>(x => x == _args)))
					.Returns(_asyncModelController.Object);

		_asyncModelController.Setup(x => x.Invoke())
			.Returns(Task.FromResult(response)!);

		// Act
		var result = await _executor.Execute(_args);

		// Assert

		_controllerFactory.Verify(x => x.CreateController(It.Is<IControllerExecutionArgs>(x => x == _args)));

		_asyncModelController.Verify(x => x.Invoke());

		Assert.That(result, Is.EqualTo(response));
	}
}