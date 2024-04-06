using System.Threading.Tasks;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.StaticFiles;

/// <summary>
/// Represent static files request handler.
/// </summary>
public interface IStaticFileRequestHandler
{
	/// <summary>
	/// Determines whether current route path is for static file.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	bool IsStaticFileRoutePath(IHttpContext context);

	/// <summary>
	/// Processes the HTTP request for static files.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	Task ExecuteAsync(IHttpContext context);
}