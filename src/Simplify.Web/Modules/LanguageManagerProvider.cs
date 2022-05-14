using Microsoft.AspNetCore.Http;
using Simplify.Web.Settings;

namespace Simplify.Web.Modules;

/// <summary>
/// Provides language manager provider
/// </summary>
public class LanguageManagerProvider : ILanguageManagerProvider
{
	private readonly ISimplifyWebSettings _settings;
	private ILanguageManager? _languageManager;

	/// <summary>
	/// Initializes a new instance of the <see cref="LanguageManagerProvider" /> class.
	/// </summary>
	/// <param name="settings">The settings.</param>
	public LanguageManagerProvider(ISimplifyWebSettings settings) => _settings = settings;

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