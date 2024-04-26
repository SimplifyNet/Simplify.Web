using System;
using System.Threading.Tasks;
using Simplify.Web.Http;
using Simplify.Web.StaticFiles;
using Simplify.Web.StaticFiles.Context;

namespace Simplify.Web.RequestHandling.Handlers;

public class StaticFilesHandler(IStaticFileProcessingPipeline pipeline, IStaticFileProcessingContextFactory contextFactory, IStaticFileHandler fileHandler) : IRequestHandler
{
	public async Task HandleAsync(IHttpContext context, Action stopProcessing)
	{
		if (!fileHandler.IsStaticFileRoutePath(context))
			return;

		await pipeline.ExecuteAsync(contextFactory.Create(context));

		stopProcessing();
	}
}