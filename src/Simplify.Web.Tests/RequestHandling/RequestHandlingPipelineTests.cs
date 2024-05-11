using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.RequestHandling;

namespace Simplify.Web.Tests.RequestHandling;

[TestFixture]
public class RequestHandlingPipelineTests
{
	[Test]
	public async Task ExecuteAsync_TwoStepsEachShowRun_TwoStepsRun()
	{
		// Arrange

		var handler1 = new Mock<IRequestHandler>();
		var handler2 = new Mock<IRequestHandler>();

		handler1.Setup(x => x.HandleAsync(It.IsAny<HttpContext>(), It.IsAny<RequestHandlerAsync>()))
			.Callback<HttpContext, RequestHandlerAsync>((context, next) => next());

		var pipeline = new RequestHandlingPipeline([handler1.Object, handler2.Object]);

		// Act
		await pipeline.ExecuteAsync(null!);

		// Asset

		handler1.Verify(x => x.HandleAsync(It.IsAny<HttpContext>(), It.IsAny<RequestHandlerAsync>()));
		handler2.Verify(x => x.HandleAsync(It.IsAny<HttpContext>(), It.IsAny<RequestHandlerAsync>()));
	}

	[Test]
	public async Task ExecuteAsync_TwoStepsFirstExist_FirstRun()
	{
		// Arrange

		var handler1 = new Mock<IRequestHandler>();
		var handler2 = new Mock<IRequestHandler>();

		handler1.Setup(x => x.HandleAsync(It.IsAny<HttpContext>(), It.IsAny<RequestHandlerAsync>()));

		var pipeline = new RequestHandlingPipeline([handler1.Object, handler2.Object]);

		// Act
		await pipeline.ExecuteAsync(null!);

		// Asset

		handler1.Verify(x => x.HandleAsync(It.IsAny<HttpContext>(), It.IsAny<RequestHandlerAsync>()));
		handler2.Verify(x => x.HandleAsync(It.IsAny<HttpContext>(), It.IsAny<RequestHandlerAsync>()), Times.Never);
	}
}