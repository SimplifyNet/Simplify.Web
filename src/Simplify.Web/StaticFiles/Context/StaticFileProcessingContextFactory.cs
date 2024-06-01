using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.Cache;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.StaticFiles.Context;

/// <summary>
/// Provides the static file processing context factory.
/// </summary>
/// <seealso cref="IStaticFileProcessingContextFactory" />
public class StaticFileProcessingContextFactory(IStaticFile file) : IStaticFileProcessingContextFactory
{
	/// <summary>
	/// Creates the context.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="relativeFilePath">The relative file path.</param>
	public IStaticFileProcessingContext Create(HttpContext context, string relativeFilePath)
	{
		var lastModificationTime = file.GetLastModificationTime(relativeFilePath);

		return new StaticFileProcessingContext(
			relativeFilePath,
			lastModificationTime,
			context.Request.IsFileCanBeUsedFromCache(lastModificationTime));
	}
}