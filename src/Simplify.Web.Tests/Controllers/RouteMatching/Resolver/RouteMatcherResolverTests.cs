using System;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.RouteMatching.Resolver;

namespace Simplify.Web.Tests.Controllers.RouteMatching.Resolver;

[TestFixture]
public class RouteMatcherResolverTests
{
	[Test]
	public void Resolve_TwoMatchersFirstMatched_FirstResolved()
	{
		// Arrange

		var md = Mock.Of<IControllerMetadata>();
		var matcher1 = new Mock<IRouteMatcher>();
		var matcher2 = new Mock<IRouteMatcher>();

		matcher1.Setup(x => x.CanHandle(It.Is<IControllerMetadata>(m => m == md))).Returns(true);

		var resolver = new RouteMatcherResolver([matcher1.Object, matcher2.Object]);

		// Act
		var result = resolver.Resolve(md);

		// Assert

		Assert.That(result, Is.EqualTo(matcher1.Object));

		matcher1.Verify(x => x.CanHandle(It.Is<IControllerMetadata>(m => m == md)));
		matcher2.Verify(x => x.CanHandle(It.IsAny<IControllerMetadata>()), Times.Never);
	}

	[Test]
	public void Resolve_TwoMatchersSecondMatched_SecondResolved()
	{
		// Arrange

		var md = Mock.Of<IControllerMetadata>();
		var matcher1 = new Mock<IRouteMatcher>();
		var matcher2 = new Mock<IRouteMatcher>();

		matcher2.Setup(x => x.CanHandle(It.Is<IControllerMetadata>(m => m == md))).Returns(true);

		var resolver = new RouteMatcherResolver([matcher1.Object, matcher2.Object]);

		// Act
		var result = resolver.Resolve(md);

		// Assert

		Assert.That(result, Is.EqualTo(matcher2.Object));

		matcher2.Verify(x => x.CanHandle(It.Is<IControllerMetadata>(m => m == md)));
	}

	[Test]
	public void Resolve_NoMatchersMatched_Exception()
	{
		// Arrange

		var md = Mock.Of<IControllerMetadata>();

		var resolver = new RouteMatcherResolver([]);

		// Act
		Assert.Throws<InvalidOperationException>(() => resolver.Resolve(md));
	}
}