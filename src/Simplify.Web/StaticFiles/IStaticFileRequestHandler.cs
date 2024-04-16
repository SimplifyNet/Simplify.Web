using System.Threading.Tasks;
using Simplify.Web.Http;

namespace Simplify.Web.StaticFiles;

/// <summary>
/// Represents a static files request handler.
/// </summary>
public interface IStaticFileRequestHandler
{
	/// <summary>
	/// Determines whether a current route path is the route for a static file.
	/// </summary>
	/// <param name="context">The context.</param>
	bool IsStaticFileRoutePath(IHttpContext context);

	/// <summary>
	/// Processes the HTTP request for static files.
	/// </summary>
	/// <param name="context">The context.</param>
	Task ExecuteAsync(IHttpContext context);
}