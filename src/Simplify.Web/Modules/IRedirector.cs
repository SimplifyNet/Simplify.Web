#nullable disable

namespace Simplify.Web.Modules;

/// <summary>
/// Represent website redirection manager, which controls current user location, url to previous page and url to specified page
/// </summary>
public interface IRedirector : IHideObjectMembers
{
	/// <summary>
	/// Gets or sets the previous page url.
	/// </summary>
	/// <value>
	/// The previous page url.
	/// </value>
	string PreviousPageUrl { get; set; }

	/// <summary>
	/// Gets or sets the redirect url.
	/// </summary>
	/// <value>
	/// The redirect url.
	/// </value>
	string RedirectUrl { get; set; }

	/// <summary>
	/// Gets the login return URL.
	/// </summary>
	/// <value>
	/// The login return URL.
	/// </value>
	string LoginReturnUrl { get; set; }

	/// <summary>
	/// Gets or sets the previous navigated URL.
	/// </summary>
	/// <value>
	/// The previous navigated URL.
	/// </value>
	string PreviousNavigatedUrl { get; set; }

	/// <summary>
	/// Sets the redirect url to current page.
	/// </summary>
	void SetRedirectUrlToCurrentPage();

	/// <summary>
	/// Sets the login return URL from current URI.
	/// </summary>
	void SetLoginReturnUrlFromCurrentUri();

	/// <summary>
	/// Navigates the client by specifying redirection type.
	/// </summary>
	/// <param name="redirectionType">Type of the redirection.</param>
	/// <param name="bookmarkName">Name of the bookmark.</param>
	void Redirect(RedirectionType redirectionType, string bookmarkName = null);

	/// <summary>
	/// Redirects the client to specified URL.
	/// </summary>
	/// <param name="url">The URL.</param>
	void Redirect(string url);
}