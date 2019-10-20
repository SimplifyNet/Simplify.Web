#nullable disable

using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Core.PageAssembly;
using Simplify.Web.Modules;

namespace Simplify.Web.Tests.Core.PageAssembly
{
	[TestFixture]
	public class PageProcessorTests
	{
		private PageProcessor _processor;
		private Mock<IPageBuilder> _pageBuilder;
		private Mock<IResponseWriter> _responseWriter;
		private Mock<IRedirector> _redirector;

		private Mock<HttpContext> _context;

		[SetUp]
		public void Initialize()
		{
			_pageBuilder = new Mock<IPageBuilder>();
			_responseWriter = new Mock<IResponseWriter>();
			_redirector = new Mock<IRedirector>();
			_processor = new PageProcessor(_pageBuilder.Object, _responseWriter.Object, _redirector.Object);

			_context = new Mock<HttpContext>();

			_context.SetupSet(x => x.Response.ContentType = It.IsAny<string>());
		}

		[Test]
		public void Process_Ok_PageBuiltWithOutput()
		{
			// Assign

			_pageBuilder.Setup(x => x.Build(It.IsAny<IDIContainerProvider>())).Returns("Foo");
			_context.SetupGet(x => x.Request.Scheme).Returns("http");
			_context.SetupGet(x => x.Request.Host).Returns(new HostString("localhost", 8080));
			_context.SetupGet(x => x.Request.Path).Returns("/test");

			// Act
			_processor.ProcessPage(null, _context.Object);

			// Assert

			_pageBuilder.Verify(x => x.Build(It.IsAny<IDIContainerProvider>()));
			_responseWriter.Verify(x => x.WriteAsync(It.Is<string>(d => d == "Foo"), It.Is<HttpResponse>(d => d == _context.Object.Response)));
			_redirector.VerifySet(x => x.PreviousPageUrl = It.Is<string>(d => d == "http://localhost:8080/test"));
			_context.VerifySet(x => x.Response.ContentType = It.Is<string>(s => s == "text/html"));
		}
	}
}