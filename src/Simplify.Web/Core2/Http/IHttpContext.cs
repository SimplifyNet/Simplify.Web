namespace Simplify.Web.Core2.Http;

public interface IHttpContext
{
	Microsoft.AspNetCore.Http.HttpContext Context { get; }

	bool IsTerminalMiddleware { get; }
}