using System;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http;

/// <summary>
/// Provides local Simplify.Web HTTP context
/// </summary>
/// <seealso cref="IHttpContext" />
public class DefaultHttpContext(HttpContext context, bool isTerminalMiddleware) : IHttpContext
{
	/// <summary>
	/// Gets the original HTTP context.
	/// </summary>
	public HttpContext Context { get; } = context ?? throw new ArgumentNullException(nameof(context));

	/// <summary>
	/// Gets a value indicating whether Simplify.Web performing as terminal middleware.
	/// </summary>
	public bool IsTerminalMiddleware { get; } = isTerminalMiddleware;
}