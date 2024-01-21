namespace Simplify.Web.Modules;

/// <summary>
/// Redirection types.
/// </summary>
public enum RedirectionType
{
	/// <summary>
	/// Redirect to default page.
	/// </summary>
	DefaultPage,

	/// <summary>
	/// Redirect to redirect URL specified in Redirector.
	/// </summary>
	RedirectUrl,

	/// <summary>
	/// Redirect to login redirect URL specified by OWIN in case of unauthenticated page access.
	/// </summary>
	LoginReturnUrl,

	/// <summary>
	/// Redirect to previous page URL.
	/// </summary>
	PreviousPage,

	/// <summary>
	/// The previous page URL with bookmark.
	/// </summary>
	PreviousPageWithBookmark,

	/// <summary>
	/// Redirect to current page (refresh the page).
	/// </summary>
	CurrentPage
}