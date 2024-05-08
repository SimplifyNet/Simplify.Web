using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.PageComposition;

/// <summary>
/// Represents a web-page composer.
/// </summary>
public interface IPageComposer
{
	/// <summary>
	/// Composes the current web-page.
	/// </summary>
	/// <param name="context">The context.</param>
	Task<string> ComposeAsync(HttpContext context);
}