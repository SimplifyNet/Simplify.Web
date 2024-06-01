using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.Mime;
using Simplify.Web.Http.ResponseTime;
using Simplify.Web.StaticFiles.Context;

namespace Simplify.Web.StaticFiles.Handlers;

/// <summary>
/// Provides the cached file handler.
/// </summary>
/// <seealso cref="IStaticFileRequestHandler" />
public class CachedFileHandler : IStaticFileRequestHandler
{
	/// <summary>
	/// Determines whether this handler can handle the file requested.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns>
	///   <c>true</c> if this handler can handle the file requested; otherwise, <c>false</c>.
	/// </returns>
	public bool CanHandle(IStaticFileProcessingContext context) => context.CanBeCached;

	/// <summary>
	/// Executes the handler asynchronously.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="response">The response.</param>
	public Task ExecuteAsync(IStaticFileProcessingContext context, HttpResponse response)
	{
		response.SetContentMimeType(context.RelativeFilePath);
		response.SetLastModifiedTime(context.LastModificationTime);
		response.StatusCode = (int)HttpStatusCode.NotModified;

		return Task.CompletedTask;
	}
}