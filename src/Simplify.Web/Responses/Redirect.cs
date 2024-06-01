using System.Threading.Tasks;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.Responses;

/// <summary>
/// Provides the controller redirect response (redirects the client to specified URL.).
/// </summary>
/// <seealso cref="ControllerResponse" />
public class Redirect : ControllerResponse
{
	/// <summary>
	/// Redirects the client to specified URL.
	/// </summary>
	/// <param name="url">The URL.</param>
	public Redirect(string url) => Url = url;

	/// <summary>
	/// Redirects the client by specifying redirection type.
	/// </summary>
	/// <param name="redirectionType">Type of the navigation.</param>
	/// <param name="bookmarkName">Name of the bookmark.</param>
	public Redirect(RedirectionType redirectionType = RedirectionType.DefaultPage, string? bookmarkName = null)
	{
		RedirectionType = redirectionType;
		BookmarkName = bookmarkName;
	}

	/// <summary>
	/// Gets the type of the redirection.
	/// </summary>
	public RedirectionType RedirectionType { get; }

	/// <summary>
	/// Gets the name of the bookmark.
	/// </summary>
	public string? BookmarkName { get; }

	/// <summary>
	/// Gets the URL.
	/// </summary>
	public string? Url { get; }

	/// <summary>
	/// Executes this response asynchronously.
	/// </summary>
	public override Task<ResponseBehavior> ExecuteAsync()
	{
		if (!string.IsNullOrEmpty(Url))
			Redirector.Redirect(Url!);
		else
			Redirector.Redirect(RedirectionType, BookmarkName);

		return Task.FromResult(ResponseBehavior.Redirect);
	}
}