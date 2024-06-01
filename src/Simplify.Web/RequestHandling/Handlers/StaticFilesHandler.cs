using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.RequestPath;
using Simplify.Web.StaticFiles;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.RequestHandling.Handlers;

/// <summary>
/// Provides the static files handler.
/// </summary>
/// <seealso cref="IRequestHandler" />
public class StaticFilesHandler(IStaticFileRequestHandlingPipeline pipeline,
	IStaticFileProcessingContextFactory contextFactory,
	IStaticFile file)
		: IRequestHandler
{
	/// <summary>
	/// Handle the request.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="next">The next handler in the chain.</param>
	public async Task HandleAsync(HttpContext context, RequestHandlerAsync next)
	{
		var relativeFilePath = context.Request.GetRelativeFilePath();

		if (file.IsValidPath(relativeFilePath))
			await pipeline.ExecuteAsync(contextFactory.Create(context, relativeFilePath), context.Response);
		else
			await next();
	}
}