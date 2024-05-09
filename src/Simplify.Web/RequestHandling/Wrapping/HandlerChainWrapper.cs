using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.RequestHandling.Wrapping;

public class HandlerChainWrapper(IRequestHandler handler, HandlerChainWrapper? nextHandler = null)
{
	public Task HandleAsync(HttpContext context) =>
		handler.HandleAsync(context, () =>
			 nextHandler != null
			 	? nextHandler.HandleAsync(context)
				: Task.CompletedTask);
}