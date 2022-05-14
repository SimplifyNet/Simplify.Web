using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Core.StaticFiles;

/// <summary>
/// Represent static files request handler
/// </summary>
public interface IStaticFilesRequestHandler
{
	/// <summary>
	/// Determines whether current route path is for static file.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	bool IsStaticFileRoutePath(HttpContext context);

	/// <summary>
	/// Processes the HTTP request for static files.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	Task<RequestHandlingStatus> ProcessRequest(HttpContext context);
}