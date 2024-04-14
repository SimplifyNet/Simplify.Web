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

	/// <summary>
	/// Gets a value indicating whether Simplify.Web performing as terminal middleware.
	/// </summary>
	bool IsTerminalMiddleware { get; }
}