using System;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http;

public class DefaultHttpContext(HttpContext context, bool isTerminalMiddleware) : IHttpContext
{
	public HttpContext Context { get; } = context ?? throw new ArgumentNullException(nameof(context));

	public bool IsTerminalMiddleware { get; } = isTerminalMiddleware;
}