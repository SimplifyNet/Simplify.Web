using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Core2.Http;

public interface IHttpContext
{
	HttpContext Context { get; }

	bool IsTerminalMiddleware { get; }
}