#nullable disable

using System.Threading.Tasks;
using Simplify.Web.Modules;

namespace Simplify.Web.Responses
{
	/// <summary>
	/// Provides controller redirect response (redirects the client to specified URL.)
	/// </summary>
	public class Redirect : ControllerResponse
	{
		/// <summary>
		/// Redirects the client to specified URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		public Redirect(string url)
		{
			Url = url;
		}

		/// <summary>
		/// Redirects the client by specifying redirection type.
		/// </summary>
		/// <param name="redirectionType">Type of the navigation.</param>
		/// <param name="bookmarkName">Name of the bookmark.</param>
		public Redirect(RedirectionType redirectionType = RedirectionType.DefaultPage, string bookmarkName = null)
		{
			RedirectionType = redirectionType;
			BookmarkName = bookmarkName;
		}

		/// <summary>
		/// Gets the type of the redirection.
		/// </summary>
		/// <value>
		/// The type of the redirection.
		/// </value>
		public RedirectionType RedirectionType { get; }

		/// <summary>
		/// Gets the name of the bookmark.
		/// </summary>
		/// <value>
		/// The name of the bookmark.
		/// </value>
		public string BookmarkName { get; }

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		public string Url { get; }

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override Task<ControllerResponseResult> ProcessAsync()
		{
			if (!string.IsNullOrEmpty(Url))
				Redirector.Redirect(Url);
			else
				Redirector.Redirect(RedirectionType, BookmarkName);

			return Task.FromResult(ControllerResponseResult.Redirect);
		}
	}
}