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

	private readonly IWebContext _context = context;

	public string? PreviousPageUrl
	{
		get => _context.Request.Cookies[PreviousPageUrlCookieFieldName];
		set => _context.Response.Cookies.Append(PreviousPageUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
	}

	public string? RedirectUrl
	{
		get => _context.Request.Cookies[RedirectUrlCookieFieldName];
		set => _context.Response.Cookies.Append(RedirectUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
	}

	public string? LoginReturnUrl
	{
		get => _context.Request.Cookies[LoginReturnUrlCookieFieldName];
		set => _context.Response.Cookies.Append(LoginReturnUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
	}

	public string? PreviousNavigatedUrl
	{
		get => _context.Request.Cookies[PreviousNavigatedUrlCookieFieldName];
		set => _context.Response.Cookies.Append(PreviousNavigatedUrlCookieFieldName, value ?? "", new CookieOptions
		{
			SameSite = SameSiteMode.None,
			Secure = true
		});
	}

	public void SetRedirectUrlToCurrentPage() => RedirectUrl = _context.Request.GetEncodedUrl();

	public void SetLoginReturnUrlFromCurrentUri() => LoginReturnUrl = _context.Request.GetEncodedUrl();

	public void SetPreviousPageUrlToCurrentPage() => PreviousPageUrl = _context.Request.GetEncodedUrl();

	public void Redirect(RedirectionType redirectionType, string? bookmarkName = null)
	{
		PreviousNavigatedUrl = _context.Request.GetEncodedUrl();

		switch (redirectionType)
		{
			case RedirectionType.RedirectUrl:
				Redirect(string.IsNullOrEmpty(RedirectUrl) ? _context.SiteUrl : RedirectUrl);
				break;

			case RedirectionType.LoginReturnUrl:
				Redirect(string.IsNullOrEmpty(LoginReturnUrl) ? _context.SiteUrl : LoginReturnUrl);
				break;

			case RedirectionType.PreviousPage:
				Redirect(string.IsNullOrEmpty(PreviousPageUrl) ? _context.SiteUrl : PreviousPageUrl);
				break;

			case RedirectionType.PreviousPageWithBookmark:
				Redirect(string.IsNullOrEmpty(PreviousPageUrl) ? _context.SiteUrl : PreviousPageUrl + "#" + bookmarkName);
				break;

			case RedirectionType.CurrentPage:
				Redirect(_context.Request.GetEncodedUrl());
				break;

			case RedirectionType.DefaultPage:
				Redirect(_context.SiteUrl);
				break;

			default:
				throw new ArgumentOutOfRangeException(nameof(redirectionType), redirectionType, null);
		}
	}

	public void Redirect(string? url)
	{
		if (string.IsNullOrEmpty(url))
			throw new ArgumentNullException(nameof(url));

		if (!url!.StartsWith(_context.SiteUrl))
			throw new SecurityException("Redirection outside of the website, redirection URL: " + url);

		_context.Response.Redirect(url);
	}
}