using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.RequestPath;
using Simplify.Web.StaticFiles;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.RequestHandling.Handlers;

public class StaticFilesHandler(IStaticFileRequestHandlingPipeline pipeline,
	IStaticFileProcessingContextFactory contextFactory,
	IStaticFile file)
		: IRequestHandler
{
	public async Task HandleAsync(HttpContext context, RequestHandlerAsync next)
	{
		var relativeFilePath = context.Request.GetRelativeFilePath();

		if (file.IsValidPath(relativeFilePath))
			await pipeline.ExecuteAsync(contextFactory.Create(context, relativeFilePath), context.Response);
		else
			await next();
	}
}