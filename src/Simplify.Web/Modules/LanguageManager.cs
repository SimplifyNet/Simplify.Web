using System;
using System.Globalization;
using System.Threading;
using Microsoft.Owin;
using Simplify.Web.Settings;

#nullable disable

namespace Simplify.Web.Modules;

/// <summary>
/// Current language controller and information container
/// </summary>
public class LanguageManager : ILanguageManager
{
	/// <summary>
	/// Language field name in user cookies
	/// </summary>
	public const string CookieLanguageFieldName = "language";

	private readonly ResponseCookieCollection _responseCookies;

	/// <summary>
	/// Initializes a new instance of the <see cref="LanguageManager" /> class.
	/// </summary>
	/// <param name="settings">The settings.</param>
	/// <param name="context">The OWIN context.</param>
	public LanguageManager(ISimplifyWebSettings settings, IOwinContext context)
	{
		_responseCookies = context.Response.Cookies;

		if (!TrySetLanguageFromCookie(context))
			if (!settings.AcceptBrowserLanguage || (settings.AcceptBrowserLanguage && !TrySetLanguageFromRequestHeader(context)))
				SetCurrentLanguage(settings.DefaultLanguage);
	}

	/// <summary>
	/// Site current language, for example: "en", "ru", "de" etc.
	/// </summary>
	public string Language { get; private set; }

	/// <summary>
	/// Set site cookie language value
	/// </summary>
	/// <param name="language">Language code</param>
	public void SetCookieLanguage(string language)
	{
		if (string.IsNullOrEmpty(language))
			throw new ArgumentNullException(nameof(language));

		_responseCookies.Append(CookieLanguageFieldName, language, new CookieOptions { Expires = DateTime.Now.AddYears(5) });
	}

	/// <summary>
	/// Set language only for current request
	/// </summary>
	/// <param name="language">Language code</param>
	public bool SetCurrentLanguage(string language)
	{
		try
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(language);

			Language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

			return true;
		}
		catch
		{
			return false;
		}
	}

	private bool TrySetLanguageFromCookie(IOwinContext context)
	{
		var cookieLanguage = context.Request.Cookies[CookieLanguageFieldName];

		return !string.IsNullOrEmpty(cookieLanguage) && SetCurrentLanguage(cookieLanguage);
	}

	private bool TrySetLanguageFromRequestHeader(IOwinContext context)
	{
		var languages = context.Request.Headers.GetValues("Accept-Language");

		if (languages != null && languages.Count > 0)
		{
			var languageString = languages[0];

			var items = languageString.Split(';');

			return SetCurrentLanguage(items[0]);
		}

		return false;
	}
}