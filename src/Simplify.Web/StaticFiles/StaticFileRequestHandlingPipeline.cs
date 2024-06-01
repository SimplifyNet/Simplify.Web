using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.StaticFiles.Context;

namespace Simplify.Web.StaticFiles;

/// <summary>
/// Provides the static file request handling pipeline.
/// </summary>
/// <seealso cref="IStaticFileRequestHandlingPipeline" />
public class StaticFileRequestHandlingPipeline(IReadOnlyList<IStaticFileRequestHandler> handlers) : IStaticFileRequestHandlingPipeline
{
	/// <summary>
	/// Executes the pipeline asynchronously.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="response">The response.</param>
	public async Task ExecuteAsync(IStaticFileProcessingContext context, HttpResponse response) =>
		await handlers
			.First(x => x.CanHandle(context))
			.ExecuteAsync(context, response);
}