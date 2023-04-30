using System.Threading.Tasks;
using Microsoft.Owin;

namespace Simplify.Web.Core.StaticFiles;

/// <summary>
/// Provides static files request handler
/// </summary>
public class StaticFilesRequestHandler : IStaticFilesRequestHandler
{
	private readonly IStaticFileHandler _fileHandler;
	private readonly IStaticFileResponseFactory _responseFactory;

	/// <summary>
	/// Initializes a new instance of the <see cref="StaticFilesRequestHandler" /> class.
	/// </summary>
	/// <param name="fileHandler">The file handler.</param>
	/// <param name="responseFactory">The response factory.</param>
	public StaticFilesRequestHandler(IStaticFileHandler fileHandler, IStaticFileResponseFactory responseFactory)
	{
		_fileHandler = fileHandler;
		_responseFactory = responseFactory;
	}

	/// <summary>
	/// Determines whether current route path is for static file.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public bool IsStaticFileRoutePath(IOwinContext context)
	{
		return _fileHandler.IsStaticFileRoutePath(_fileHandler.GetRelativeFilePath(context.Request));
	}

	/// <summary>
	/// Processes the HTTP request for static files.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public Task ProcessRequest(IOwinContext context)
	{
		var relativeFilePath = _fileHandler.GetRelativeFilePath(context.Request);
		var lastModificationTime = _fileHandler.GetFileLastModificationTime(relativeFilePath);
		var response = _responseFactory.Create(context.Response);

		return _fileHandler.IsFileCanBeUsedFromCache(context.Request.CacheControl,
			_fileHandler.GetIfModifiedSinceTime(context.Request.Headers), lastModificationTime)
			? response.SendNotModified(lastModificationTime, relativeFilePath)
			: response.SendNew(_fileHandler.GetFileData(relativeFilePath), lastModificationTime, relativeFilePath);
	}
}