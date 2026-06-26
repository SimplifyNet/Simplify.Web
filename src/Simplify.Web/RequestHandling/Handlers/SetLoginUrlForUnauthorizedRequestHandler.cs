using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.RequestHandling.Handlers;

/// <summary>
/// Provides the set login URL for unauthorized request handler.
/// </summary>
/// <seealso cref="IRequestHandler" />
public class SetLoginUrlForUnauthorizedRequestHandler(IRedirector redirector) : IRequestHandler
{
	/// <summary>
	/// Handle the request.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="next">The next handler in the chain.</param>
	public async Task HandleAsync(HttpContext context, RequestHandlerAsync next)
	{
		await next();

		// The login return URL is stored in a Set-Cookie header, which cannot be added once the
		// response has started (e.g. a controller that returned 401 together with a body). Skip it
		// in that case instead of throwing "response has already started".
		if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized && !context.Response.HasStarted)
			redirector.SetLoginReturnUrlFromCurrentUri();
	}
}