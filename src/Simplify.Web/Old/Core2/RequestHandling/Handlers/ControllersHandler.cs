using System;
using System.Threading.Tasks;
using Simplify.Web.Old.Core2.Controllers.Processing;
using Simplify.Web.Old.Core2.Controllers.Processing.Context;
using Simplify.Web.Old.Core2.Controllers.RouteMatching;
using Simplify.Web.Old.Http;

namespace Simplify.Web.Old.Core2.RequestHandling.Handlers;

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