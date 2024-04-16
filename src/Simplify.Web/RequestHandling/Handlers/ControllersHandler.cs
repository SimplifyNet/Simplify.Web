using System;
using System.Threading.Tasks;
using Simplify.Web.Controllers.Processing;
using Simplify.Web.Controllers.Processing.Context;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Http;

namespace Simplify.Web.RequestHandling.Handlers;

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