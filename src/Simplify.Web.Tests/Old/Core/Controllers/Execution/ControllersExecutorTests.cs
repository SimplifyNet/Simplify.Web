using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Meta;

namespace Simplify.Web.Tests.Old.Core.Controllers.Execution;

[TestFixture]
public class ControllersExecutorTests
{
	private ControllerExecutor _executor = null!;

	private Mock<IVersionedControllerExecutor> _executor1 = null!;
	private Mock<IVersionedControllerExecutor> _executor2 = null!;

	private Mock<IControllerResponseBuilder> _controllerResponseBuilder = null!;

	[SetUp]
	public void Initialize()
	{
		_executor1 = new Mock<IVersionedControllerExecutor>();
		_executor2 = new Mock<IVersionedControllerExecutor>();

		_executor1.SetupGet(x => x.Version).Returns(ControllerVersion.V1);
		_executor2.SetupGet(x => x.Version).Returns(ControllerVersion.V2);

		_controllerResponseBuilder = new Mock<IControllerResponseBuilder>();

		_executor = new ControllerExecutor(new List<IVersionedControllerExecutor>{
			_executor2.Object,
			_executor1.Object
		}, _controllerResponseBuilder.Object);
	}

	[Test]
	public void Ctor_NotAllVersionedExecutorsSpecified_InvalidOperationException()
	{
		// Act & Assert
		Assert.Throws<InvalidOperationException>(() => new ControllerExecutor(new List<IVersionedControllerExecutor>(), null!));
	}

	[Test]
	public async Task Execute_ControllerWithoutResponse_ExecutedWithDefaultResult()
	{
		// Arrange

		var controller = Mock.Of<IControllerMetaData>(x => x.Version == ControllerVersion.V2);
		var args = Mock.Of<IControllerExecutionArgs>(x => x.ControllerMetaData == controller);

		// Act
		var result = await _executor.Execute(args);

		// Assert

		Assert.That(result, Is.EqualTo(ControllerResponseResult.Default));

		_executor2.Verify(x => x.Execute(It.IsAny<IControllerExecutionArgs>()));

		_controllerResponseBuilder.Verify(x => x.BuildControllerResponseProperties(It.IsAny<ControllerResponse>(), It.IsAny<IDIResolver>()),
			Times.Never);
	}

	[Test]
	public async Task Execute_ControllerWithResponse_ExecutedAndResponseProcessed()
	{
		// Arrange

		var controller = Mock.Of<IControllerMetaData>(x => x.Version == ControllerVersion.V2);
		var args = Mock.Of<IControllerExecutionArgs>(x => x.ControllerMetaData == controller);

		var response = new Mock<ControllerResponse>();

		response.Setup(x => x.ExecuteAsync()).ReturnsAsync(ControllerResponseResult.RawOutput);

		_executor2.Setup(x => x.Execute(It.IsAny<IControllerExecutionArgs>()))
			.ReturnsAsync(response.Object);

		// Act
		var result = await _executor.Execute(args);

		// Assert

		Assert.That(result, Is.EqualTo(ControllerResponseResult.RawOutput));

		_executor2.Verify(x => x.Execute(It.IsAny<IControllerExecutionArgs>()));

		_controllerResponseBuilder.Verify(x => x.BuildControllerResponseProperties(It.IsAny<ControllerResponse>(), It.IsAny<IDIResolver>()));

		response.Verify(x => x.ExecuteAsync());
	}
}