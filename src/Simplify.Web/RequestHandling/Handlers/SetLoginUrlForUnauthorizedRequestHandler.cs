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

		if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
			redirector.SetLoginReturnUrlFromCurrentUri();
	}
}