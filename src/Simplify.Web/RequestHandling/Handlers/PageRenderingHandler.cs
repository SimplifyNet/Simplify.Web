using System;
using System.Threading.Tasks;
using Simplify.Web.Http;
using Simplify.Web.PageComposition;

namespace Simplify.Web.RequestHandling.Handlers;

public class PageRenderingHandler(IPageRenderer renderer) : IRequestHandler
{
	public async Task HandleAsync(IHttpContext context, Action stopProcessing) => await renderer.Render(context);
}