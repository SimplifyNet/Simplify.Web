using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.RequestHandling.Wrapping;

/// <summary>
/// Provides the handler chain wrapper.
/// </summary>
public class HandlerChainWrapper(IRequestHandler handler, HandlerChainWrapper? nextHandler = null)
{
	/// <summary>
	/// Handles the request asynchronously.
	/// </summary>
	/// <param name="context">The context.</param>
	public Task HandleAsync(HttpContext context) =>
		handler.HandleAsync(context, () =>
			 nextHandler != null
			 	? nextHandler.HandleAsync(context)
				: Task.CompletedTask);
}