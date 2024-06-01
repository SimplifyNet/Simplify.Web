using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.Page.Composition;

namespace Simplify.Web.RequestHandling.Handlers;

/// <summary>
/// Provides the page generation handler.
/// </summary>
/// <seealso cref="IRequestHandler" />
public class PageGenerationHandler(IPageComposer pageComposer, IResponseWriter responseWriter) : IRequestHandler
{
	/// <summary>
	/// Handle the request.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="next">The next handler in the chain.</param>
	public Task HandleAsync(HttpContext context, RequestHandlerAsync next)
	{
		context.Response.ContentType = "text/html";

		return responseWriter.WriteAsync(context.Response, pageComposer.Compose());
	}
}