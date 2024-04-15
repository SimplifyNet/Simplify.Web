﻿using System.Threading.Tasks;
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
	private readonly IStaticFileHandler _fileHandler = fileHandler;
	private readonly IStaticFileResponseFactory _responseFactory = responseFactory;

	/// <summary>
	/// Determines whether current route path is for static file.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public bool IsStaticFileRoutePath(HttpContext context) => _fileHandler.IsStaticFileRoutePath(_fileHandler.GetRelativeFilePath(context.Request));

	/// <summary>
	/// Processes the HTTP request for static files.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <returns></returns>
	public async Task<RequestHandlingStatus> ProcessRequest(HttpContext context)
	{
		var relativeFilePath = _fileHandler.GetRelativeFilePath(context.Request);
		var lastModificationTime = _fileHandler.GetFileLastModificationTime(relativeFilePath);
		var response = _responseFactory.Create(context.Response);

		if (_fileHandler.IsFileCanBeUsedFromCache(context.Request.Headers["Cache-Control"], _fileHandler.GetIfModifiedSinceTime(context.Request.Headers), lastModificationTime))
			await response.SendNotModified(lastModificationTime, relativeFilePath);
		else
			await response.SendNew(await _fileHandler.GetFileData(relativeFilePath), lastModificationTime, relativeFilePath);

		return RequestHandlingStatus.RequestWasHandled;
	}
}