using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.PageComposition;

namespace Simplify.Web.RequestHandling.Handlers;

public class PageRenderingHandler(IPageRenderer renderer) : IRequestHandler
{
	public async Task HandleAsync(HttpContext context, Action stopProcessing) => await renderer.Render(context);
}