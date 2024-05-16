using System;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.Tests.Modules.Redirection;

[TestFixture]
public class RedirectorTests
{
	private Redirector _redirector = null!;

	private Mock<IWebContext> _context = null!;
	private Mock<IResponseCookies> _responseCookies = null!;

	[SetUp]
	public void Initialize()
	{
		_context = new Mock<IWebContext>();
		_redirector = new Redirector(_context.Object);
		_responseCookies = new Mock<IResponseCookies>();

		_context.Setup(x => x.Response.Redirect(It.IsAny<string>()));

		_context.SetupGet(x => x.Request.Scheme).Returns("http");
		_context.SetupGet(x => x.Request.Host).Returns(new HostString("localhost"));
		_context.SetupGet(x => x.Request.PathBase).Returns("/my-website");
		_context.SetupGet(x => x.Request.Path).Returns("/my-action?=foo");
		_context.SetupGet(x => x.SiteUrl).Returns("http://localhost/my-website/");

		_context.SetupGet(x => x.Response.Cookies).Returns(_responseCookies.Object);
		_context.SetupGet(x => x.Request.Cookies).Returns(Mock.Of<IRequestCookieCollection>());
	}

	[Test]
	public void Redirect_NullUrl_ArgumentNullExceptionThrown() =>
		Assert.Throws<ArgumentNullException>(() => _redirector.Redirect(null));

	[Test]
	public void Redirect_NormalUrl_ResponseRedirectCalled()
	{
		// Arrange
		_context.SetupGet(x => x.SiteUrl).Returns("http://test-website.com");

		// Act
		_redirector.Redirect("http://test-website.com");

		// Assert
		_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "http://test-website.com")), Times.Once);
	}

	[Test]
	public void Redirect_ToRedirectUrlHaveRedirectUrl_RedirectCalledWithCorrectLinkPreviousNavigatedUrlSet()
	{
		// Arrange

		var cookieCollection = new Mock<IRequestCookieCollection>();
		cookieCollection.SetupGet(x => x[It.Is<string>(s => s == Redirector.RedirectUrlCookieFieldName)]).Returns("foo");
		_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);

		_context.SetupGet(x => x.SiteUrl).Returns("foo");

		_responseCookies.Setup(x => x.Append(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, value) =>
		{
			Assert.That(key, Is.EqualTo(Redirector.PreviousNavigatedUrlCookieFieldName));
			Assert.That(value, Is.EqualTo("http://localhost/my-website/myaction%3F=foo"));
		});

		// Act
		_redirector.Redirect(RedirectionType.RedirectUrl);

		// Assert
		_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "foo")), Times.Once);
	}

	[Test]
	public void Redirect_ToRedirectLinkNoUrl_RedirectCalledToSiteVirtualPath()
	{
		// Act
		_redirector.Redirect(RedirectionType.RedirectUrl);

		// Assert
		_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "http://localhost/my-website/")), Times.Once);
	}

	[Test]
	public void Redirect_ToLoginReturnUrl_NoUrl_SiteUrl()
	{
		// Act
		_redirector.Redirect(RedirectionType.LoginReturnUrl);

		// Assert
		_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "http://localhost/my-website/")), Times.Once);
	}

	[Test]
	public void Redirect_ToLoginReturnUrl_NotNullOrEmpty_LoginReturnUrl()
	{
		// Arrange

		var cookieCollection = new Mock<IRequestCookieCollection>();
		cookieCollection.SetupGet(x => x[It.Is<string>(s => s == Redirector.LoginReturnUrlCookieFieldName)]).Returns("loginFoo");
		_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);

		_context.SetupGet(x => x.SiteUrl).Returns("loginFoo");

		// Act
		_redirector.Redirect(RedirectionType.LoginReturnUrl);

		// Assert
		_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "loginFoo")), Times.Once);
	}

	[Test]
	public void Redirect_ToPreviousPageHavePreviousPageUrl_RedirectCalledWithCorrectUrl()
	{
		// Arrange

		var cookieCollection = new Mock<IRequestCookieCollection>();
		cookieCollection.SetupGet(x => x[It.Is<string>(s => s == Redirector.PreviousPageUrlCookieFieldName)]).Returns("foo");
		_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);

		_context.SetupGet(x => x.SiteUrl).Returns("foo");

		// Act
		_redirector.Redirect(RedirectionType.PreviousPage);

		// Assert
		_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "foo")), Times.Once);
	}

	[Test]
	public void Redirect_ToPreviousPageWithBookmarkHaveUrl_RedirectCalledWithCorrectBookmarkUrl()
	{
		// Arrange

		var cookieCollection = new Mock<IRequestCookieCollection>();
		cookieCollection.SetupGet(x => x[It.Is<string>(s => s == Redirector.PreviousPageUrlCookieFieldName)]).Returns("foo");
		_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);

		_context.SetupGet(x => x.SiteUrl).Returns("foo");

		// Act
		_redirector.Redirect(RedirectionType.PreviousPageWithBookmark, "bar");

		// Assert
		_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "foo#bar")), Times.Once);
	}

	[Test]
	public void Redirect_ToCurrentPage_RedirectCalledToCurrentPage()
	{
		// Act
		_redirector.Redirect(RedirectionType.CurrentPage);

		// Assert
		_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "http://localhost/my-website/my-action%3F=foo")), Times.Once);
	}

	[Test]
	public void Redirect_ToDefaultPage_RedirectCalledToDefaultPage()
	{
		// Act
		_redirector.Redirect(RedirectionType.DefaultPage);

		// Assert
		_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "http://localhost/my-website/")), Times.Once);
	}

	[Test]
	public void SetRedirectUrlToCurrentPage_NormalUrl_Set()
	{
		// Arrange
		_responseCookies.Setup(x => x.Append(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, value) =>
		{
			Assert.That(key, Is.EqualTo(Redirector.RedirectUrlCookieFieldName));
			Assert.That(value, Is.EqualTo("http://localhost/my-website/my-action%3F=foo"));
		});

		// Act
		_redirector.SetRedirectUrlToCurrentPage();
	}

	[Test]
	public void SetLoginReturnUrlFromQuery_NormalUrl_Set()
	{
		// Arrange

		_context.SetupGet(x => x.Request.Path).Returns("/foo2");

		_responseCookies.Setup(x => x.Append(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, value) =>
		{
			Assert.That(key, Is.EqualTo(Redirector.LoginReturnUrlCookieFieldName));
			Assert.That(value, Is.EqualTo("http://localhost/my-website/foo2"));
		});

		// Act
		_redirector.SetLoginReturnUrlFromCurrentUri();
	}

	[Test]
	public void GetPreviousPageUrl_Return()
	{
		// Arrange

		var cookieCollection = new Mock<IRequestCookieCollection>();
		cookieCollection.SetupGet(x => x[It.Is<string>(s => s == Redirector.PreviousPageUrlCookieFieldName)]).Returns("foo");
		_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);

		// Act & Assert
		Assert.That(_redirector.PreviousPageUrl, Is.EqualTo("foo"));
	}

	[Test]
	public void GetPreviousNavigatedUrl_Return()
	{
		// Arrange

		var cookieCollection = new Mock<IRequestCookieCollection>();
		cookieCollection.SetupGet(x => x[It.Is<string>(s => s == Redirector.PreviousNavigatedUrlCookieFieldName)]).Returns("foo");
		_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);

		// Act & Assert
		Assert.That(_redirector.PreviousNavigatedUrl, Is.EqualTo("foo"));
	}

	[Test]
	public void SetPreviousPageUrl_Set()
	{
		// Arrange
		_responseCookies.Setup(x => x.Append(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, value) =>
		{
			Assert.That(key, Is.EqualTo(Redirector.PreviousPageUrlCookieFieldName));
			Assert.That(value, Is.EqualTo("foo"));
		});

		// Act & Assert
		_redirector.PreviousPageUrl = "foo";
	}
}