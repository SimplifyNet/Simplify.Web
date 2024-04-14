using System.Threading.Tasks;
using Simplify.Web.Old.Http;

namespace Simplify.Web.Old.Core2.PageComposition;

/// <summary>
/// Represent web-page processor.
/// </summary>
public interface IPageRenderer
{
	/// <summary>
	/// Processes (build web-page and send to client, process current page state) the current web-page.
	/// </summary>
	/// <param name="context">The context.</param>
	Task Render(IHttpContext context);
}