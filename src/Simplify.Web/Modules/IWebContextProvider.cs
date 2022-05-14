using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Modules;

/// <summary>
/// Represent web context provider
/// </summary>
public interface IWebContextProvider
{
	/// <summary>
	/// Creates the web context.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	void Setup(HttpContext context);

	/// <summary>
	/// Gets the web context.
	/// </summary>
	/// <returns></returns>
	IWebContext Get();
}