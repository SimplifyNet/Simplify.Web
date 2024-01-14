using Microsoft.AspNetCore.Http;
using Simplify.Web.Settings;

namespace Simplify.Web.Modules;

/// <summary>
/// Provides language manager provider
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="LanguageManagerProvider" /> class.
/// </remarks>
/// <param name="settings">The settings.</param>
public class LanguageManagerProvider(ISimplifyWebSettings settings) : ILanguageManagerProvider
{
	private readonly ISimplifyWebSettings _settings = settings;
	private ILanguageManager? _languageManager;

	/// <summary>
	/// Creates the language manager instance.
	/// </summary>
	/// <param name="context">The context.</param>
	public void Setup(HttpContext context) => _languageManager ??= new LanguageManager(_settings, context);

	/// <summary>
	/// Gets the language manager.
	/// </summary>
	/// <returns></returns>
	public ILanguageManager Get() => _languageManager!;
}