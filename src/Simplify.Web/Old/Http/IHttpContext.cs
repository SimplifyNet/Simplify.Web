using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Http;

public interface IHttpContext
{
	HttpContext Context { get; }

	bool IsTerminalMiddleware { get; }
}