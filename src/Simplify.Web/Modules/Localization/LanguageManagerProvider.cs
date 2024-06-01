using Microsoft.AspNetCore.Http;
using Simplify.Web.Settings;

namespace Simplify.Web.Modules.Localization;

/// <summary>
/// Provides the language manager provider.
/// </summary>
/// <seealso cref="ILanguageManagerProvider" />
/// <remarks>
/// Initializes a new instance of the <see cref="LanguageManagerProvider" /> class.
/// </remarks>
/// <param name="settings">The settings.</param>
public sealed class LanguageManagerProvider(ISimplifyWebSettings settings) : ILanguageManagerProvider
{
	private ILanguageManager? _languageManager;

	/// <summary>
	/// Creates the language manager instance.
	/// </summary>
	/// <param name="context">The context.</param>
	public void Setup(HttpContext context) => _languageManager ??= new LanguageManager(settings, context);

	/// <summary>
	/// Gets the language manager.
	/// </summary>
	public ILanguageManager Get() => _languageManager!;
}