using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Redirection;
using Simplify.Web.RequestHandling;
using Simplify.Web.RequestHandling.Handlers;

namespace Simplify.Web.Tests.RequestHandling.Handlers;

[TestFixture]
public class SetLoginUrlForUnauthorizedRequestHandlerTests
{
	private SetLoginUrlForUnauthorizedRequestHandler _handler = null!;

	private Mock<IRedirector> _redirector = null!;

	[SetUp]
	public void Initialize()
	{
		_redirector = new Mock<IRedirector>();
		_handler = new SetLoginUrlForUnauthorizedRequestHandler(_redirector.Object);
	}

	[Test]
	public async Task HandleAsync_StatusCodeIsUnauthorized_SetLoginReturnUrlFromCurrentUriCalled()
	{
		// Arrange

		var httpContext = Mock.Of<HttpContext>(x => x.Response.StatusCode == (int)HttpStatusCode.Unauthorized);
		var next = new Mock<RequestHandlerAsync>();

		// Checking calls sequence
		next.Setup(x => x.Invoke()).Callback(() => _redirector.Verify(x => x.SetLoginReturnUrlFromCurrentUri(), Times.Never));

		// Act
		await _handler.HandleAsync(httpContext, next.Object);

		// Assert

		next.Verify(x => x.Invoke());
		_redirector.Verify(x => x.SetLoginReturnUrlFromCurrentUri());
	}

	[Test]
	public async Task HandleAsync_StatusCodeIsNoUnauthorized_SetLoginReturnUrlFromCurrentUriNotCalled()
	{
		// Arrange

		var httpContext = Mock.Of<HttpContext>(x => x.Response.StatusCode == (int)HttpStatusCode.OK);
		var next = new Mock<RequestHandlerAsync>();

		// Checking calls sequence
		next.Setup(x => x.Invoke()).Callback(() => _redirector.Verify(x => x.SetLoginReturnUrlFromCurrentUri(), Times.Never));

		// Act
		await _handler.HandleAsync(httpContext, next.Object);

		// Assert

		next.Verify(x => x.Invoke());
		_redirector.Verify(x => x.SetLoginReturnUrlFromCurrentUri(), Times.Never);
	}
}
