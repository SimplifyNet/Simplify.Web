namespace Simplify.Web.Modules.Redirection;

/// <summary>
/// Provides the redirection types.
/// </summary>
public enum RedirectionType
{
	/// <summary>
	/// Redirects to the default page.
	/// </summary>
	DefaultPage,

	/// <summary>
	/// Redirects to the redirect URL specified in Redirector.
	/// </summary>
	RedirectUrl,

	/// <summary>
	/// Redirects to the login redirect URL specified by OWIN in case of unauthenticated page access.
	/// </summary>
	LoginReturnUrl,

	/// <summary>
	/// Redirects to the previous page URL.
	/// </summary>
	PreviousPage,

	/// <summary>
	/// The previous page URL with bookmark.
	/// </summary>
	PreviousPageWithBookmark,

	/// <summary>
	/// Redirect to the current page (refresh the page).
	/// </summary>
	CurrentPage
}