using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.StaticFiles;
using Simplify.Web.StaticFiles.Context;

namespace Simplify.Web.Tests.RequestHandling;

[TestFixture]
public class StaticFileRequestHandlingPipelineTests
{
	[Test]
	public async Task ExecuteAsync_ThreeStepsSecondCanHandle_SecondCalled()
	{
		// Arrange

		var handler1 = new Mock<IStaticFileRequestHandler>();
		var handler2 = new Mock<IStaticFileRequestHandler>();
		var handler3 = new Mock<IStaticFileRequestHandler>();

		handler2.Setup(x => x.CanHandle(It.IsAny<IStaticFileProcessingContext>())).Returns(true);

		var pipeline = new StaticFileRequestHandlingPipeline([handler1.Object, handler2.Object, handler3.Object]);

		// Act
		await pipeline.ExecuteAsync(null!, null!);

		// Asset

		handler1.Verify(x => x.Execute(It.IsAny<IStaticFileProcessingContext>(), It.IsAny<HttpResponse>()), Times.Never);
		handler2.Verify(x => x.Execute(It.IsAny<IStaticFileProcessingContext>(), It.IsAny<HttpResponse>()));
		handler3.Verify(x => x.Execute(It.IsAny<IStaticFileProcessingContext>(), It.IsAny<HttpResponse>()), Times.Never);
	}
}