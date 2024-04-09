using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Controllers;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.RequestHandling.Handlers;

public class ControllersHandler(IControllersRequestHandler requestHandler, IControllersProcessingContextFactory contextFactory) : IRequestHandler
{
	public Task HandleAsync(IHttpContext context, Action stopProcessing) =>
		requestHandler.HandleAsync(contextFactory.Create(context));
}