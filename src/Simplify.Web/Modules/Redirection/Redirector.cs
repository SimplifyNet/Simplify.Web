using System;
using System.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Simplify.Web.Modules.Context;

namespace Simplify.Web.Modules.Redirection;

/// <summary>
/// Provides the redirector.
/// </summary>
/// <seealso cref="IRedirector" />
public class Redirector(IWebContext context) : IRedirector
{
	/// <summary>
	/// The previous page URL cookie field name
	/// </summary>
	public const string PreviousPageUrlCookieFieldName = "PreviousPageUrl";

	/// <summary>
	/// The redirect URL cookie field name
	/// </summary>
	public const string RedirectUrlCookieFieldName = "RedirectUrl";

	/// <summary>
	/// The login return URL cookie field name
	/// </summary>
	public const string LoginReturnUrlCookieFieldName = "LoginReturnUrl";

	/// <summary>
	/// The previous navigated URL cookie field name
	/// </summary>
	public const string PreviousNavigatedUrlCookieFieldName = "PreviousNavigatedUrl";

	private static CookieOptions BuildSecureRedirectCookieOptions() => new()
	{
		HttpOnly = true,
		SameSite = SameSiteMode.Lax,
		Secure = true
	};

	/// <summary>
	/// Gets or sets the previous page url.
	/// </summary>
	/// <value>
	/// The previous page URL.
	/// </value>
	public string? PreviousPageUrl
	{
		get => context.Request.Cookies[PreviousPageUrlCookieFieldName];
		set => context.Response.Cookies.Append(PreviousPageUrlCookieFieldName, value ?? "", BuildSecureRedirectCookieOptions());
	}

	/// <summary>
	/// Gets or sets the redirect url.
	/// </summary>
	/// <value>
	/// The redirect URL.
	/// </value>
	public string? RedirectUrl
	{
		get => context.Request.Cookies[RedirectUrlCookieFieldName];
		set => context.Response.Cookies.Append(RedirectUrlCookieFieldName, value ?? "", BuildSecureRedirectCookieOptions());
	}

	/// <summary>
	/// Gets the login return URL.
	/// </summary>
	/// <value>
	/// The login return URL.
	/// </value>
	public string? LoginReturnUrl
	{
		get => context.Request.Cookies[LoginReturnUrlCookieFieldName];
		set => context.Response.Cookies.Append(LoginReturnUrlCookieFieldName, value ?? "", BuildSecureRedirectCookieOptions());
	}

	/// <summary>
	/// Gets or sets the previous navigated URL.
	/// </summary>
	/// <value>
	/// The previous navigated URL.
	/// </value>
	public string? PreviousNavigatedUrl
	{
		get => context.Request.Cookies[PreviousNavigatedUrlCookieFieldName];
		set => context.Response.Cookies.Append(PreviousNavigatedUrlCookieFieldName, value ?? "", BuildSecureRedirectCookieOptions());
	}

	/// <summary>
	/// Sets the redirect url to current page.
	/// </summary>
	public void SetRedirectUrlToCurrentPage() => RedirectUrl = context.Request.GetEncodedUrl();

	/// <summary>
	/// Sets the login return URL from current URI.
	/// </summary>
	public void SetLoginReturnUrlFromCurrentUri() => LoginReturnUrl = context.Request.GetEncodedUrl();

	/// <summary>
	/// Sets the previous page URL to current page.
	/// </summary>
	public void SetPreviousPageUrlToCurrentPage() => PreviousPageUrl = context.Request.GetEncodedUrl();

	/// <summary>
	/// Navigates the client by specifying redirection type.
	/// </summary>
	/// <param name="redirectionType">Type of the redirection.</param>
	/// <param name="bookmarkName">Name of the bookmark.</param>
	/// <exception cref="ArgumentOutOfRangeException">redirectionType - null</exception>
	public void Redirect(RedirectionType redirectionType, string? bookmarkName = null)
	{
		PreviousNavigatedUrl = context.Request.GetEncodedUrl();

		switch (redirectionType)
		{
			case RedirectionType.RedirectUrl:
				Redirect(string.IsNullOrEmpty(RedirectUrl) ? context.SiteUrl : RedirectUrl);
				break;

			case RedirectionType.LoginReturnUrl:
				Redirect(string.IsNullOrEmpty(LoginReturnUrl) ? context.SiteUrl : LoginReturnUrl);
				break;

			case RedirectionType.PreviousPage:
				Redirect(string.IsNullOrEmpty(PreviousPageUrl) ? context.SiteUrl : PreviousPageUrl);
				break;

			case RedirectionType.PreviousPageWithBookmark:
				Redirect(string.IsNullOrEmpty(PreviousPageUrl) ? context.SiteUrl : PreviousPageUrl + "#" + bookmarkName);
				break;

			case RedirectionType.CurrentPage:
				Redirect(context.Request.GetEncodedUrl());
				break;

			case RedirectionType.DefaultPage:
				Redirect(context.SiteUrl);
				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(redirectionType), redirectionType, null);
		}
	}

	/// <summary>
	/// Redirects the client to specified URL.
	/// </summary>
	/// <param name="url">The URL.</param>
	/// <exception cref="ArgumentNullException">url</exception>
	/// <exception cref="SecurityException">Redirection outside the website, redirection URL: " + url</exception>
	public void Redirect(string? url)
	{
		if (string.IsNullOrEmpty(url))
			throw new ArgumentNullException(nameof(url));

		if (!IsSameSiteUrl(url!))
			throw new SecurityException("Redirection outside of the website, redirection URL: " + url);

		context.Response.Redirect(url!);
	}

	private bool IsSameSiteUrl(string url)
	{
		// Allow only same-origin relative paths ("/path") and reject protocol-relative
		// ("//evil.com") or backslash-prefixed ("/\\evil.com") variants that browsers
		// treat as absolute.
		if (url.Length > 1 && url[0] == '/' && url[1] != '/' && url[1] != '\\')
			return true;

		if (!Uri.TryCreate(url, UriKind.Absolute, out var target))
			return false;

		if (!Uri.TryCreate(context.SiteUrl, UriKind.Absolute, out var site))
			return url.StartsWith(context.SiteUrl, StringComparison.Ordinal);

		// Match scheme + host + port to avoid being fooled by substring tricks such as
		// "http://localhost.attacker.com/" sharing a SiteUrl prefix.
		return string.Equals(target.Scheme, site.Scheme, StringComparison.OrdinalIgnoreCase)
			&& string.Equals(target.Host, site.Host, StringComparison.OrdinalIgnoreCase)
			&& target.Port == site.Port;
	}
}