using System;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Settings;

namespace Simplify.Web.Modules;

/// <summary>
/// Current language controller and information container.
/// </summary>
public class LanguageManager : ILanguageManager
{
	/// <summary>
	/// Language field name in user cookies.
	/// </summary>
	public const string CookieLanguageFieldName = "language";

	/// <summary>
	/// Language field name in request header.
	/// </summary>
	public const string HeaderLanguageFieldName = "Accept-Language";

	private readonly IResponseCookies _responseCookies;

	/// <summary>
	/// Initializes a new instance of the <see cref="LanguageManager" /> class.
	/// </summary>
	/// <param name="settings">The settings.</param>
	/// <param name="context">The OWIN context.</param>
	public LanguageManager(ISimplifyWebSettings settings, HttpContext context)
	{
		_responseCookies = context.Response.Cookies;

		if (settings.AcceptCookieLanguage && TrySetLanguageFromCookie(context))
			return;

		if (!settings.AcceptHeaderLanguage || (settings.AcceptHeaderLanguage && !TrySetLanguageFromRequestHeader(context)))
			if (!SetCurrentLanguage(settings.DefaultLanguage))
				SetInvariantCulture();
	}

	/// <summary>
	/// Site current language, for example: "en", "ru", "de" etc.
	/// </summary>
	public string Language { get; private set; } = null!;

	/// <summary>
	/// Set site cookie language value
	/// </summary>
	/// <param name="language">Language code</param>
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
	/// Set language only for current request.
	/// </summary>
	/// <param name="language">Language code.</param>
	public bool SetCurrentLanguage(string language)
	{
		try
		{
#if NET6_0
			CultureInfo.GetCultureInfo(language, true);
#else
			if (!CultureExists(language))
				return false;
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

	private void SetInvariantCulture()
	{
		Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

		Language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
	}

	private static bool CultureExists(string name)
	{
		var availableCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

		foreach (CultureInfo culture in availableCultures)
			if (culture.Name.Equals(name))
				return true;

		return false;
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

		var items = languageString.Split(';');

		return SetCurrentLanguage(items[0]);
	}
}