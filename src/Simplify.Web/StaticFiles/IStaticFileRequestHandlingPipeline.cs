using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.StaticFiles.Context;

namespace Simplify.Web.StaticFiles;

/// <summary>
/// Provides a static file request handling pipeline.
/// </summary>
public interface IStaticFileRequestHandlingPipeline
{
	/// <summary>
	/// Executes the pipeline asynchronously.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="response">The response.</param>
	Task ExecuteAsync(IStaticFileProcessingContext context, HttpResponse response);
}