using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.PageComposition;

namespace Simplify.Web.RequestHandling.Handlers;

public class PageGenerationHandler(IPageComposer pageComposer, IResponseWriter responseWriter) : IRequestHandler
{
	public async Task HandleAsync(HttpContext context, RequestHandlerAsync next)
	{
		context.Response.ContentType = "text/html";

		await responseWriter.WriteAsync(context.Response, await pageComposer.ComposeAsync(context));
	}
}