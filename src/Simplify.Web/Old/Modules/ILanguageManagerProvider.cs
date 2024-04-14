using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Old.Modules;

/// <summary>
/// Represent language manager provider.
/// </summary>
public interface ILanguageManagerProvider
{
	/// <summary>
	/// Creates the language manager instance.
	/// </summary>
	/// <param name="context">The context.</param>
	void Setup(HttpContext context);

	/// <summary>
	/// Gets the language manager.
	/// </summary>
	ILanguageManager Get();
}