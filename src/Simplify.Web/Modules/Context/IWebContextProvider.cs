using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Modules.Context;

/// <summary>
/// Represents a web context provider.
/// </summary>
public interface IWebContextProvider
{
	/// <summary>
	/// Creates the web context.
	/// </summary>
	/// <param name="context">The context.</param>
	void Setup(HttpContext context);

	/// <summary>
	/// Gets the web context.
	/// </summary>
	IWebContext Get();
}