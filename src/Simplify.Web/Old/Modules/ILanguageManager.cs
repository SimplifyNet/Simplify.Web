namespace Simplify.Web.Old.Modules;

/// <summary>
/// Represent current language controller and information container.
/// </summary>
public interface ILanguageManager
{
	/// <summary>
	/// Site current language, for example: "en", "ru", "de" etc.
	/// </summary>
	string Language { get; }

	/// <summary>
	/// Set site cookie language value.
	/// </summary>
	/// <param name="language">Language code.</param>
	void SetCookieLanguage(string language);

	/// <summary>
	/// Set language only for current request.
	/// </summary>
	/// <param name="language">Language code.</param>
	bool SetCurrentLanguage(string language);
}