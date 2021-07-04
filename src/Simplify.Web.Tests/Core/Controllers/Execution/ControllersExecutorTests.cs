using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Core.Controllers.Execution.Building;
using Simplify.Web.Meta;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Core.Controllers.Execution
{
	[TestFixture]
	public class ControllersExecutorTests
	{
		private ControllerExecutor _executor = null!;
		private Mock<IControllerFactory> _controllerFactory = null!;
		private Mock<IControllerResponseBuilder> _controllerResponseBuilder = null!;

		private Mock<Controller> _syncController = null!;

		private Mock<AsyncController> _asyncController = null!;
		private Mock<Controller<TestModel>> _syncModelController = null!;

		private Mock<ControllerResponse> _controllerResponse = null!;

		[SetUp]
		public void Initialize()
		{
			_controllerFactory = new Mock<IControllerFactory>();
			_controllerResponseBuilder = new Mock<IControllerResponseBuilder>();
			_executor = new ControllerExecutor(_controllerFactory.Object, _controllerResponseBuilder.Object);

			_syncController = new Mock<Controller>();
			_asyncController = new Mock<AsyncController>();
			_syncModelController = new Mock<Controller<TestModel>>();
			_controllerResponse = new Mock<ControllerResponse>();
		}

		[Test]
		public async Task Process_StandardControllerNoResponse_CreatedDefaultReturned()
		{
			// Assign
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_syncController.Object);
			// Act
			var result = await _executor.Execute(Mock.Of<IControllerMetaData>(), null!, null!);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Default, result);
			_controllerFactory.Verify(
			x =>
				x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
					It.IsAny<IDictionary<string, object>>()));
			_syncController.Verify(x => x.Invoke());
		}

		[Test]
		public async Task Process_StandardControllerHaveDefaultResponse_CreatedProcessedDefaultReturned()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(Task.FromResult(ControllerResponseResult.Default));
			_syncController.Setup(x => x.Invoke()).Returns(_controllerResponse.Object);
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_syncController.Object);
			// Act
			var result = await _executor.Execute(Mock.Of<IControllerMetaData>(), null!, null!);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Default, result);
			_controllerFactory.Verify(
			x =>
				x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
					It.IsAny<IDictionary<string, object>>()));
			_syncController.Verify(x => x.Invoke());
			_controllerResponse.Setup(x => x.Process());
		}

		[Test]
		public async Task Process_StandardControllerHaveRawDataResponse_CreatedProcessedRawDataReturned()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(Task.FromResult(ControllerResponseResult.RawOutput));
			_syncController.Setup(x => x.Invoke()).Returns(_controllerResponse.Object);
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_syncController.Object);
			// Act
			var result = await _executor.Execute(Mock.Of<IControllerMetaData>(), null!, null!);

			// Assert

			Assert.AreEqual(ControllerResponseResult.RawOutput, result);
			_controllerFactory.Verify(
			x =>
				x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
					It.IsAny<IDictionary<string, object>>()));
			_syncController.Verify(x => x.Invoke());
			_controllerResponse.Setup(x => x.Process());
		}

		[Test]
		public async Task Process_AsyncControllerHaveRawDataResponse_CreatedDefaultReturnedRawDataReturnedAfterGetProcessResponse()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(Task.FromResult(ControllerResponseResult.RawOutput));
			_asyncController.Setup(x => x.Invoke()).Returns(Task.FromResult(_controllerResponse.Object)!);
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_asyncController.Object);
			// Act
			var result = await _executor.Execute(Mock.Of<IControllerMetaData>(), null!, null!);

			// Assert

			Assert.AreEqual(ControllerResponseResult.RawOutput, result);
			_controllerFactory.Verify(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>()));
			_asyncController.Verify(x => x.Invoke());
			_controllerResponse.Setup(x => x.Process());
		}

		[Test]
		public async Task Process_ModelControllerHaveDefaultResponse_CreatedProcessedDefaultReturned()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(Task.FromResult(ControllerResponseResult.Default));
			_syncModelController.Setup(x => x.Invoke()).Returns(_controllerResponse.Object);
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_syncModelController.Object);
			// Act
			var result = await _executor.Execute(Mock.Of<IControllerMetaData>(), null!, null!);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Default, result);
			_controllerFactory.Verify(
			x =>
				x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
					It.IsAny<IDictionary<string, object>>()));
			_syncModelController.Verify(x => x.Invoke());
			_controllerResponse.Setup(x => x.Process());
		}
	}
}