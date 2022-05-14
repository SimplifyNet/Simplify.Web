using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Core.PageAssembly;

namespace Simplify.Web.Tests.Core.PageAssembly;

[TestFixture]
public class PageProcessorTests
{
	private PageProcessor _processor = null!;
	private Mock<IPageBuilder> _pageBuilder = null!;
	private Mock<IResponseWriter> _responseWriter = null!;

	private Mock<HttpContext> _context = null!;

	[SetUp]
	public void Initialize()
	{
		_pageBuilder = new Mock<IPageBuilder>();
		_responseWriter = new Mock<IResponseWriter>();
		_processor = new PageProcessor(_pageBuilder.Object, _responseWriter.Object);

		_context = new Mock<HttpContext>();

		_context.SetupSet(x => x.Response.ContentType = It.IsAny<string>());
	}

	[Test]
	public async Task Process_Ok_PageBuiltWithOutput()
	{
		// Assign

		_pageBuilder.Setup(x => x.Build(It.IsAny<IDIContainerProvider>())).Returns("Foo");
		_context.SetupGet(x => x.Request.Scheme).Returns("http");
		_context.SetupGet(x => x.Request.Host).Returns(new HostString("localhost", 8080));
		_context.SetupGet(x => x.Request.Path).Returns("/test");

		// Act
		await _processor.ProcessPage(null!, _context.Object);

		// Assert

		_pageBuilder.Verify(x => x.Build(It.IsAny<IDIContainerProvider>()));
		_responseWriter.Verify(x => x.WriteAsync(It.Is<string>(d => d == "Foo"), It.Is<HttpResponse>(d => d == _context.Object.Response)));
		_context.VerifySet(x => x.Response.ContentType = It.Is<string>(s => s == "text/html"));
	}
}