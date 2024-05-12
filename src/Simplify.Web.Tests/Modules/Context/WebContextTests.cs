using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Context;

namespace Simplify.Web.Tests.Modules.Context;

[TestFixture]
public class WebContextTests
{
	private Mock<HttpContext> _httpContext = null!;

	[SetUp]
	public void Initialize()
	{
		_httpContext = new Mock<HttpContext>();

		_httpContext.SetupGet(x => x.Response).Returns(new Mock<HttpResponse>().Object);
		_httpContext.SetupGet(x => x.Request.PathBase).Returns(new PathString("/my-website"));
		_httpContext.SetupGet(x => x.Request.Path).Returns(new PathString("/"));
		_httpContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_httpContext.SetupGet(x => x.Request.Host).Returns(new HostString("localhost"));
		_httpContext.SetupGet(x => x.Request.Query).Returns(new Mock<IQueryCollection>().Object);

		_httpContext.SetupGet(x => x.Request.Headers)
			.Returns(new HeaderDictionary(new Dictionary<string, StringValues>()));
	}

	[Test]
	public void Constructor_NormalContext_SetCorrectly()
	{
		// Act
		var context = new WebContext(_httpContext.Object);

		// Assert

		Assert.That(context.Context, Is.EqualTo(_httpContext.Object));
		Assert.That(context.Context.Request, Is.EqualTo(_httpContext.Object.Request));
		Assert.That(context.Context.Response, Is.EqualTo(_httpContext.Object.Response));
		Assert.That(context.Query, Is.EqualTo(_httpContext.Object.Request.Query));
		Assert.That(context.SiteUrl, Is.EqualTo("http://localhost/my-website/"));
		Assert.That(context.VirtualPath, Is.EqualTo("/my-website"));
		Assert.That(context.Route, Is.EqualTo("/"));
		Assert.That(context.IsAjax, Is.False);
	}

	[Test]
	public void Constructor_PathWithPort_SetCorrectly()
	{
		// Arrange

		_httpContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));
		_httpContext.SetupGet(x => x.Request.Path).Returns(new PathString("/"));
		_httpContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_httpContext.SetupGet(x => x.Request.Host).Returns(new HostString("localhost", 8080));

		// Act
		var context = new WebContext(_httpContext.Object);

		// Assert

		Assert.That(context.SiteUrl, Is.EqualTo("http://localhost:8080/"));
		Assert.That(context.VirtualPath, Is.EqualTo(""));
	}

	[Test]
	public void Constructor_PathWithPortAndQueryString_SetCorrectly()
	{
		// Arrange

		_httpContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));
		_httpContext.SetupGet(x => x.Request.Path).Returns(new PathString("/?act=test"));
		_httpContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_httpContext.SetupGet(x => x.Request.Host).Returns(new HostString("localhost", 8080));

		// Act
		var context = new WebContext(_httpContext.Object);

		// Assert

		Assert.That(context.SiteUrl, Is.EqualTo("http://localhost:8080/"));
		Assert.That(context.VirtualPath, Is.EqualTo(""));
	}

	[Test]
	public void Constructor_VirtualPathWithPort_SetCorrectly()
	{
		// Arrange

		_httpContext.SetupGet(x => x.Request.PathBase).Returns(new PathString("/my-website"));
		_httpContext.SetupGet(x => x.Request.Path).Returns(new PathString("/"));
		_httpContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_httpContext.SetupGet(x => x.Request.Host).Returns(new HostString("localhost", 8080));

		// Act
		var context = new WebContext(_httpContext.Object);

		// Assert

		Assert.That(context.SiteUrl, Is.EqualTo("http://localhost:8080/my-website/"));
		Assert.That(context.VirtualPath, Is.EqualTo("/my-website"));
	}

	[Test]
	public void Constructor_NoVirtualPath_Empty()
	{
		// Arrange
		_httpContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));

		// Act
		var context = new WebContext(_httpContext.Object);

		// Assert
		Assert.That(context.VirtualPath, Is.EqualTo(""));
	}

	[Test]
	public void Constructor_LocalhostWithVirtualPathAndSegmentsWithQueryString_ParsedCorrectly()
	{
		// Arrange

		_httpContext.SetupGet(x => x.Request.PathBase).Returns(new PathString("/my-website"));
		_httpContext.SetupGet(x => x.Request.Path).Returns(new PathString("/test?act=foo"));
		_httpContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_httpContext.SetupGet(x => x.Request.Host).Returns(new HostString("localhost"));

		// Act
		var context = new WebContext(_httpContext.Object);

		// Assert

		Assert.That(context.SiteUrl, Is.EqualTo("http://localhost/my-website/"));
	}

	[Test]
	public void Constructor_NormalPath_ParsedCorrectly()
	{
		// Arrange

		_httpContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));
		_httpContext.SetupGet(x => x.Request.Path).Returns(new PathString("/"));
		_httpContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_httpContext.SetupGet(x => x.Request.Host).Returns(new HostString("my-website.com"));

		// Act
		var context = new WebContext(_httpContext.Object);

		// Assert

		Assert.That(context.SiteUrl, Is.EqualTo("http://my-website.com/"));
	}

	[Test]
	public void Constructor_NormalPathAndSegmentsWithQueryString_ParsedCorrectly()
	{
		// Arrange

		_httpContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));
		_httpContext.SetupGet(x => x.Request.Path).Returns(new PathString("/test/?act=foo"));
		_httpContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_httpContext.SetupGet(x => x.Request.Host).Returns(new HostString("my-website.com"));

		// Act
		var context = new WebContext(_httpContext.Object);

		// Assert

		Assert.That(context.SiteUrl, Is.EqualTo("http://my-website.com/"));
	}

	[Test]
	public void Constructor_AjaxRequest_True()
	{
		// Arrange
		_httpContext.SetupGet(x => x.Request.Headers)
			.Returns(new HeaderDictionary(new Dictionary<string, StringValues> { { "X-Requested-With", new[] { "test" } } }));

		// Act
		var context = new WebContext(_httpContext.Object);

		// Assert

		Assert.That(context.IsAjax, Is.True);
	}

	[Test]
	public void Constructor_SpecificRoute_SetCorrectly()
	{
		// Arrange
		_httpContext.SetupGet(x => x.Request.Path).Returns(new PathString("/test"));

		// Act
		var context = new WebContext(_httpContext.Object);

		// Assert

		Assert.That(context.Route, Is.EqualTo("/test"));
	}

	[Test]
	public void IsAuthenticated_UserIsNotNullAndAuthenticated_True()
	{
		// Arrange

		_httpContext.SetupGet(x => x.User)
			.Returns(Mock.Of<ClaimsPrincipal>(f => f.Identity == Mock.Of<IIdentity>(i => i.IsAuthenticated)));

		var context = new WebContext(_httpContext.Object);

		// Act & Assert
		Assert.That(context.IsAuthenticated, Is.True);
	}

	[Test]
	public void IsAuthenticated_UserIsNotNullAndNotAuthenticated_False()
	{
		// Arrange

		_httpContext.SetupGet(x => x.User)
			.Returns(Mock.Of<ClaimsPrincipal>(f => f.Identity == Mock.Of<IIdentity>(i => i.IsAuthenticated == false)));

		var context = new WebContext(_httpContext.Object);

		// Act & Assert
		Assert.That(context.IsAuthenticated, Is.False);
	}

	[Test]
	public void IsAuthenticated_UserIsNull_False()
	{
		// Arrange

		_httpContext.SetupGet(x => x.User).Returns((ClaimsPrincipal?)null!);

		var context = new WebContext(_httpContext.Object);

		// Act & Assert
		Assert.That(context.IsAuthenticated, Is.False);
	}
}