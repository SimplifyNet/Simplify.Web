﻿using System;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Settings;

namespace Simplify.Web.Modules.Localization;

/// <summary>
/// Provides the application language handler and information container.
/// </summary>
/// <seealso cref="ILanguageManager" />
public class LanguageManager : ILanguageManager
{
	/// <summary>
	/// Provides the language field name in user cookies.
	/// </summary>
	public const string CookieLanguageFieldName = "language";

	/// <summary>
	/// Provides the language field name in request header.
	/// </summary>
	public const string HeaderLanguageFieldName = "Accept-Language";

	private readonly IResponseCookies _responseCookies;

	/// <summary>
	/// Initializes a new instance of the <see cref="LanguageManager" /> class.
	/// </summary>
	/// <param name="settings">The settings.</param>
	/// <param name="context">The HTTP context.</param>
	public LanguageManager(ISimplifyWebSettings settings, HttpContext context)
	{
		_responseCookies = context.Response.Cookies;

		if (settings.AcceptCookieLanguage && TrySetLanguageFromCookie(context))
			return;

		if ((!settings.AcceptHeaderLanguage ||
			 (settings.AcceptHeaderLanguage && !TrySetLanguageFromRequestHeader(context))) && !SetCurrentLanguage(settings.DefaultLanguage))
			SetInvariantCulture();
	}

	/// <summary>
	/// Provides the site current language, for example: "en", "ru", "de" etc.
	/// </summary>
	public string Language { get; private set; } = null!;

	/// <summary>
	/// Sets the site cookie language value
	/// </summary>
	/// <param name="language">The language code</param>
	public void SetCookieLanguage(string? language)
	{
		if (string.IsNullOrEmpty(language))
			throw new ArgumentNullException(nameof(language));

		_responseCookies.Append(CookieLanguageFieldName, language, new CookieOptions
		{
			Expires = DateTime.Now.AddYears(5),
			SameSite = SameSiteMode.None,
			Secure = true
		});
	}

	/// <summary>
	/// Sets the language only for current request.
	/// </summary>
	/// <param name="language">The language code.</param>
	public bool SetCurrentLanguage(string language)
	{
		try
		{
#if NETSTANDARD2_0 || NETSTANDARD2_1
			if (!CultureExists(language))
				return false;
#else
			CultureInfo.GetCultureInfo(language, true);
#endif
		}
		catch
		{
			return false;
		}

		Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
		Thread.CurrentThread.CurrentCulture = new CultureInfo(language);

		Language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

		return true;
	}

#if NETSTANDARD2_0 || NETSTANDARD2_1
	private static bool CultureExists(string name) =>
		Array.Exists(CultureInfo.GetCultures(CultureTypes.AllCultures), culture => culture.Name.Equals(name));
#endif

	private void SetInvariantCulture()
	{
		Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

		Language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
	}

	private bool TrySetLanguageFromCookie(HttpContext context)
	{
		var cookieLanguage = context.Request.Cookies[CookieLanguageFieldName];

		return !string.IsNullOrEmpty(cookieLanguage) && SetCurrentLanguage(cookieLanguage);
	}

	private bool TrySetLanguageFromRequestHeader(HttpContext context)
	{
		var languages = context.Request.Headers[HeaderLanguageFieldName];

		if (languages.Count == 0)
			return false;

		var languageString = languages[0];

		var items = languageString!.Split(';');

		return SetCurrentLanguage(items[0]);
	}
}