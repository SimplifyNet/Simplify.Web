using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Core.Controllers;
using Simplify.Web.Core.PageAssembly;
using Simplify.Web.Modules;

namespace Simplify.Web.Tests.Core.Controllers
{
	[TestFixture]
	public class ControllersRequestHandlerTests
	{
		private Mock<IControllersProcessor> _controllersProcessor = null!;
		private Mock<IPageProcessor> _pageProcessor = null!;
		private Mock<IRedirector> _redirector = null!;
		private ControllersRequestHandler _requestHandler = null!;
		private Mock<HttpContext> _context = null!;

		[SetUp]
		public void Initialize()
		{
			_controllersProcessor = new Mock<IControllersProcessor>();
			_pageProcessor = new Mock<IPageProcessor>();
			_redirector = new Mock<IRedirector>();
			_requestHandler = new ControllersRequestHandler(_controllersProcessor.Object, _pageProcessor.Object, _redirector.Object);

			_context = new Mock<HttpContext>();
			_context.SetupGet(x => x.Response.StatusCode);
		}

		[Test]
		public async Task ProcessRequest_Ok_PageBuiltWithOutput()
		{
			// Assign

			_controllersProcessor.Setup(x => x.ProcessControllers(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>())).Returns(Task.FromResult(ControllersProcessorResult.Ok));
			_pageProcessor.Setup(x => x.ProcessPage(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>())).Returns(Task.FromResult(RequestHandlingStatus.RequestWasHandled));

			// Act
			var result = await _requestHandler.ProcessRequest(null!, _context.Object);

			// Assert
			Assert.AreEqual(RequestHandlingStatus.RequestWasHandled, result);
			_pageProcessor.Verify(x => x.ProcessPage(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>()));
		}

		[Test]
		public async Task ProcessRequest_RawOutput_NoPageBuildsWithOutput()
		{
			// Assign
			_controllersProcessor.Setup(x => x.ProcessControllers(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>())).Returns(
				Task.FromResult(ControllersProcessorResult.RawOutput));

			// Act
			await _requestHandler.ProcessRequest(null!, _context.Object);

			// Assert

			_pageProcessor.Verify(x => x.ProcessPage(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>()), Times.Never);
			_context.VerifySet(x => x.Response.StatusCode = It.IsAny<int>(), Times.Never);
		}

		[Test]
		public async Task ProcessRequest_Http401_401StatusCodeSetNoPageBuiltWithOutput()
		{
			// Assign
			_controllersProcessor.Setup(x => x.ProcessControllers(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>())).Returns(
				Task.FromResult(ControllersProcessorResult.Http401));

			// Act
			await _requestHandler.ProcessRequest(null!, _context.Object);

			// Assert

			_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(d => d == 401));
			_redirector.Verify(x => x.SetLoginReturnUrlFromCurrentUri());
		}

		[Test]
		public async Task ProcessRequest_Http403_403StatusCodeSetNoPageBuiltWithOutput()
		{
			// Assign
			_controllersProcessor.Setup(x => x.ProcessControllers(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>())).Returns(
				Task.FromResult(ControllersProcessorResult.Http403));

			// Act
			await _requestHandler.ProcessRequest(null!, _context.Object);

			// Assert
			_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(d => d == 403));
		}

		[Test]
		public async Task ProcessRequest_Http404_404StatusCodeSetNoPageBuiltWithOutput()
		{
			// Assign
			_controllersProcessor.Setup(x => x.ProcessControllers(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>())).Returns(Task.FromResult(ControllersProcessorResult.Http404));

			// Act
			await _requestHandler.ProcessRequest(null!, _context.Object);

			// Assert

			_pageProcessor.Verify(x => x.ProcessPage(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>()), Times.Never);
			_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(d => d == 404));
		}
	}
}