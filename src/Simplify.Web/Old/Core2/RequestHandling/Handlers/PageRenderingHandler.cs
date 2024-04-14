using System;
using System.Threading.Tasks;
using Simplify.Web.Old.Core2.PageComposition;
using Simplify.Web.Old.Http;

namespace Simplify.Web.Old.Core2.RequestHandling.Handlers;

public class PageRenderingHandler(IPageRenderer renderer) : IRequestHandler
{
	public async Task HandleAsync(IHttpContext context, Action stopProcessing) => await renderer.Render(context);
}