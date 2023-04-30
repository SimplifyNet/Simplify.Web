using Microsoft.Owin;

#nullable disable

namespace Simplify.Web.Modules;

/// <summary>
/// Provides web context provider
/// </summary>
public class WebContextProvider : IWebContextProvider
{
	private IWebContext _webContext;

	/// <summary>
	/// Creates the web context.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public void Setup(IOwinContext context)
	{
		if (_webContext == null)
			_webContext = new WebContext(context);
	}

	/// <summary>
	/// Gets the web context.
	/// </summary>
	/// <returns></returns>
	public IWebContext Get()
	{
		return _webContext;
	}
}