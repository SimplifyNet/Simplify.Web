using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http;

/// <summary>
/// Provides local Simplify.Web HTTP context
/// </summary>
/// <seealso cref="IHttpContext" />
public class DefaultHttpContext(HttpContext originalContext, bool isTerminalMiddleware) : IHttpContext
{
	/// <summary>
	/// Gets the original HTTP context.
	/// </summary>
	public HttpContext Context { get; } = originalContext ?? throw new ArgumentNullException(nameof(originalContext));

	/// <summary>
	/// Gets a value indicating whether Simplify.Web performing as terminal middleware.
	/// </summary>
	public bool IsTerminalMiddleware { get; } = isTerminalMiddleware;

	public IHttpRequest Request => new DefaultHttpRequest(originalContext.Request);

	public IHttpResponse Response => new DefaultHttpResponse(originalContext.Response);

	public ClaimsPrincipal? User => Context.User;

	public void SetResponseStatusCode(int code) => Context.Response.StatusCode = code;
}