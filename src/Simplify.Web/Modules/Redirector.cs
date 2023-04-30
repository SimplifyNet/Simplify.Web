using System;

#nullable disable

namespace Simplify.Web.Modules;

/// <summary>
/// Provides website redirection manager, which controls current user location, url to previous page and url to specified page
/// </summary>
public class Redirector : IRedirector
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

	private readonly IWebContext _context;

	/// <summary>
	/// Initializes a new instance of the <see cref="Redirector"/> class.
	/// </summary>
	/// <param name="context">The context.</param>
	public Redirector(IWebContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Gets or sets the previous page url.
	/// </summary>
	/// <value>
	/// The previous page url.
	/// </value>
	public string PreviousPageUrl
	{
		get { return _context.Request.Cookies[PreviousPageUrlCookieFieldName]; }
		set { _context.Response.Cookies.Append(PreviousPageUrlCookieFieldName, value); }
	}

	/// <summary>
	/// Gets or sets the redirect url.
	/// </summary>
	/// <value>
	/// The redirect url.
	/// </value>
	public string RedirectUrl
	{
		get { return _context.Request.Cookies[RedirectUrlCookieFieldName]; }
		set { _context.Response.Cookies.Append(RedirectUrlCookieFieldName, value); }
	}

	/// <summary>
	/// Gets the login return URL.
	/// </summary>
	/// <value>
	/// The login return URL.
	/// </value>
	public string LoginReturnUrl
	{
		get { return _context.Request.Cookies[LoginReturnUrlCookieFieldName]; }
		set { _context.Response.Cookies.Append(LoginReturnUrlCookieFieldName, value); }
	}

	/// <summary>
	/// Gets or sets the previous navigated URL.
	/// </summary>
	/// <value>
	/// The previous navigated URL.
	/// </value>
	public string PreviousNavigatedUrl
	{
		get { return _context.Request.Cookies[PreviousNavigatedUrlCookieFieldName]; }
		set { _context.Response.Cookies.Append(PreviousNavigatedUrlCookieFieldName, value); }
	}

	/// <summary>
	/// Sets the redirect url to current page.
	/// </summary>
	public void SetRedirectUrlToCurrentPage()
	{
		RedirectUrl = _context.Request.Uri.AbsoluteUri;
	}

	/// <summary>
	/// Sets the login return URL from current URI.
	/// </summary>
	public void SetLoginReturnUrlFromCurrentUri()
	{
		LoginReturnUrl = _context.Request.Uri.AbsoluteUri;
	}

	/// <summary>
	/// Navigates the client by specifying redirection type.
	/// </summary>
	/// <param name="redirectionType">Type of the redirection.</param>
	/// <param name="bookmarkName">Name of the bookmark.</param>
	public void Redirect(RedirectionType redirectionType, string bookmarkName = null)
	{
		PreviousNavigatedUrl = _context.Request.Uri.AbsoluteUri;

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
				Redirect(_context.Request.Uri.AbsoluteUri);
				break;

			case RedirectionType.DefaultPage:
				Redirect(_context.SiteUrl);
				break;
		}
	}

	/// <summary>
	/// Redirects the client to specified URL.
	/// </summary>
	/// <param name="url">The URL.</param>
	public void Redirect(string url)
	{
		if (string.IsNullOrEmpty(url))
			throw new ArgumentNullException(nameof(url));

		_context.Response.Redirect(url);
	}
}