using System;
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
	public async Task HandleAsync(HttpContext context, Action stopProcessing)
	{
		var relativeFilePath = context.Request.GetRelativeFilePath();

		if (relativeFilePath == null || !file.IsValidPath(relativeFilePath))
			return;

		await pipeline.ExecuteAsync(contextFactory.Create(context, relativeFilePath), context.Response);

		stopProcessing();
	}
}