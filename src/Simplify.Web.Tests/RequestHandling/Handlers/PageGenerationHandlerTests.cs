using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.Page.Composition;
using Simplify.Web.RequestHandling;
using Simplify.Web.RequestHandling.Handlers;

namespace Simplify.Web.Tests.RequestHandling.Handlers;

[TestFixture]
public class PageGenerationHandlerTests
{
	private PageGenerationHandler _handler = null!;

	private Mock<IPageComposer> _pageComposer = null!;
	private Mock<IResponseWriter> _responseWriter = null!;

	[SetUp]
	public void Initialize()
	{
		_pageComposer = new Mock<IPageComposer>();
		_responseWriter = new Mock<IResponseWriter>();

		_handler = new PageGenerationHandler(_pageComposer.Object, _responseWriter.Object);
	}

	[Test]
	public async Task HandleAsync_ResponseWriterCalledWithDataFromComposeAndRespectiveResponseFieldsAreSet()
	{
		// Arrange

		_pageComposer.Setup(x => x.Compose()).Returns("Foo");

		var response = Mock.Of<HttpResponse>();

		var httpContext = Mock.Of<HttpContext>(x =>
			x.Request.Path == new PathString("/foo") &&
			x.Response == response);

		var next = new Mock<RequestHandlerAsync>();

		// Act
		await _handler.HandleAsync(httpContext, next.Object);

		// Assert

		Assert.That(response.ContentType, Is.EqualTo("text/html"));

		_responseWriter.Verify(x => x.WriteAsync(It.Is<HttpResponse>(r => r == response), It.Is<string>(s => s == "Foo")));
		next.Verify(x => x.Invoke(), Times.Never);
	}
}