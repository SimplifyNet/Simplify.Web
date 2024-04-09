using System;
using System.Threading.Tasks;
using Simplify.Web.Core2.Http;
using Simplify.Web.Core2.StaticFiles;

namespace Simplify.Web.Core2.RequestHandling.Handlers;

public class StaticFilesHandler(IStaticFileRequestHandler handler) : IRequestHandler
{
	public async Task HandleAsync(IHttpContext context, Action stopProcessing)
	{
		if (!handler.IsStaticFileRoutePath(context))
			return;

		await handler.ExecuteAsync(context);

		stopProcessing();
	}
}