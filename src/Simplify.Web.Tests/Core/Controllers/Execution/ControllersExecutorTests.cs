#nullable disable

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
		private ControllerExecutor _executor;
		private Mock<IControllerFactory> _controllerFactory;
		private Mock<IControllerResponseBuilder> _controllerResponseBuilder;

		private Mock<Controller> _syncController;
		private Mock<AsyncController> _asyncController;
		private Mock<Controller<TestModel>> _syncModelController;
		private Mock<AsyncController<TestModel>> _asyncModelController;
		private Mock<ControllerResponse> _controllerResponse;

		[SetUp]
		public void Initialize()
		{
			_controllerFactory = new Mock<IControllerFactory>();
			_controllerResponseBuilder = new Mock<IControllerResponseBuilder>();
			_executor = new ControllerExecutor(_controllerFactory.Object, _controllerResponseBuilder.Object);

			_syncController = new Mock<Controller>();
			_asyncController = new Mock<AsyncController>();
			_syncModelController = new Mock<Controller<TestModel>>();
			_asyncModelController = new Mock<AsyncController<TestModel>>();
			_controllerResponse = new Mock<ControllerResponse>();
		}

		[Test]
		public void Process_StandardControllerNoResponse_CreatedDefaultReturned()
		{
			// Assign
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_syncController.Object);
			// Act
			var result = _executor.Execute(Mock.Of<IControllerMetaData>(), null, null);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Default, result);
			_controllerFactory.Verify(
			x =>
				x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
					It.IsAny<IDictionary<string, object>>()));
			_syncController.Verify(x => x.Invoke());
		}

		[Test]
		public void Process_StandardControllerHaveDefaultResponse_CreatedProcessedDefaultReturned()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(ControllerResponseResult.Default);
			_syncController.Setup(x => x.Invoke()).Returns(_controllerResponse.Object);
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_syncController.Object);
			// Act
			var result = _executor.Execute(Mock.Of<IControllerMetaData>(), null, null);

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
		public void Process_StandardControllerHaveRawDataResponse_CreatedProcessedRawDataReturned()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(ControllerResponseResult.RawOutput);
			_syncController.Setup(x => x.Invoke()).Returns(_controllerResponse.Object);
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_syncController.Object);
			// Act
			var result = _executor.Execute(Mock.Of<IControllerMetaData>(), null, null);

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
		public void Process_AsyncControllerHaveRawDataResponse_CreatedDefaultReturnedRawDataReturnedAfterGetProcessResponse()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(ControllerResponseResult.RawOutput);
			_asyncController.Setup(x => x.Invoke()).Returns(Task.FromResult(_controllerResponse.Object));
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_asyncController.Object);
			// Act
			var result = _executor.Execute(Mock.Of<IControllerMetaData>(), null, null);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Default, result);
			_asyncController.Verify(x => x.Invoke());
			_controllerResponse.Verify(x => x.Process(), Times.Never());
			_controllerFactory.Verify(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>()));

			// Act & Assert

			foreach (var response in _executor.ProcessAsyncControllersResponses(null))
			{
				_controllerResponse.Verify(x => x.Process());
				Assert.AreEqual(ControllerResponseResult.RawOutput, response);
			}
		}

		[Test]
		public void Process_ModelControllerHaveDefaultResponse_CreatedProcessedDefaultReturned()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(ControllerResponseResult.Default);
			_syncModelController.Setup(x => x.Invoke()).Returns(_controllerResponse.Object);
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_syncModelController.Object);
			// Act
			var result = _executor.Execute(Mock.Of<IControllerMetaData>(), null, null);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Default, result);
			_controllerFactory.Verify(
			x =>
				x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
					It.IsAny<IDictionary<string, object>>()));
			_syncModelController.Verify(x => x.Invoke());
			_controllerResponse.Setup(x => x.Process());
		}

		[Test]
		public void Process_AsyncModelControllerDefaultResponse_CreatedDefaultReturnedDefaultReturnedAfterGetProcessResponse()
		{
			// Assign

			_controllerResponse.Setup(x => x.Process()).Returns(ControllerResponseResult.RawOutput);
			_asyncModelController.Setup(x => x.Invoke()).Returns(Task.FromResult(_controllerResponse.Object));
			_controllerFactory.Setup(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>())).Returns(_asyncModelController.Object);
			// Act
			var result = _executor.Execute(Mock.Of<IControllerMetaData>(), null, null);

			// Assert

			Assert.AreEqual(ControllerResponseResult.Default, result);
			_asyncModelController.Verify(x => x.Invoke());
			_controllerResponse.Verify(x => x.Process(), Times.Never());
			_controllerFactory.Verify(
				x =>
					x.CreateController(It.IsAny<Type>(), It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>(),
						It.IsAny<IDictionary<string, object>>()));

			// Act & Assert

			foreach (var response in _executor.ProcessAsyncControllersResponses(null))
			{
				_controllerResponse.Verify(x => x.Process());
				Assert.AreEqual(ControllerResponseResult.RawOutput, response);
			}
		}
	}
}