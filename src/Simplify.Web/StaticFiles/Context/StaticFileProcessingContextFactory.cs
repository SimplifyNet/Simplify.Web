using Microsoft.AspNetCore.Http;
using Simplify.Web.Http.Cache;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.StaticFiles.Context;

public class StaticFileProcessingContextFactory(IStaticFile file) : IStaticFileProcessingContextFactory
{
	public IStaticFileProcessingContext Create(HttpContext context, string relativeFilePath)
	{
		var lastModificationTime = file.GetLastModificationTime(relativeFilePath);

		return new StaticFileProcessingContext(
			relativeFilePath,
			lastModificationTime,
			context.Request.IsFileCanBeUsedFromCache(lastModificationTime));
	}
}