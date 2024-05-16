using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.RequestHandling;
using Simplify.Web.RequestHandling.Handlers;
using Simplify.Web.StaticFiles;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.Tests.RequestHandling.Handlers;

[TestFixture]
public class StaticFileHandlerTests
{
	private StaticFilesHandler _handler = null!;

	private Mock<IStaticFileRequestHandlingPipeline> _pipeline = null!;
	private Mock<IStaticFileProcessingContextFactory> _contextFactory = null!;
	private Mock<IStaticFile> _staticFile = null!;

	[SetUp]
	public void Initialize()
	{
		_pipeline = new Mock<IStaticFileRequestHandlingPipeline>();
		_contextFactory = new Mock<IStaticFileProcessingContextFactory>();
		_staticFile = new Mock<IStaticFile>();

		_handler = new StaticFilesHandler(_pipeline.Object, _contextFactory.Object, _staticFile.Object);
	}

	[Test]
	public async Task HandleAsync_PathIsValidStaticFilePath_PipelineCalledWithConstructedContext()
	{
		// Arrange

		var path = "foo";
		var response = Mock.Of<HttpResponse>();

		var httpContext = Mock.Of<HttpContext>(x =>
			x.Request.Path == new PathString("/foo") &&
			x.Response == response);

		var staticFileProcessingContext = Mock.Of<IStaticFileProcessingContext>();

		_contextFactory.Setup(x => x.Create(It.Is<HttpContext>(c => c == httpContext), It.Is<string>(s => s == path))).Returns(staticFileProcessingContext);
		_staticFile.Setup(x => x.IsValidPath(It.Is<string>(s => s == path))).Returns(true);

		// Act
		await _handler.HandleAsync(httpContext, null!);

		// Assert
		_pipeline.Verify(x => x.ExecuteAsync(It.Is<IStaticFileProcessingContext>(c => c == staticFileProcessingContext), It.Is<HttpResponse>(r => r == response)));
	}

	[Test]
	public async Task HandleAsync_PathIsNotValidStaticFilePath_NoStaticFileProcessingAndNextHandlerCalled()
	{
		// Arrange

		var httpContext = Mock.Of<HttpContext>(x => x.Request.Path == new PathString("/foo"));
		var next = new Mock<RequestHandlerAsync>();

		_staticFile.Setup(x => x.IsValidPath(It.IsAny<string>())).Returns(false);

		// Act
		await _handler.HandleAsync(httpContext, next.Object);

		// Assert

		_pipeline.Verify(x => x.ExecuteAsync(It.IsAny<IStaticFileProcessingContext>(), It.IsAny<HttpResponse>()), Times.Never);
		next.Verify(x => x.Invoke());
	}
}