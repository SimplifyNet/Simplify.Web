using Microsoft.AspNetCore.Http;

namespace Simplify.Web.StaticFiles.Context;

/// <summary>
/// Represents a static file processing context factory.
/// </summary>
public interface IStaticFileProcessingContextFactory
{
	/// <summary>
	/// Creates the context.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="relativeFilePath">The relative file path.</param>
	IStaticFileProcessingContext Create(HttpContext context, string relativeFilePath);
}