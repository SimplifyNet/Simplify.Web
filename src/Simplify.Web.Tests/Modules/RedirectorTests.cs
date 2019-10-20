#nullable disable

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;

namespace Simplify.Web.Tests.Modules
{
	[TestFixture]
	public class RedirectorTests
	{
		private Mock<IWebContext> _context;
		private Redirector _redirector;
		private Mock<IResponseCookies> _responseCookies;

		[SetUp]
		public void Initialize()
		{
			_context = new Mock<IWebContext>();
			_redirector = new Redirector(_context.Object);
			_responseCookies = new Mock<IResponseCookies>();

			_context.Setup(x => x.Response.Redirect(It.IsAny<string>()));

			_context.SetupGet(x => x.Request.Scheme).Returns("http");
			_context.SetupGet(x => x.Request.Host).Returns(new HostString("localhost"));
			_context.SetupGet(x => x.Request.PathBase).Returns("/mywebsite");
			_context.SetupGet(x => x.Request.Path).Returns("/myaction?=foo");
			_context.SetupGet(x => x.SiteUrl).Returns("http://localhost/mywebsite/");

			_context.SetupGet(x => x.Response.Cookies).Returns(_responseCookies.Object);
			_context.SetupGet(x => x.Request.Cookies).Returns(new RequestCookieCollection(new Dictionary<string, string>()));
		}

		[Test]
		public void Redirect_NullUrl_ArgumentNullExceptionThrown()
		{
			Assert.Throws<ArgumentNullException>(() => _redirector.Redirect(null));
		}

		[Test]
		public void Redirect_NormalUrl_ResponseRedirectCalled()
		{
			// Act
			_redirector.Redirect("http://testwebsite.com");

			// Assert
			_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "http://testwebsite.com")), Times.Once);
		}

		[Test]
		public void Redirect_ToRedirectUrlHaveRedirectUrl_RedirectCalledWithCorrectLinkPreviousNavigatedUrlSet()
		{
			// Assign

			_context.SetupGet(x => x.Request.Cookies)
				.Returns(new RequestCookieCollection(new Dictionary<string, string> { { Redirector.RedirectUrlCookieFieldName, "foo" } }));

			_responseCookies.Setup(x => x.Append(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, value) =>
			{
				Assert.AreEqual(Redirector.PreviousNavigatedUrlCookieFieldName, key);
				Assert.AreEqual("http://localhost/mywebsite/myaction%3F=foo", value);
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
			_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "http://localhost/mywebsite/")), Times.Once);
		}

		[Test]
		public void Redirect_ToLoginReturnUrl_NoUrl_SiteUrl()
		{
			// Act
			_redirector.Redirect(RedirectionType.LoginReturnUrl);

			// Assert
			_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "http://localhost/mywebsite/")), Times.Once);
		}

		[Test]
		public void Redirect_ToLoginReturnUrl_NotNullOrEmpty_LoginReturnUrl()
		{
			// Assign
			_context.SetupGet(x => x.Request.Cookies)
				.Returns(new RequestCookieCollection(new Dictionary<string, string> { { Redirector.LoginReturnUrlCookieFieldName, "loginFoo" } }));

			// Act
			_redirector.Redirect(RedirectionType.LoginReturnUrl);

			// Assert
			_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "loginFoo")), Times.Once);
		}

		[Test]
		public void Redirect_ToPreviousPageHavePreviousPageUrl_RedirectCalledWithCorrectUrl()
		{
			// Arrange
			_context.SetupGet(x => x.Request.Cookies)
				.Returns(new RequestCookieCollection(new Dictionary<string, string> { { Redirector.PreviousPageUrlCookieFieldName, "foo" } }));

			// Act
			_redirector.Redirect(RedirectionType.PreviousPage);

			// Assert
			_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "foo")), Times.Once);
		}

		[Test]
		public void Redirect_ToPreviousPageWithBookmarkHaveUrl_RedirectCalledWithCorrectBookmarkUrl()
		{
			// Assign
			_context.SetupGet(x => x.Request.Cookies)
				.Returns(new RequestCookieCollection(new Dictionary<string, string> { { Redirector.PreviousPageUrlCookieFieldName, "foo" } }));

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
			_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "http://localhost/mywebsite/myaction%3F=foo")), Times.Once);
		}

		[Test]
		public void Redirect_ToDefaultPage_RedirectCalledToDefaultPage()
		{
			// Act
			_redirector.Redirect(RedirectionType.DefaultPage);

			// Assert
			_context.Verify(x => x.Response.Redirect(It.Is<string>(c => c == "http://localhost/mywebsite/")), Times.Once);
		}

		[Test]
		public void SetRedirectUrlToCurrentPage_NormalUrl_Set()
		{
			// Assign
			_responseCookies.Setup(x => x.Append(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, value) =>
			{
				Assert.AreEqual(Redirector.RedirectUrlCookieFieldName, key);
				Assert.AreEqual("http://localhost/mywebsite/myaction%3F=foo", value);
			});

			// Act
			_redirector.SetRedirectUrlToCurrentPage();
		}

		[Test]
		public void SetLoginReturnUrlFromQuery_NormalUrl_Set()
		{
			// Assign

			_context.SetupGet(x => x.Request.Path).Returns("/foo2");

			_responseCookies.Setup(x => x.Append(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, value) =>
			{
				Assert.AreEqual(Redirector.LoginReturnUrlCookieFieldName, key);
				Assert.AreEqual("http://localhost/mywebsite/foo2", value);
			});

			// Act
			_redirector.SetLoginReturnUrlFromCurrentUri();
		}

		[Test]
		public void GetPreviousPageUrl_Return()
		{
			// Assign
			_context.SetupGet(x => x.Request.Cookies)
				.Returns(new RequestCookieCollection(new Dictionary<string, string> { { Redirector.PreviousPageUrlCookieFieldName, "foo" } }));

			// Act & Assert
			Assert.AreEqual("foo", _redirector.PreviousPageUrl);
		}

		[Test]
		public void GetPreviousNavigatedUrl_Return()
		{
			// Assign
			_context.SetupGet(x => x.Request.Cookies)
				.Returns(new RequestCookieCollection(new Dictionary<string, string> { { Redirector.PreviousNavigatedUrlCookieFieldName, "foo" } }));

			// Act & Assert
			Assert.AreEqual("foo", _redirector.PreviousNavigatedUrl);
		}

		[Test]
		public void SetPreviousPageUrl_Set()
		{
			// Assign
			_responseCookies.Setup(x => x.Append(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, value) =>
			{
				Assert.AreEqual(Redirector.PreviousPageUrlCookieFieldName, key);
				Assert.AreEqual("foo", value);
			});

			// Act & Assert
			_redirector.PreviousPageUrl = "foo";
		}
	}
}