using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.StaticFiles.Cache;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.StaticFiles.Handlers;

/// <summary>
/// Provides the in-memory files cache.
/// </summary>
/// <seealso cref="IStaticFileRequestHandler" />
public class InMemoryFilesCacheHandler(IResponseWriter responseWriter, IStaticFile staticFile) : IStaticFileRequestHandler
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
		response.SetNewReturningFileAttributes(context);

		await responseWriter.WriteAsync(response, GetOrLoad(context));
	}

	private byte[] GetOrLoad(IStaticFileProcessingContext context)
	{
		var key = context.RelativeFilePath;

		if (FilesInMemoryCache.Items.TryGetValue(key, out var cached)
			&& cached.LastModificationTime == context.LastModificationTime)
			return cached.Data;

		var data = staticFile.GetData(context.RelativeFilePath);

		// Cap entries to avoid unbounded growth (e.g. case-variant path DoS).
		if (FilesInMemoryCache.Items.Count < FilesInMemoryCache.MaxItems)
			FilesInMemoryCache.Items[key] = new FilesInMemoryCache.CachedFile(data, context.LastModificationTime);

		return data;
	}
}