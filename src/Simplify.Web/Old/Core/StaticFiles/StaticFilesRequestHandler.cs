using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Old.Core.StaticFiles;

/// <summary>
/// Provides static files request handler.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="StaticFilesRequestHandler" /> class.
/// </remarks>
/// <param name="fileHandler">The file handler.</param>
/// <param name="responseFactory">The response factory.</param>
public class StaticFilesRequestHandler(IStaticFileHandler fileHandler, IStaticFileResponseFactory responseFactory) : IStaticFilesRequestHandler
{
	/// <summary>
	/// Determines whether current route path is for static file.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public bool IsStaticFileRoutePath(HttpContext context) => fileHandler.IsStaticFileRoutePath(fileHandler.GetRelativeFilePath(context.Request));

	/// <summary>
	/// Processes the HTTP request for static files.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public async Task<RequestHandlingStatus> ProcessRequest(HttpContext context)
	{
		var relativeFilePath = fileHandler.GetRelativeFilePath(context.Request);
		var lastModificationTime = fileHandler.GetFileLastModificationTime(relativeFilePath);
		var response = responseFactory.Create(context.Response);

		if (fileHandler.IsFileCanBeUsedFromCache(context.Request.Headers["Cache-Control"], fileHandler.GetIfModifiedSinceTime(context.Request.Headers), lastModificationTime))
			await response.SendNotModified(lastModificationTime, relativeFilePath);
		else
			await response.SendNew(await fileHandler.GetFileData(relativeFilePath), lastModificationTime, relativeFilePath);

		return RequestHandlingStatus.RequestWasHandled;
	}
}