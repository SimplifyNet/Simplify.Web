namespace Simplify.Web.Modules.Localization;

/// <summary>
/// Represents an application language handler and information container.
/// </summary>
public interface ILanguageManager
{
	/// <summary>
	/// Gets the application current language, for example: "en", "ru", "de" etc.
	/// </summary>
	/// <value>
	/// The language.
	/// </value>
	string Language { get; }

	/// <summary>
	/// Sets the site cookie language value.
	/// </summary>
	/// <param name="language">The language code.</param>
	void SetCookieLanguage(string language);

	/// <summary>
	/// Sets the language only for current request.
	/// </summary>
	/// <param name="language">The language code.</param>
	bool SetCurrentLanguage(string language);
}