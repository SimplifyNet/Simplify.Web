using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.System;
using Simplify.Web.Http.Mime;
using Simplify.Web.Http.ResponseTime;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.StaticFiles.Handlers;

/// <summary>
/// Provides the new file handler.
/// </summary>
/// <seealso cref="IStaticFileRequestHandler" />
public class NewFileHandler(IResponseWriter responseWriter, IStaticFile staticFile) : IStaticFileRequestHandler
{
	/// <summary>
	/// Determines whether this handler can handle the file requested.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns>
	///   <c>true</c> if this handler can handle the file requested; otherwise, <c>false</c>.
	/// </returns>
	public bool CanHandle(IStaticFileProcessingContext context) => !context.CanBeCached;

	/// <summary>
	/// Executes the handler asynchronously.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="response">The response.</param>
	public async Task ExecuteAsync(IStaticFileProcessingContext context, HttpResponse response)
	{
		response.SetContentMimeType(context.RelativeFilePath);
		response.SetLastModifiedTime(context.LastModificationTime);
		response.Headers["Expires"] = new DateTimeOffset(TimeProvider.Current.Now.AddYears(1)).ToString("R");

		await responseWriter.WriteAsync(response, await staticFile.GetDataAsync(context.RelativeFilePath));
	}
}