using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;

namespace Simplify.Web.Old.Core.PageAssembly;

/// <summary>
/// Represent web-page processor.
/// </summary>
public interface IPageProcessor
{
	/// <summary>
	/// Processes (build web-page and send to client, process current page state) the current web-page.
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	Task<RequestHandlingStatus> ProcessPage(IDIResolver resolver, HttpContext context);
}