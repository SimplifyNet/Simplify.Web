using System.Threading.Tasks;
using Simplify.Web.Core2.Http;
using Simplify.Web.Core2.PageComposition;

namespace Simplify.Web.Core2.RequestHandling.Handlers;

public class PageRenderingHandler(IPageRenderer renderer) : IRequestHandler
{
	public async Task Execute(IHttpContext context, RequestHandler next)
	{
		await renderer.Render(context);
		await next();
	}
}