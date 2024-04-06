using System.Threading.Tasks;
using Simplify.Web.Core2.Http;
using Simplify.Web.Core2.StaticFiles;

namespace Simplify.Web.Core2.RequestHandling.Handlers;

public class StaticFilesHandler(IStaticFileRequestHandler handler) : IRequestHandler
{
	public async Task Execute(IHttpContext context, RequestHandler next)
	{
		if (handler.IsStaticFileRoutePath(context))
			await handler.Execute(context);
		else
			await next();
	}
}