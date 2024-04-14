using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers.Processing;
using Simplify.Web.Core2.Controllers.Processing.Context;
using Simplify.Web.Core2.Controllers.RouteMatching;

namespace Simplify.Web.Core2.RequestHandling.Handlers;

public class ControllersHandler(IMatchedControllersFactory matchedControllersFactory,
	IControllerProcessingPipeline processingPipeline,
	IControllerProcessingContextFactory argsFactory) : IRequestHandler
{
	public async Task HandleAsync(IHttpContext context, Action stopProcessing)
	{
		foreach (var controller in matchedControllersFactory.Create(context))
			await processingPipeline.Execute(argsFactory.Create(controller, context));
	}
}