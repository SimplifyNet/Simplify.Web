using System;
using System.Collections.Generic;

namespace Simplify.Web.RequestHandling.Wrapping;

public static class HandlersListPackExtensions
{
	public static HandlerChainWrapper WrapUp(this IReadOnlyList<IRequestHandler> handlers)
	{
		if (handlers == null || handlers.Count == 0)
			throw new InvalidOperationException("No handlers to process");

		HandlerChainWrapper? currentHandler = null;

		for (var i = handlers.Count; i > 0; i--)
			currentHandler = new HandlerChainWrapper(handlers[i - 1], currentHandler);

		return currentHandler!;
	}
}