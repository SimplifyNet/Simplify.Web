using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.PageComposition;
using Simplify.Web.Http;

namespace Simplify.Web.Core2.RequestHandling.Handlers;

public class PageRenderingHandler(IPageRenderer renderer) : IRequestHandler
{
	public async Task HandleAsync(IHttpContext context, Action stopProcessing) => await renderer.Render(context);
}