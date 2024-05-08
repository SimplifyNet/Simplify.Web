using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.RequestHandling.Wrapping;

public class HandlerChainWrapper(IRequestHandler handler, HandlerChainWrapper? nextHandler = null)
{
	private readonly IRequestHandler _handler = handler;

	private readonly HandlerChainWrapper? _nextHandler = nextHandler;

	public Task HandleAsync(HttpContext context) =>
		_handler.HandleAsync(context, () =>
			 _nextHandler != null
			 	? _nextHandler.HandleAsync(context)
				: Task.CompletedTask);
}