﻿namespace Simplify.Web.Modules.Localization;

/// <summary>
/// Represents a current language controller and information container.
/// </summary>
public interface ILanguageManager
{
	/// <summary>
	/// Gets the site current language, for example: "en", "ru", "de" etc.
	/// </summary>
	string Language { get; }

	/// <summary>
	/// Sets the site cookie language value.
	/// </summary>
	/// <param name="language">Language code.</param>
	void SetCookieLanguage(string language);

	/// <summary>
	/// Sets the language only for current request.
	/// </summary>
	/// <param name="language">Language code.</param>
	bool SetCurrentLanguage(string language);
}