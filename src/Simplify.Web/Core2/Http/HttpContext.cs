using System;

namespace Simplify.Web.Core2.Http;

public class HttpContext(Microsoft.AspNetCore.Http.HttpContext context, bool isTerminalMiddleware) : IHttpContext
{
	public Microsoft.AspNetCore.Http.HttpContext Context { get; } = context ?? throw new ArgumentNullException(nameof(context));

	public bool IsTerminalMiddleware { get; } = isTerminalMiddleware;
}