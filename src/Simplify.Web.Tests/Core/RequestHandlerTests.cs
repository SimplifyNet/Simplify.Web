using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Core.Controllers;
using Simplify.Web.Core.StaticFiles;

namespace Simplify.Web.Tests.Core;

[TestFixture]
public class RequestHandlerTests
{
	private Mock<IControllersRequestHandler> _controllersRequestHandler = null!;
	private Mock<IStaticFilesRequestHandler> _staticFilesRequestHandler = null!;
	private RequestHandler _requestHandler = null!;

	[SetUp]
	public void Initialize()
	{
		_controllersRequestHandler = new Mock<IControllersRequestHandler>();
		_staticFilesRequestHandler = new Mock<IStaticFilesRequestHandler>();
		_staticFilesRequestHandler = new Mock<IStaticFilesRequestHandler>();
		_requestHandler = new RequestHandler(_controllersRequestHandler.Object, _staticFilesRequestHandler.Object, true);
	}

	[Test]
	public void ProcessRequest_StaticFilesDisabledButFound_StaticFilesRequestHandlerNotExecuted()
	{
		// Assign
		_requestHandler = new RequestHandler(_controllersRequestHandler.Object, _staticFilesRequestHandler.Object, false);
		_staticFilesRequestHandler.Setup(x => x.IsStaticFileRoutePath(It.IsAny<HttpContext>())).Returns(true);

		// Act
		_requestHandler.ProcessRequest(null!, null!);

		// Assert

		_controllersRequestHandler.Verify(
			x => x.ProcessRequest(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>()), Times.Once);
		_staticFilesRequestHandler.Verify(x => x.ProcessRequest(It.IsAny<HttpContext>()), Times.Never);
	}

	[Test]
	public void ProcessRequest_IsStaticFileRoutePath_StaticFilesRequestHandlerExecuted()
	{
		// Assign
		_staticFilesRequestHandler.Setup(x => x.IsStaticFileRoutePath(It.IsAny<HttpContext>())).Returns(true);

		// Act
		_requestHandler.ProcessRequest(null!, null!);

		// Assert

		_controllersRequestHandler.Verify(x => x.ProcessRequest(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>()), Times.Never);
		_staticFilesRequestHandler.Verify(x => x.ProcessRequest(It.IsAny<HttpContext>()));
	}

	[Test]
	public void ProcessRequest_IsNotStaticFileRoutePath_StaticFilesRequestHandlerExecuted()
	{
		// Assign
		_staticFilesRequestHandler.Setup(x => x.IsStaticFileRoutePath(It.IsAny<HttpContext>())).Returns(false);

		// Act
		_requestHandler.ProcessRequest(null!, null!);

		// Assert

		_controllersRequestHandler.Verify(x => x.ProcessRequest(It.IsAny<IDIContainerProvider>(), It.IsAny<HttpContext>()));
		_staticFilesRequestHandler.Verify(x => x.ProcessRequest(It.IsAny<HttpContext>()), Times.Never);
	}
}