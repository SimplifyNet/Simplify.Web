using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Processing;
using Simplify.Web.Controllers.Processing.Context;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.RouteMatching.Extensions;

namespace Simplify.Web.RequestHandling.Handlers;

public class ControllersHandler(IMatchedControllersFactory matchedControllersFactory,
	IControllerProcessingPipeline processingPipeline,
	IControllerProcessingContextFactory argsFactory) : IRequestHandler
{
	public async Task HandleAsync(HttpContext context, Action stopProcessing)
	{
		var items = matchedControllersFactory.Create(context);

		if (items.TryProcessUnhandledRoute(context.Response))
			stopProcessing();
		else
			foreach (var item in items)
				await processingPipeline.Execute(argsFactory.Create(item, context));
	}
}