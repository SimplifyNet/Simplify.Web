using Microsoft.Owin;
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
	private Mock<IControllersRequestHandler> _controllersRequestHandler;
	private Mock<IStaticFilesRequestHandler> _staticFilesRequestHandler;
	private RequestHandler _requestHandler;

	[SetUp]
	public void Initialize()
	{
		_controllersRequestHandler = new Mock<IControllersRequestHandler>();
		_staticFilesRequestHandler = new Mock<IStaticFilesRequestHandler>();
		_requestHandler = new RequestHandler(_controllersRequestHandler.Object, _staticFilesRequestHandler.Object);
	}

	[Test]
	public void ProcessRequest_IsStaticFileRoutePath_StaticFilesRequestHandlerExecuted()
	{
		// Assign
		_staticFilesRequestHandler.Setup(x => x.IsStaticFileRoutePath(It.IsAny<IOwinContext>())).Returns(true);

		// Act
		_requestHandler.ProcessRequest(null, null);

		// Assert

		_controllersRequestHandler.Verify(x => x.ProcessRequest(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>()), Times.Never);
		_staticFilesRequestHandler.Verify(x => x.ProcessRequest(It.IsAny<IOwinContext>()));
	}

	[Test]
	public void ProcessRequest_IsNotStaticFileRoutePath_StaticFilesRequestHandlerExecuted()
	{
		// Assign
		_staticFilesRequestHandler.Setup(x => x.IsStaticFileRoutePath(It.IsAny<IOwinContext>())).Returns(false);

		// Act
		_requestHandler.ProcessRequest(null, null);

		// Assert

		_controllersRequestHandler.Verify(x => x.ProcessRequest(It.IsAny<IDIContainerProvider>(), It.IsAny<IOwinContext>()));
		_staticFilesRequestHandler.Verify(x => x.ProcessRequest(It.IsAny<IOwinContext>()), Times.Never);
	}
}