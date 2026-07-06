using Microsoft.AspNetCore.Http;
using Simplify.Web.Settings;

namespace Simplify.Web.Modules.Context;

/// <summary>
/// Provides the web context provider.
/// </summary>
/// <seealso cref="IWebContextProvider" />
/// <remarks>
/// Initializes a new instance of the <see cref="WebContextProvider" /> class.
/// </remarks>
/// <param name="settings">The Simplify.Web settings.</param>
public sealed class WebContextProvider(ISimplifyWebSettings settings) : IWebContextProvider
{
	private readonly long _maxRequestBodySize = settings.MaxRequestBodySize;
	private IWebContext? _webContext;

	/// <summary>
	/// Creates the web context.
	/// </summary>
	/// <param name="context">The HTTP context.</param>
	public void Setup(HttpContext context) => _webContext ??= new WebContext(context, _maxRequestBodySize);

	/// <summary>
	/// Gets the web context.
	/// </summary>
	public IWebContext Get() => _webContext!;
}