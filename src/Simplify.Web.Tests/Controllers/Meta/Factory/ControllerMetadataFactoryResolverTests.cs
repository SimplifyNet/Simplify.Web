using System;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta.Factory;

namespace Simplify.Web.Tests.Controllers.Meta.Factory;

[TestFixture]
public class ControllerMetadataFactoryResolverTests
{
	[Test]
	public void Resolve_TwoFactoriesFirstMatched_FirstResolved()
	{
		// Arrange

		var type = Mock.Of<Type>();
		var factory1 = new Mock<IControllerMetadataFactory>();
		var factory2 = new Mock<IControllerMetadataFactory>();

		factory1.Setup(x => x.CanHandle(It.Is<Type>(t => t == type))).Returns(true);

		var resolver = new ControllerMetadataFactoryResolver([factory1.Object, factory2.Object]);

		// Act
		var result = resolver.Resolve(type);

		// Assert

		Assert.That(result, Is.EqualTo(factory1.Object));

		factory1.Verify(x => x.CanHandle(It.Is<Type>(t => t == type)));
		factory2.Verify(x => x.CanHandle(It.IsAny<Type>()), Times.Never);
	}

	[Test]
	public void Resolve_TwoFactoriesSecondMatched_SecondResolved()
	{
		// Arrange

		var type = Mock.Of<Type>();
		var factory1 = new Mock<IControllerMetadataFactory>();
		var factory2 = new Mock<IControllerMetadataFactory>();

		factory2.Setup(x => x.CanHandle(It.Is<Type>(t => t == type))).Returns(true);

		var resolver = new ControllerMetadataFactoryResolver([factory1.Object, factory2.Object]);

		// Act
		var result = resolver.Resolve(type);

		// Assert

		Assert.That(result, Is.EqualTo(factory2.Object));

		factory2.Verify(x => x.CanHandle(It.Is<Type>(t => t == type)));
	}

	[Test]
	public void Resolve_NoExecutorsMatched_Exception()
	{
		// Arrange

		var type = Mock.Of<Type>();

		var resolver = new ControllerMetadataFactoryResolver([]);

		// Act
		Assert.Throws<InvalidOperationException>(() => resolver.Resolve(type));
	}
}