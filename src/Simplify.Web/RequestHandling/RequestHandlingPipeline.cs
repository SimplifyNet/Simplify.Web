using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.RequestHandling.Wrapping;

namespace Simplify.Web.RequestHandling;

public class RequestHandlingPipeline(IReadOnlyList<IRequestHandler> handlers) : IRequestHandlingPipeline
{
	private readonly HandlerChainWrapper _startHandler = handlers.WrapUp();

	public Task ExecuteAsync(HttpContext context) =>
		_startHandler.HandleAsync(context);
}