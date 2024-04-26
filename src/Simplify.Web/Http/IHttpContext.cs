using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http;

/// <summary>
/// Represents local Simplify.Web HTTP context
/// </summary>
public interface IHttpContext
{
	/// <summary>
	/// Gets the original HTTP context.
	/// </summary>
	HttpContext Context { get; }

	ClaimsPrincipal? User { get; }

	IHttpRequest Request { get; }
	IHttpResponse Response { get; }

	void SetResponseStatusCode(int code);
}