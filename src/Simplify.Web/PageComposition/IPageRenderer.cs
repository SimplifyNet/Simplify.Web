using System.Threading.Tasks;
using Simplify.Web.Http;

namespace Simplify.Web.PageComposition;

/// <summary>
/// Represents a web-page renderer.
/// </summary>
public interface IPageRenderer
{
	/// <summary>
	/// Processes (build web-page and send it to the client) the current web-page.
	/// </summary>
	/// <param name="context">The context.</param>
	Task Render(IHttpContext context);
}