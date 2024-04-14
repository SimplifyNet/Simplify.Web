using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Old.Http;

public interface IHttpContext
{
	HttpContext Context { get; }

	bool IsTerminalMiddleware { get; }
}