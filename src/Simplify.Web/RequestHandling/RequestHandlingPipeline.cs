using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.RequestHandling.Process;
using Simplify.Web.RequestHandling.Wrapping;

namespace Simplify.Web.RequestHandling;

public class RequestHandlingPipeline(IReadOnlyList<IRequestHandler> handlers) : IRequestHandlingPipeline
{
	private readonly HandlerChainWrapper _startingHandler = handlers.WrapUp();

	public Task ExecuteAsync(HttpContext context) =>
		_startingHandler.HandleAsync(context);
}