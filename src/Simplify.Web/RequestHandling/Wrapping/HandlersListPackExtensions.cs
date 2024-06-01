using System;
using System.Collections.Generic;

namespace Simplify.Web.RequestHandling.Wrapping;

/// <summary>
/// Provides the handlers list pack extensions.
/// </summary>
public static class HandlersListPackExtensions
{
	/// <summary>
	/// Wraps up the handlers list to linked handler chain wrapper.
	/// </summary>
	/// <param name="handlers">The handlers.</param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException">No handlers to process</exception>
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