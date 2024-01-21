using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Meta;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Core.Controllers.Execution;

[TestFixture]
public class Controller1ExecutorTests
{
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
			x.CreateController(It.IsAny<Type>(),
				It.IsAny<IDIContainerProvider>(),
				It.IsAny<HttpContext>(),
				It.IsAny<IDictionary<string, object>>()))
					.Returns(_syncController.Object);

		// Act
		var result = await _executor.Execute(Mock.Of<IControllerMetaData>(), null!, null!);

		// Assert

		_controllerFactory.Verify(x =>
			x.CreateController(It.IsAny<Type>(),
				It.IsAny<IDIContainerProvider>(),
				It.IsAny<HttpContext>(),
				It.IsAny<IDictionary<string, object>>()));

		_syncController.Verify(x => x.Invoke());

		Assert.That(result, Is.Null);
	}

	[Test]
	public async Task Execute_ControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var response = Mock.Of<ControllerResponse>();

		_controllerFactory.Setup(x =>
			x.CreateController(It.IsAny<Type>(),
				It.IsAny<IDIContainerProvider>(),
				It.IsAny<HttpContext>(),
				It.IsAny<IDictionary<string, object>>()))
					.Returns(_syncController.Object);

		_syncController.Setup(x => x.Invoke())
			.Returns(response);

		// Act
		var result = await _executor.Execute(Mock.Of<IControllerMetaData>(), null!, null!);

		// Assert

		_controllerFactory.Verify(x =>
			x.CreateController(It.IsAny<Type>(),
				It.IsAny<IDIContainerProvider>(),
				It.IsAny<HttpContext>(),
				It.IsAny<IDictionary<string, object>>()));

		_syncController.Verify(x => x.Invoke());

		Assert.That(result, Is.EqualTo(response));
	}

	[Test]
	public async Task Process_ModelControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var response = Mock.Of<ControllerResponse>();

		_controllerFactory.Setup(x =>
			x.CreateController(It.IsAny<Type>(),
				It.IsAny<IDIContainerProvider>(),
				It.IsAny<HttpContext>(),
				It.IsAny<IDictionary<string, object>>()))
					.Returns(_syncModelController.Object);

		_syncModelController.Setup(x => x.Invoke())
			.Returns(response);

		// Act
		var result = await _executor.Execute(Mock.Of<IControllerMetaData>(), null!, null!);

		// Assert

		_controllerFactory.Verify(x =>
			x.CreateController(It.IsAny<Type>(),
				It.IsAny<IDIContainerProvider>(),
				It.IsAny<HttpContext>(),
				It.IsAny<IDictionary<string, object>>()));

		_syncModelController.Verify(x => x.Invoke());

		Assert.That(result, Is.EqualTo(response));
	}


	[Test]
	public async Task Execute_AsyncControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var response = Mock.Of<ControllerResponse>();

		_controllerFactory.Setup(x =>
			x.CreateController(It.IsAny<Type>(),
				It.IsAny<IDIContainerProvider>(),
				It.IsAny<HttpContext>(),
				It.IsAny<IDictionary<string, object>>()))
					.Returns(_asyncController.Object);

		_asyncController.Setup(x => x.Invoke())
			.Returns(Task.FromResult(response)!);

		// Act
		var result = await _executor.Execute(Mock.Of<IControllerMetaData>(), null!, null!);

		// Assert

		_controllerFactory.Verify(x =>
			x.CreateController(It.IsAny<Type>(),
				It.IsAny<IDIContainerProvider>(),
				It.IsAny<HttpContext>(),
				It.IsAny<IDictionary<string, object>>()));

		_asyncController.Verify(x => x.Invoke());

		Assert.That(result, Is.EqualTo(response));
	}

	[Test]
	public async Task Process_AsyncModelControllerWithResponse_InvokedAndResponseReturned()
	{
		// Arrange

		var response = Mock.Of<ControllerResponse>();

		_controllerFactory.Setup(x =>
			x.CreateController(It.IsAny<Type>(),
				It.IsAny<IDIContainerProvider>(),
				It.IsAny<HttpContext>(),
				It.IsAny<IDictionary<string, object>>()))
					.Returns(_asyncModelController.Object);

		_asyncModelController.Setup(x => x.Invoke())
			.Returns(Task.FromResult(response)!);

		// Act
		var result = await _executor.Execute(Mock.Of<IControllerMetaData>(), null!, null!);

		// Assert

		_controllerFactory.Verify(x =>
			x.CreateController(It.IsAny<Type>(),
				It.IsAny<IDIContainerProvider>(),
				It.IsAny<HttpContext>(),
				It.IsAny<IDictionary<string, object>>()));

		_asyncModelController.Verify(x => x.Invoke());

		Assert.That(result, Is.EqualTo(response));
	}
}