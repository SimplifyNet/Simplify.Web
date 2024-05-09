using System;
using System.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Simplify.Web.Modules.Context;

namespace Simplify.Web.Modules.Redirection;

public class Redirector(IWebContext context) : IRedirector
{
	public const string PreviousPageUrlCookieFieldName = "PreviousPageUrl";

	public const string RedirectUrlCookieFieldName = "RedirectUrl";

	public const string LoginReturnUrlCookieFieldName = "LoginReturnUrl";

	public const string PreviousNavigatedUrlCookieFieldName = "PreviousNavigatedUrl";

	public string? PreviousPageUrl
	{
		get => context.Request.Cookies[PreviousPageUrlCookieFieldName];
		set => context.Response.Cookies.Append(PreviousPageUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
	}

	public string? RedirectUrl
	{
		get => context.Request.Cookies[RedirectUrlCookieFieldName];
		set => context.Response.Cookies.Append(RedirectUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
	}

	public string? LoginReturnUrl
	{
		get => context.Request.Cookies[LoginReturnUrlCookieFieldName];
		set => context.Response.Cookies.Append(LoginReturnUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
	}

	public string? PreviousNavigatedUrl
	{
		get => context.Request.Cookies[PreviousNavigatedUrlCookieFieldName];
		set => context.Response.Cookies.Append(PreviousNavigatedUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
	}

	public void SetRedirectUrlToCurrentPage() => RedirectUrl = context.Request.GetEncodedUrl();

	public void SetLoginReturnUrlFromCurrentUri() => LoginReturnUrl = context.Request.GetEncodedUrl();

	public void SetPreviousPageUrlToCurrentPage() => PreviousPageUrl = context.Request.GetEncodedUrl();

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

	public void Redirect(string? url)
	{
		if (string.IsNullOrEmpty(url))
			throw new ArgumentNullException(nameof(url));

		if (!url!.StartsWith(context.SiteUrl))
			throw new SecurityException("Redirection outside of the website, redirection URL: " + url);

		context.Response.Redirect(url);
	}
}