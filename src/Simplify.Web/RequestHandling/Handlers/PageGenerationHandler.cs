using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.Page.Composition;

namespace Simplify.Web.RequestHandling.Handlers;

public class PageGenerationHandler(IPageComposer pageComposer, IResponseWriter responseWriter) : IRequestHandler
{
	public Task HandleAsync(HttpContext context, RequestHandlerAsync next)
	{
		context.Response.ContentType = "text/html";

		return responseWriter.WriteAsync(context.Response, pageComposer.Compose());
	}
}