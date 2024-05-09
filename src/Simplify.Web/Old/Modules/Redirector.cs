using System;
using System.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Simplify.Web.Old.Modules;

/// <summary>
/// Provides website redirection manager, which controls current user location, url to previous page and url to specified page.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Redirector"/> class.
/// </remarks>
/// <param name="context">The context.</param>
public class Redirector(IWebContext context) : IRedirector
{
	/// <summary>
	/// The previous page URL cookie field name.
	/// </summary>
	public const string PreviousPageUrlCookieFieldName = "PreviousPageUrl";

	/// <summary>
	/// The redirect URL cookie field name.
	/// </summary>
	public const string RedirectUrlCookieFieldName = "RedirectUrl";

	/// <summary>
	/// The login return URL cookie field name.
	/// </summary>
	public const string LoginReturnUrlCookieFieldName = "LoginReturnUrl";

	/// <summary>
	/// The previous navigated URL cookie field name
	/// </summary>
	public const string PreviousNavigatedUrlCookieFieldName = "PreviousNavigatedUrl";

	/// <summary>
	/// Gets or sets the previous page url.
	/// </summary>
	/// <value>
	/// The previous page url.
	/// </value>
	public string? PreviousPageUrl
	{
		get => context.Request.Cookies[PreviousPageUrlCookieFieldName];
		set => context.Response.Cookies.Append(PreviousPageUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
	}

	/// <summary>
	/// Gets or sets the redirect url.
	/// </summary>
	/// <value>
	/// The redirect url.
	/// </value>
	public string? RedirectUrl
	{
		get => context.Request.Cookies[RedirectUrlCookieFieldName];
		set => context.Response.Cookies.Append(RedirectUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
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
		set => context.Response.Cookies.Append(LoginReturnUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
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
		set => context.Response.Cookies.Append(PreviousNavigatedUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
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
	public void Redirect(string? url)
	{
		if (string.IsNullOrEmpty(url))
			throw new ArgumentNullException(nameof(url));

		if (!url!.StartsWith(context.SiteUrl))
			throw new SecurityException("Redirection outside of the website, redirection URL: " + url);

		context.Response.Redirect(url);
	}
}