using Simplify.Web.Http;

namespace Simplify.Web.StaticFiles;

/// <summary>
/// Represents a static files handler.
/// </summary>
public interface IStaticFileHandler
{
	bool IsStaticFileRoutePath(IHttpContext context);
}