namespace Simplify.Web.Modules.Redirection;

/// <summary>
/// Represents a website redirection manager, which controls current user location, url to previous page and url to specified page.
/// </summary>
public interface IRedirector
{
	/// <summary>
	/// Gets or sets the previous page url.
	/// </summary>
	string? PreviousPageUrl { get; set; }

	/// <summary>
	/// Gets or sets the redirect url.
	/// </summary>
	string? RedirectUrl { get; set; }

	/// <summary>
	/// Gets the login return URL.
	/// </summary>
	string? LoginReturnUrl { get; set; }

	/// <summary>
	/// Gets or sets the previous navigated URL.
	/// </summary>
	string? PreviousNavigatedUrl { get; set; }

	/// <summary>
	/// Sets the redirect url to current page.
	/// </summary>
	void SetRedirectUrlToCurrentPage();

	/// <summary>
	/// Sets the login return URL from current URI.
	/// </summary>
	void SetLoginReturnUrlFromCurrentUri();

	/// <summary>
	/// Sets the previous page URL to current page.
	/// </summary>
	void SetPreviousPageUrlToCurrentPage();

	/// <summary>
	/// Navigates the client by specifying redirection type.
	/// </summary>
	/// <param name="redirectionType">Type of the redirection.</param>
	/// <param name="bookmarkName">Name of the bookmark.</param>
	void Redirect(RedirectionType redirectionType, string? bookmarkName = null);

	/// <summary>
	/// Redirects the client to specified URL.
	/// </summary>
	/// <param name="url">The URL.</param>
	void Redirect(string url);
}