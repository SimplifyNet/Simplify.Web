using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;

namespace Simplify.Web.Tests.Old.Modules;

[TestFixture]
public class WebContextTests
{
	private Mock<HttpContext> _owinContext = null!;

	[SetUp]
	public void Initialize()
	{
		_owinContext = new Mock<HttpContext>();

		_owinContext.SetupGet(x => x.Response).Returns(new Mock<HttpResponse>().Object);
		_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString("/mywebsite"));
		_owinContext.SetupGet(x => x.Request.Path).Returns(new PathString("/"));
		_owinContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_owinContext.SetupGet(x => x.Request.Host).Returns(new HostString("localhost"));
		_owinContext.SetupGet(x => x.Request.Query).Returns(new Mock<IQueryCollection>().Object);

		_owinContext.SetupGet(x => x.Request.Headers)
			.Returns(new HeaderDictionary(new Dictionary<string, StringValues>()));
	}

	[Test]
	public void Constructor_NormalContext_SetCorrectly()
	{
		// Act
		var context = new WebContext(_owinContext.Object);

		// Assert

		Assert.AreEqual(_owinContext.Object, context.Context);
		Assert.AreEqual(_owinContext.Object.Request, context.Request);
		Assert.AreEqual(_owinContext.Object.Response, context.Response);
		Assert.AreEqual(_owinContext.Object.Request.Query, context.Query);
		Assert.AreEqual("http://localhost/mywebsite/", context.SiteUrl);
		Assert.AreEqual("/mywebsite", context.VirtualPath);
		Assert.AreEqual("/", context.Route);
		Assert.IsFalse(context.IsAjax);
	}

	[Test]
	public void Constructor_PathWithPort_SetCorrectly()
	{
		// Assign

		_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));
		_owinContext.SetupGet(x => x.Request.Path).Returns(new PathString("/"));
		_owinContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_owinContext.SetupGet(x => x.Request.Host).Returns(new HostString("localhost", 8080));

		// Act
		var context = new WebContext(_owinContext.Object);

		// Assert

		Assert.AreEqual("http://localhost:8080/", context.SiteUrl);
		Assert.AreEqual("", context.VirtualPath);
	}

	[Test]
	public void Constructor_PathWithPortAndQueryString_SetCorrectly()
	{
		// Assign

		_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));
		_owinContext.SetupGet(x => x.Request.Path).Returns(new PathString("/?act=test"));
		_owinContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_owinContext.SetupGet(x => x.Request.Host).Returns(new HostString("localhost", 8080));

		// Act
		var context = new WebContext(_owinContext.Object);

		// Assert

		Assert.AreEqual("http://localhost:8080/", context.SiteUrl);
		Assert.AreEqual("", context.VirtualPath);
	}

	[Test]
	public void Constructor_VirtualPathWithPort_SetCorrectly()
	{
		// Assign

		_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString("/mywebsite"));
		_owinContext.SetupGet(x => x.Request.Path).Returns(new PathString("/"));
		_owinContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_owinContext.SetupGet(x => x.Request.Host).Returns(new HostString("localhost", 8080));

		// Act
		var context = new WebContext(_owinContext.Object);

		// Assert

		Assert.AreEqual("http://localhost:8080/mywebsite/", context.SiteUrl);
		Assert.AreEqual("/mywebsite", context.VirtualPath);
	}

	[Test]
	public void Constructor_NoVirtualPath_Empty()
	{
		// Assign
		_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));

		// Act
		var context = new WebContext(_owinContext.Object);

		// Assert
		Assert.AreEqual("", context.VirtualPath);
	}

	[Test]
	public void Constructor_LocalhostWithVirtualPathAndSegmentsWithQueryString_ParsedCorrectly()
	{
		// Assign

		_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString("/mywebsite"));
		_owinContext.SetupGet(x => x.Request.Path).Returns(new PathString("/test?act=foo"));
		_owinContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_owinContext.SetupGet(x => x.Request.Host).Returns(new HostString("localhost"));

		// Act
		var context = new WebContext(_owinContext.Object);

		// Assert

		Assert.AreEqual("http://localhost/mywebsite/", context.SiteUrl);
	}

	[Test]
	public void Constructor_NormalPath_ParsedCorrectly()
	{
		// Assign

		_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));
		_owinContext.SetupGet(x => x.Request.Path).Returns(new PathString("/"));
		_owinContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_owinContext.SetupGet(x => x.Request.Host).Returns(new HostString("mywebsite.com"));

		// Act
		var context = new WebContext(_owinContext.Object);

		// Assert

		Assert.AreEqual("http://mywebsite.com/", context.SiteUrl);
	}

	[Test]
	public void Constructor_NormalPathAndSegmentsWithQueryString_ParsedCorrectly()
	{
		// Assign

		_owinContext.SetupGet(x => x.Request.PathBase).Returns(new PathString(""));
		_owinContext.SetupGet(x => x.Request.Path).Returns(new PathString("/test/?act=foo"));
		_owinContext.SetupGet(x => x.Request.Scheme).Returns("http");
		_owinContext.SetupGet(x => x.Request.Host).Returns(new HostString("mywebsite.com"));

		// Act
		var context = new WebContext(_owinContext.Object);

		// Assert

		Assert.AreEqual("http://mywebsite.com/", context.SiteUrl);
	}

	[Test]
	public void Constructor_AjaxRequest_True()
	{
		// Assign
		_owinContext.SetupGet(x => x.Request.Headers)
			.Returns(new HeaderDictionary(new Dictionary<string, StringValues> { { "X-Requested-With", new[] { "test" } } }));

		// Act
		var context = new WebContext(_owinContext.Object);

		// Assert

		Assert.IsTrue(context.IsAjax);
	}

	[Test]
	public void Constructor_SpecificRoute_SetCorrectly()
	{
		// Assign
		_owinContext.SetupGet(x => x.Request.Path).Returns(new PathString("/test"));

		// Act
		var context = new WebContext(_owinContext.Object);

		// Assert

		Assert.AreEqual("/test", context.Route);
	}

	[Test]
	public void IsAuthenticated_UserIsNotNullAndAuthenticated_True()
	{
		// Arrange

		_owinContext.SetupGet(x => x.User)
			.Returns(Mock.Of<ClaimsPrincipal>(f => f.Identity == Mock.Of<IIdentity>(i => i.IsAuthenticated)));

		var context = new WebContext(_owinContext.Object);

		// Act & Assert
		Assert.IsTrue(context.IsAuthenticated);
	}

	[Test]
	public void IsAuthenticated_UserIsNotNullAndNotAuthenticated_False()
	{
		// Arrange

		_owinContext.SetupGet(x => x.User)
			.Returns(Mock.Of<ClaimsPrincipal>(f => f.Identity == Mock.Of<IIdentity>(i => i.IsAuthenticated == false)));

		var context = new WebContext(_owinContext.Object);

		// Act & Assert
		Assert.IsFalse(context.IsAuthenticated);
	}

	[Test]
	public void IsAuthenticated_UserIsNull_False()
	{
		// Arrange

		_owinContext.SetupGet(x => x.User).Returns((ClaimsPrincipal?)null!);

		var context = new WebContext(_owinContext.Object);

		// Act & Assert
		Assert.IsFalse(context.IsAuthenticated);
	}
}