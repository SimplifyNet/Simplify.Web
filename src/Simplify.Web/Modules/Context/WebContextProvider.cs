using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Modules.Context;

/// <summary>
/// Provides the web context provider.
/// </summary>
public sealed class WebContextProvider : IWebContextProvider
{
	private IWebContext? _webContext;

	/// <summary>
	/// Creates the web context.
	/// </summary>
	/// <param name="context">The HTTP context.</param>
	public void Setup(HttpContext context) => _webContext ??= new WebContext(context);

	/// <summary>
	/// Gets the web context.
	/// </summary>
	public IWebContext Get() => _webContext!;
}