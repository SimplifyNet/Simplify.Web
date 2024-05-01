using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.RequestHandling.Handlers;

public class SetLoginUrlForUnauthorizedRequestHandler(IRedirector redirector) : IRequestHandler
{
	public async Task Handle(HttpContext context, RequestHandlerAsync next)
	{
		await next();

		if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
			redirector.SetLoginReturnUrlFromCurrentUri();
	}
}