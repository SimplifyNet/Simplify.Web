using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.RequestHandling.Wrapping;

namespace Simplify.Web.RequestHandling;

/// <summary>
/// Provides the request handling pipeline.
/// </summary>
/// <seealso cref="IRequestHandlingPipeline" />
public class RequestHandlingPipeline(IReadOnlyList<IRequestHandler> handlers) : IRequestHandlingPipeline
{
	private readonly HandlerChainWrapper _startHandler = handlers.WrapUp();

	/// <summary>
	/// Executes the pipeline.
	/// </summary>
	/// <param name="context">The context.</param>
	public Task ExecuteAsync(HttpContext context) =>
		_startHandler.HandleAsync(context);
}