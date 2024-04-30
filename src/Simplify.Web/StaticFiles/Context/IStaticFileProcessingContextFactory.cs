using Microsoft.AspNetCore.Http;

namespace Simplify.Web.StaticFiles.Context;

public interface IStaticFileProcessingContextFactory
{
	IStaticFileProcessingContext Create(HttpContext context, string relativeFilePath);
}