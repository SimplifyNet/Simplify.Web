using System;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Execution.Resolver;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Tests.Controllers.Execution.Resolver;

[TestFixture]
public class ControllerExecutorResolverTests
{
	[Test]
	public void Resolve_TwoExecutorsFirstMatched_FirstResolved()
	{
		// Arrange

		var md = Mock.Of<IControllerMetadata>();
		var executor1 = new Mock<IControllerExecutor>();
		var executor2 = new Mock<IControllerExecutor>();

		executor1.Setup(x => x.CanHandle(It.Is<IControllerMetadata>(m => m == md))).Returns(true);

		var resolver = new ControllerExecutorResolver([executor1.Object, executor2.Object]);

		// Act
		var result = resolver.Resolve(md);

		// Assert

		Assert.That(result, Is.EqualTo(executor1.Object));

		executor1.Verify(x => x.CanHandle(It.Is<IControllerMetadata>(m => m == md)));
		executor2.Verify(x => x.CanHandle(It.IsAny<IControllerMetadata>()), Times.Never);
	}

	[Test]
	public void Resolve_TwoExecutorsSecondMatched_SecondResolved()
	{
		// Arrange

		var md = Mock.Of<IControllerMetadata>();
		var executor1 = new Mock<IControllerExecutor>();
		var executor2 = new Mock<IControllerExecutor>();

		executor2.Setup(x => x.CanHandle(It.Is<IControllerMetadata>(m => m == md))).Returns(true);

		var resolver = new ControllerExecutorResolver([executor1.Object, executor2.Object]);

		// Act
		var result = resolver.Resolve(md);

		// Assert

		Assert.That(result, Is.EqualTo(executor2.Object));

		executor2.Verify(x => x.CanHandle(It.Is<IControllerMetadata>(m => m == md)));
	}

	[Test]
	public void Resolve_NoExecutorsMatched_Exception()
	{
		// Arrange

		var md = Mock.Of<IControllerMetadata>();

		var resolver = new ControllerExecutorResolver([]);

		// Act
		Assert.Throws<InvalidOperationException>(() => resolver.Resolve(md));
	}
}